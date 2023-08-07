import React, { useMemo, useCallback } from 'react'
import { Redirect } from 'react-router-dom'
import Steam from 'extensions/steam'
import { useSecurityAction } from 'security/store/ducks/security'
import Resolve from 'components/Resolve/Resolve'
import useClient from '../../clients/Client/useClient'

const steam = new Steam()
const SteamCallback = () => {
  const steamResponse = steam.getSteamResponse()
  const steamId = steam.getSteamIdFromSteamResponse(steamResponse)
  const { setUser } = useSecurityAction()
  const client = useClient()

  const handleResolve = useMemo(
    () => ({
      user: () => new Promise((resolve, reject) => {
        client().getUser(steamId).then(
          (response) => {
            resolve(response.data)
          },
          () => {
            reject()
          },
        )
      }),
    }),
    // eslint-disable-next-line react-hooks/exhaustive-deps
    [client],
  )

  const handleLoaded = useCallback((data, resolve) => {
    const { user } = data
    user.expires_at = Math.round(Date.now() / 1000 + 30)

    setUser(user)
    resolve()
  }, [setUser])

  return (
    <>
      <Resolve
        onLoaded={handleLoaded}
        resolve={handleResolve}
      >
        <Redirect to="/lobby" />
      </Resolve>
    </>
  )
}

export default SteamCallback
