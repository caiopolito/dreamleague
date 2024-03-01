import React, { useContext } from 'react'

const LobbyContext = React.createContext()

export const useLobbyContext = () => {
  const {
    queue,
    setQueue,
    chat,
    setChat,
    matchStarted,
    setMatchStarted,
    ipAddress,
    setIpAddress,
  } = useContext(LobbyContext)

  return {
    queue,
    setQueue,
    chat,
    setChat,
    matchStarted,
    setMatchStarted,
    ipAddress,
    setIpAddress,
  }
}

export default LobbyContext
