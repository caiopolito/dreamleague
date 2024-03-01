import { useCallback } from 'react'
import axios from 'axios'
import useSecurity from 'security/useSecurity'

const useTeamClient = () => {
  const { user } = useSecurity()

  const getTeams = useCallback((isCaptain) => axios.get('api/teams', { headers: { steamid: user?.steamId, ...(!!isCaptain && { isCaptain }) } }), [user])

  const createTeam = useCallback((request) => axios.post('api/teams', request, { headers: { steamid: user?.steamId } }), [user])

  const updateTeam = useCallback((request) => axios.put('api/teams', request, { headers: { steamid: user?.steamId } }), [user])

  const deleteTeam = useCallback((teamId) => axios.delete(`api/teams/${teamId}`), [])

  const getTeamDetail = useCallback((teamId) => axios.get(`api/teams/${teamId}`), [])

  const getAllPlayers = useCallback(({
    name, rank = 'Unranked', isFriend = false, teamId = null,
  }) => {
    const params = new URLSearchParams({
      name,
      rank,
      isFriend,
      ...(!!teamId && { teamId }),
    })
    return axios.get(`api/users/all?${params}`, { headers: { steamid: user?.steamId } })
  }, [user])

  const inviteToTeam = useCallback((request) => axios.post(`api/teams/${request.teamId}/invite`, request), [])

  const removePlayer = useCallback(({ teamId, steamId }) => axios.delete(`api/teams/${teamId}/players/${steamId}`), [])

  return useCallback(
    () => ({
      getTeams,
      createTeam,
      updateTeam,
      deleteTeam,
      getTeamDetail,
      getAllPlayers,
      inviteToTeam,
      removePlayer,
    }),
    [
      getTeams,
      createTeam,
      updateTeam,
      deleteTeam,
      getTeamDetail,
      getAllPlayers,
      inviteToTeam,
      removePlayer,
    ],
  )
}

export default useTeamClient
