import React, { useEffect, useState } from 'react'
import PropTypes from 'prop-types'
import axios from 'axios'

axios.defaults.baseURL = process.env.REACT_APP_URL_API

const AxiosSetting = ({
  onStartRequest: startRequest,
  onStopRequest: stopRequest,
  handleError,
}) => {
  const [count, setCount] = useState(0)
  const [responseError, setResponseError] = useState()

  useEffect(() => {
    const requestId = axios.interceptors.request.use(
      (config) => {
        setCount((current) => current + 1)

        const newConfig = { ...config }

        return newConfig
      },
      (error) => {
        setCount((current) => current - 1)
        return Promise.reject(error)
      },
    )

    const responseId = axios.interceptors.response.use(
      (response) => {
        setCount((current) => current - 1)
        return response
      },
      (error) => {
        let { response = {} } = error

        if (response.status === 401 || response.status === 403) {
          response = { ...response, data: '' }
        }

        setResponseError(response)
        setCount((current) => current - 1)
        return Promise.reject(response)
      },
    )

    return () => {
      axios.interceptors.request.eject(requestId)
      axios.interceptors.response.eject(responseId)
    }
  }, [])

  useEffect(() => {
    if (count === 1) {
      startRequest()
    }

    if (count === 0) {
      stopRequest()
    }
  }, [count, startRequest, stopRequest])

  useEffect(() => {
    if (responseError) {
      const handle = handleError[responseError.status]

      if (handle) {
        handle(responseError)
      }
    }
  }, [responseError, handleError])

  return <></>
}

AxiosSetting.propTypes = {
  onStartRequest: PropTypes.func.isRequired,
  onStopRequest: PropTypes.func.isRequired,
  handleError: PropTypes.object,
}

AxiosSetting.defaultProps = {
  handleError: {},
}

export default AxiosSetting
