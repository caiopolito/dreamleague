import { useCallback } from 'react'
import axios from 'axios'
import useSecurity from 'security/useSecurity'

const useHistoryClient = () => {
  const { user } = useSecurity()

  const getMatches = useCallback(() => axios.get(`api/users/${user.steamId}/matches`), [user])

  const getDetailMatch = useCallback((matchId) => axios.get(`api/match/${matchId}/details`), [])

  return useCallback(
    () => ({
      getMatches,
      getDetailMatch,
    }),
    [
      getMatches,
      getDetailMatch,
    ],
  )
}

export default useHistoryClient
