import React, { useState, useEffect, useCallback } from 'react'
import PropTypes from 'prop-types'
import { Fade } from '@mui/material'

const Resolve = ({
  fallback,
  onLoaded,
  className,
  errorFallback,
  children,
  resolve: value,
}) => {
  const [loading, setLoading] = useState(true)
  const [errorLoading, setErrorLoading] = useState(false)

  const loadSnapshot = useCallback(
    (data) => new Promise((resolve, reject) => {
      const keys = Object.keys(data)
      const promises = keys.map((key) => data[key]())

      Promise.all(promises).then(
        (values) => {
          const dataSnapshot = {}

          keys.forEach((key) => {
            const index = keys.indexOf(key)
            dataSnapshot[key] = values[index]
          })
          resolve(dataSnapshot)
        },
        () => reject(),
      )
    }),
    [],
  )

  useEffect(() => {
    const snapshot = loadSnapshot(value)

    new Promise((resolve) => {
      snapshot.then(
        (data) => {
          onLoaded(data, resolve)
          setLoading(false)
        },
        () => {
          setLoading(false)
          setErrorLoading(true)
        },
      )
    }).then(() => setLoading(false))
  }, [value, onLoaded, loadSnapshot])

  return (
    <>
      {loading && (
        <Fade in>
          <div>{fallback}</div>
        </Fade>
      )}

      {errorLoading && (
        <Fade in>
          <div>{errorFallback}</div>
        </Fade>
      )}

      {!errorLoading && !loading && (
        <Fade in className={className}>
          <div>{children}</div>
        </Fade>
      )}
    </>
  )
}

Resolve.propTypes = {
  className: PropTypes.string,
  fallback: PropTypes.element,
  errorFallback: PropTypes.element,
  children: PropTypes.element.isRequired,
  resolve: PropTypes.object.isRequired,
  onLoaded: PropTypes.func.isRequired,
}

Resolve.defaultProps = {
  className: '',
  fallback: <></>,
  errorFallback: <></>,
}

export default Resolve
