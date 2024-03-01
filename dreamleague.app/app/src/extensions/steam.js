class Steam {
  /**
 * Gets the login URL for Steam.
 * @param returnUrl The URL that Steam redirects to after authentication.
 * @returns The URL to the Steam login page.
 */
  getSteamLoginUrl = (returnUrl) => {
    const formattedReturnUrl = returnUrl.replace(/(?<!:)\/+/gm, '/')
    return `https://steamcommunity.com/openid/login?${new URLSearchParams({
      'openid.return_to': new URL(formattedReturnUrl),
      'openid.realm': new URL(formattedReturnUrl).origin,
      'openid.ns': 'http://specs.openid.net/auth/2.0',
      'openid.mode': 'checkid_setup',
      'openid.identity': 'http://specs.openid.net/auth/2.0/identifier_select',
      'openid.claimed_id': 'http://specs.openid.net/auth/2.0/identifier_select',
    })}`
  }

  /**
 * Gets the 64-bit Steam ID of the user. This is not validated by Steam, it's simply extracted
 * from the response. Useful for SPAs that don't need to perform validation.
 * Use `validateSteamLogin` if you'd like to perform validation.
 * @param response The response from Steam.
 * @returns The 64-bit Steam ID of the user.
 */
  getSteamIdFromSteamResponse = (steamResponse) => steamResponse['openid.claimed_id'].match(/\d+/)[0]

  /**
 * Gets the response from Steam. Useful for SPAs.
 * @returns The response from Steam.
 */
  /* eslint-disable max-len */
  getSteamResponse = () => Object.fromEntries(new URLSearchParams(window.location.search).entries())
}

export default Steam
