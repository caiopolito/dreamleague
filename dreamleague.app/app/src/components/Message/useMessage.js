import { useCallback } from 'react'
import { useSnackbar } from 'notistack'

const useMessage = () => {
  const { enqueueSnackbar } = useSnackbar()

  const addMsgSuccess = useCallback(
    (message) => {
      enqueueSnackbar(message, { variant: 'success' })
    },
    [enqueueSnackbar],
  )

  const addMsgWarning = useCallback(
    (message) => {
      enqueueSnackbar(message, { variant: 'warning' })
    },
    [enqueueSnackbar],
  )

  const addMsgInfo = useCallback(
    (message) => {
      enqueueSnackbar(message, { variant: 'info' })
    },
    [enqueueSnackbar],
  )

  const addMsgError = useCallback(
    (error) => {
      const { message } = error || {}

      if (!message) {
        return
      }

      enqueueSnackbar(message, { variant: 'error' })
    },
    [enqueueSnackbar],
  )

  return {
    addMsgInfo,
    addMsgError,
    addMsgSuccess,
    addMsgWarning,
  }
}

export default useMessage
