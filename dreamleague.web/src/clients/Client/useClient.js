import { useCallback } from 'react'
import axios from 'axios'

const useClient = () => {
  const getUser = useCallback((steamId) => axios.get(`${process.env.REACT_APP_URL_API}api/user/${steamId}`), [])

  return useCallback(
    () => ({
      getUser,
    }),
    [getUser],
  )
}

export default useClient
