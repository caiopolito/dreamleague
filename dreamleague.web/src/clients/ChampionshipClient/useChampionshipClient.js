import { useCallback } from 'react'
import axios from 'axios'

const useChampionshipClient = () => {
  const getChampionships = useCallback(() => axios.get('api/championships'), [])

  const getChampionshipTeams = useCallback((championshipId) => axios.get(`api/championships/${championshipId}/teams`), [])

  const createChampionship = useCallback((request) => axios.post('api/championships', request), [])

  const updateChampionship = useCallback((request) => axios.put('api/championships', request), [])

  const deleteChampionship = useCallback((championshipId) => axios.delete(`api/championships/${championshipId}`), [])

  const getChampionshipDetail = useCallback((championshipId) => axios.get(`api/championships/${championshipId}`), [])

  const registerTeam = useCallback((championshipId, teamId) => axios.post(`api/championships/${championshipId}/teams/${teamId}`), [])

  const removeTeam = useCallback(({ teamId, championshipId }) => axios.delete(`api/championships/${championshipId}/teams/${teamId}`), [])

  return useCallback(
    () => ({
      getChampionships,
      createChampionship,
      updateChampionship,
      deleteChampionship,
      getChampionshipDetail,
      registerTeam,
      getChampionshipTeams,
      removeTeam,
    }),
    [
      getChampionships,
      createChampionship,
      updateChampionship,
      deleteChampionship,
      getChampionshipDetail,
      registerTeam,
      getChampionshipTeams,
      removeTeam,
    ],
  )
}

export default useChampionshipClient
