import { useCallback } from 'react'
import { useSelector } from 'react-redux'

const useSecurity = () => {
  const { user } = useSelector(
    ({ security }) => ({
      user: security.user,
    }),
  )

  const isLogged = useCallback(() => {
    let loggedIn = false
    if (user) {
      const currentAt = Math.round(Date.now() / 1000)
      loggedIn = user.expires_at > currentAt
    }
    return loggedIn
  }, [user])

  const getUser = useCallback(() => user, [user])

  return {
    user,
    getUser,
    isLogged,
  }
}

export default useSecurity
