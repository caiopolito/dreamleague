import React, { useMemo, useState, useEffect } from 'react'
import PropTypes from 'prop-types'
import InputMask from 'react-input-mask'
import { Input } from '@mui/material'

export const PHONE_SIZE_WITHOUT_MASK = 10

export const PHONE_MASKS = {
  auto: '(99) 9999-99999',
  phone: '(99) 9999-9999',
  cellphone: '(99) 99999-9999',
}

const PhoneInput = (props) => {
  const { phoneType, value } = props
  const [mask, setMask] = useState(PHONE_MASKS[phoneType])

  useEffect(() => {
    if (phoneType === 'auto') {
      const number = value.replace(/\D/g, '')

      if (number.length <= PHONE_SIZE_WITHOUT_MASK) {
        setMask(PHONE_MASKS.auto)
      } else {
        setMask(PHONE_MASKS.cellphone)
      }
    }
  }, [phoneType, value])

  const field = useMemo(() => {
    const inputProps = { ...props }
    delete inputProps.onBlur
    delete inputProps.onChange
    delete inputProps.phoneType

    return <Input {...inputProps} />
  }, [props])

  const maskProps = { ...props }
  delete maskProps.phoneType

  return (
    <InputMask
      mask={mask}
      {...maskProps}
      maskChar={null}
    >
      {() => field}
    </InputMask>
  )
}

PhoneInput.propTypes = {
  value: PropTypes.string,
  onChange: PropTypes.func,
  id: PropTypes.string.isRequired,
  phoneType: PropTypes.oneOf(['auto', 'cellphone', 'phone']),
}

PhoneInput.defaultProps = {
  value: '',
  phoneType: 'auto',
  onChange: () => { },
}

export default PhoneInput
