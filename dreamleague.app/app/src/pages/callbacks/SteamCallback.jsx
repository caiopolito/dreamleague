import React, { useMemo, useCallback } from 'react'
import { Redirect } from 'react-router-dom'
import Steam from 'extensions/steam'
import { useSecurityAction } from 'security/store/ducks/security'
import Resolve from 'components/Resolve/Resolve'
import useClient from 'clients/Client/useClient'

const steam = new Steam()
const SteamCallback = () => {
  const steamResponse = steam.getSteamResponse()
  const steamId = steam.getSteamIdFromSteamResponse(steamResponse)
  const { setUser } = useSecurityAction()
  const appClient = useClient()

  const handleResolve = useMemo(
    () => ({
      appUser: () => new Promise((resolve, reject) => {
        appClient().getUser(steamId).then(
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
    [appClient],
  )

  const handleLoaded = useCallback((data, resolve) => {
    const { appUser } = data
    const user = appUser ?? {}

    const userSecurity = {
      ...user,
      ...appUser,
      steamId,
    }

    setUser(userSecurity)
    resolve()
  }, [setUser, steamId])

  return (
    <>
      <Resolve onLoaded={handleLoaded} resolve={handleResolve}>
        <Redirect to="/lobby" />
      </Resolve>
    </>
  )
}

export default SteamCallback
