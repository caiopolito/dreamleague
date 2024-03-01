import React from 'react'
import PropTypes from 'prop-types'
import { TextField } from '@mui/material'
import CurrencyInput from './CurrencyInputCore'

const CurrencyInputCustom = (props) => {
  const { value } = props
  const valueInput = parseFloat(value)

  return (
    <CurrencyInput
      {...props}
      autoSelect
      currency="BRL"
      value={valueInput || 0}
      component={TextField}
    />
  )
}

CurrencyInputCustom.propTypes = {
  value: PropTypes.oneOfType([
    PropTypes.string,
    PropTypes.number,
  ]),
}

CurrencyInputCustom.defaultProps = {
  value: 0,
}

export default CurrencyInputCustom
