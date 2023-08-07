import React, { useEffect, useState } from 'react'
import { Box, CircularProgress } from '@mui/material'
import { makeStyles } from '@mui/styles'
import PropTypes from 'prop-types'

const useStyles = makeStyles((theme) => ({
  root: {
    width: '100%',
    height: '100%',
    zIndex: 1000001,
    display: 'flex',
    position: 'fixed',
    alignItems: 'center',
    flexDirection: 'column',
    justifyContent: 'center',
    padding: theme.spacing(3),
    backgroundColor: '#0000004d',
  },
}))

const LOADER_ENABLE = 'loader.enable'
const LOADER_DISABLE = 'loader.disable'

const Loader = ({ show }) => {
  const classes = useStyles()
  const [disabled, setDisabled] = useState(0)

  useEffect(() => {
    const enable = () => setDisabled((count) => --count)
    document.addEventListener(LOADER_ENABLE, enable)

    const disable = () => setDisabled((count) => ++count)
    document.addEventListener(LOADER_DISABLE, disable)

    return () => {
      document.removeEventListener(LOADER_ENABLE, enable)
      document.removeEventListener(LOADER_DISABLE, disable)
    }
  }, [])

  return (
    <>
      {disabled === 0 && show && (
        <Box className={classes.root}>
          <CircularProgress />
        </Box>
      )}
    </>
  )
}

export const useLoader = () => {
  const enableLoader = () => document.dispatchEvent(new CustomEvent(LOADER_ENABLE))

  const disableLoader = () => document.dispatchEvent(new CustomEvent(LOADER_DISABLE))

  return {
    enableLoader,
    disableLoader,
  }
}

Loader.propTypes = {
  show: PropTypes.bool.isRequired,
}

export default Loader
