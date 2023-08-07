/* eslint-disable react-hooks/exhaustive-deps */
import React, {
  useState, useEffect, useCallback, useMemo,
} from 'react'
import {
  string, func, number, bool, shape, oneOfType, instanceOf, object,
} from 'prop-types'

import { DEFAULT_CONFIG_CURRENCY } from 'constants/defaults'
import formatCurrency from './format-currency'

const defaultConfig = DEFAULT_CONFIG_CURRENCY

const CurrencyInput = ({
  component: InputComponent,
  value,
  defaultValue,
  config,
  currency,
  max,
  autoFocus,
  autoSelect,
  autoReset,
  onChange,
  onBlur,
  onFocus,
  onKeyPress,
  inputRef,
  ...otherProps
}) => {
  const localInputRef = useCallback((node) => {
    const isActive = node === document.activeElement

    if (node && autoFocus && !isActive) {
      node.focus()
    }
  }, [autoFocus])

  const [maskedValue, setMaskedValue] = useState('0')

  const safeConfig = useMemo(() => () => {
    const { formats: { number: { [currency]: { maximumFractionDigits } } } } = config

    const finalConfig = {
      ...defaultConfig,
      ...config,
    }

    finalConfig.formats.number[currency].minimumFractionDigits = maximumFractionDigits

    return finalConfig
  }, [defaultConfig, config])

  const clean = (numberClean) => {
    if (typeof numberClean === 'number') {
      return numberClean
    }

    return Number(numberClean.toString().replace(/[^0-9-]/g, ''))
  }

  const normalizeValue = (numberNormalize) => {
    const { formats: { number: { [currency]: { maximumFractionDigits } } } } = safeConfig()
    let safeNumber = numberNormalize

    if (typeof numberNormalize === 'string') {
      safeNumber = clean(numberNormalize)

      if (safeNumber % 1 !== 0) {
        safeNumber = safeNumber.toFixed(maximumFractionDigits)
      }
    } else {
      safeNumber = Number.isInteger(numberNormalize)
        ? Number(numberNormalize) * (10 ** maximumFractionDigits)
        : numberNormalize.toFixed(maximumFractionDigits)
    }

    return clean(safeNumber) / (10 ** maximumFractionDigits)
  }

  const calculateValues = (inputFieldValue) => {
    const valueField = normalizeValue(inputFieldValue)
    const maskedValueField = formatCurrency(valueField, safeConfig(), currency)

    return [valueField, maskedValueField]
  }

  const updateValues = (valueUpdate) => {
    const [calculatedValue, calculatedMaskedValue] = calculateValues(valueUpdate)

    if (!max || calculatedValue <= max) {
      setMaskedValue(calculatedMaskedValue)

      return [calculatedValue, calculatedMaskedValue]
    }
    return [normalizeValue(maskedValue), maskedValue]
  }

  const handleChange = (event) => {
    event.preventDefault()

    const [valueInput, maskedValueInput] = updateValues(event.target.value)

    if (maskedValueInput) {
      onChange(event, valueInput, maskedValueInput)
    }
  }

  const handleBlur = (event) => {
    const [valueBlur, maskedValueBlur] = updateValues(event.target.value)

    if (autoReset) {
      calculateValues(0)
    }

    if (maskedValueBlur) {
      onBlur(event, valueBlur, maskedValueBlur)
    }
  }

  const handleFocus = (event) => {
    if (autoSelect) {
      event.target.select()
    }

    const [valueFocus, maskedValueFocus] = updateValues(event.target.value)

    if (maskedValueFocus) {
      onFocus(event, valueFocus, maskedValueFocus)
    }
  }

  const handleKeyUp = (event) => onKeyPress(event, event.key, event.keyCode)

  useEffect(() => {
    const currentValue = value || defaultValue || 0
    const [, maskedValueLoad] = calculateValues(currentValue)

    setMaskedValue(maskedValueLoad)
  }, [currency, value, defaultValue, config])

  return (
    <InputComponent
      {...otherProps}
      ref={inputRef || localInputRef}
      value={maskedValue}
      onChange={handleChange}
      onBlur={handleBlur}
      onFocus={handleFocus}
      onKeyUp={handleKeyUp}
    />
  )
}

CurrencyInput.propTypes = {
  defaultValue: number,
  value: number,
  max: number,
  component: object,
  currency: string,
  config: shape(),
  autoFocus: bool,
  autoSelect: bool,
  autoReset: bool,
  onChange: func,
  onBlur: func,
  onFocus: func,
  onKeyPress: func,
  inputRef: oneOfType([
    func,
    shape({ current: instanceOf(Element) }),
  ]),
}

CurrencyInput.defaultProps = {
  component: 'input',
  currency: 'BRL',
  value: 0,
  config: defaultConfig,
  autoFocus: false,
  autoSelect: false,
  autoReset: false,
  onChange: (f) => f,
  onBlur: (f) => f,
  onFocus: (f) => f,
  onKeyPress: (f) => f,
  inputRef: null,
  defaultValue: 0,
  max: 0,
}

export default CurrencyInput
