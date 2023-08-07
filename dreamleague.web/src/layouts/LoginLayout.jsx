import React from 'react'
import { Box } from '@mui/material'
import PropTypes from 'prop-types'

const LoginLayout = (props) => {
  const { children } = props

  return (
    <Box
      display="flex"
      minHeight="100vh"
      alignItems="center"
      justifyContent="center"
      backgroundColor="#1b1d1d"
    >
      {children}
    </Box>
  )
}

LoginLayout.propTypes = {
  children: PropTypes.element.isRequired,
}

export default LoginLayout
