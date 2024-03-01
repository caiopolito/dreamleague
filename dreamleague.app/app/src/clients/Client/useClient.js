import { useCallback } from 'react'
import axios from 'axios'

const useClient = () => {
  const getUser = useCallback((steamId) => axios.get(`api/users/${steamId}`), [])

  const getUsers = useCallback((steamIds) => {
    const params = new URLSearchParams()
    steamIds.forEach((x) => params.append('steamIds', x))
    return axios.get(`api/users?${params}`)
  }, [])

  const checkIfHasTeam = useCallback((steamId) => axios.get(`api/users/${steamId}/team`), [])

  const getUserFriends = useCallback((steamId) => axios.get(`api/users/${steamId}/friends`), [])

  return useCallback(
    () => ({
      getUser,
      getUsers,
      getUserFriends,
      checkIfHasTeam,
    }),
    [
      getUser,
      getUsers,
      getUserFriends,
      checkIfHasTeam,
    ],
  )
}

export default useClient
