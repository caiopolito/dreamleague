import React, { useCallback, useEffect, useState } from 'react'
import PropTypes from 'prop-types'
import MomentUtils from '@date-io/moment'
import DatePicker from '@mui/lab/DatePicker'
import TextField from '@mui/material/TextField'
import { makeStyles } from '@mui/styles'
import DateAdapter from '@mui/lab/AdapterMoment'
import LocalizationProvider from '@mui/lab/LocalizationProvider'

import useUtils from '../../hooks/useUtils'

const useStyles = makeStyles(() => ({
  date: {
    '& .MuiOutlinedInput-input': {
      padding: '13.5px 14px',
    },
  },
}))
function DateInput({
  id,
  label,
  value,
  onChange,
  helperText,
  disabled,
  hidden,
  error,
}) {
  const { formatDateApi } = useUtils()
  const classes = useStyles()

  const getValue = useCallback(() => (value !== '' ? value : null), [value])

  const [date, setDate] = useState(getValue())

  useEffect(() => {
    setDate(getValue())
  }, [value, getValue])

  const handleChange = (event) => {
    const dateValue = Date.parse(event) ? formatDateApi(event) : ''
    setDate(dateValue)

    onChange({
      target: { id, value: dateValue },
    })
  }

  const handleBlur = () => {
    if (date === '') {
      setDate(null)
    }
  }

  return (
    <LocalizationProvider dateAdapter={DateAdapter} utils={MomentUtils}>
      <DatePicker
        autoOk
        showTodayButton
        disableToolbar
        variant="standard"
        format="DD/MM/YYYY"
        id={id}
        label={label}
        value={date}
        onChange={handleChange}
        onBlur={handleBlur}
        invalidDateMessage="Data inválida"
        helperText={helperText}
        error={Boolean(error)}
        hidden={Boolean(hidden)}
        disabled={disabled}
        KeyboardButtonProps={{
          'aria-label': 'change date',
        }}
        renderInput={(params) => <TextField className={classes.date} {...params} />}

      />
    </LocalizationProvider>
  )
}

DateInput.propTypes = {
  id: PropTypes.string.isRequired,
  label: PropTypes.string,
  value: PropTypes.string.isRequired,
  onChange: PropTypes.func,
  helperText: PropTypes.string,
  disabled: PropTypes.bool,
  hidden: PropTypes.bool,
  error: PropTypes.bool,
}

DateInput.defaultProps = {
  label: '',
  helperText: undefined,
  onChange: () => { },
  disabled: false,
  hidden: false,
  error: false,
}

export default DateInput
