import React from 'react'
import { Box } from '@mui/material'
import PropTypes from 'prop-types'
import useStyles from './style'

const LoginLayout = (props) => {
  const { children } = props
  const classes = useStyles()

  return (
    <Box
      display="flex"
      minHeight="100vh"
      alignItems="center"
      justifyContent="center"
      className={classes.container}
    >
      {children}
    </Box>
  )
}

LoginLayout.propTypes = {
  children: PropTypes.element.isRequired,
}

export default LoginLayout
