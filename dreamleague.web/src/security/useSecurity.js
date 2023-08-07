import { useCallback } from 'react'
import { useSelector } from 'react-redux'

const useSecurity = () => {
  const { user } = useSelector(
    ({ security }) => ({
      user: security.user,
    }),
  )

  const getUser = useCallback(() => user, [user])

  return {
    user,
    getUser,
  }
}

export default useSecurity
