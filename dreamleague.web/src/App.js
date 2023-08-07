import React, { useMemo, useState } from 'react'
import { createBrowserHistory } from 'history'
import { ThemeProvider } from '@mui/material/styles'

import './helpers/yup'
import Loader from 'components/Loader/Loader'
import AxiosSetting from 'helpers/axios'
import AlertEmiter from 'components/Alert/Alert'
// import AlertEmiter, { useAlert } from 'components/Alert/Alert'
// import userClient from 'clients/Client'
// import Resolve from 'components/Resolve/Resolve'
import { customTheme } from './theme'
import Routes from './Routes'

const theme = customTheme()
const history = createBrowserHistory()

const App = () => {
  const [loading, setLoading] = useState(false)
  // const { addMsgDanger } = useAlert()

  const handleError = useMemo(
    () => ({
      403: () => history.replace('/'),
    }),
    [],
  )

  // const client = userClient()

  // const handleResolve = useMemo(
  //   () => ({
  //     user: () => new Promise((resolve, reject) => {
  //       client().getUser().then(
  //         (response) => {
  //           resolve(response.data)
  //         },
  //         (response) => {
  //           reject()
  //           addMsgDanger(response.data)
  //         },
  //       )
  //     }),
  //   }),
  //   // eslint-disable-next-line react-hooks/exhaustive-deps
  //   [client],
  // )

  // const handleLoaded = useCallback((data, resolve) => {
  //   const { user } = data

  //   setUser(user)
  //   resolve()
  // }, [setUser])

  return (
    <>
      <AxiosSetting
        handleError={handleError}
        onStartRequest={() => setLoading(true)}
        onStopRequest={() => setLoading(false)}
      />

      <ThemeProvider theme={theme}>
        <AlertEmiter />

        <Loader show={loading} />

        {/* <Resolve
          onLoaded={handleLoaded}
          resolve={handleResolve}
        > */}

        <Routes />

        {/* </Resolve> */}
      </ThemeProvider>
    </>
  )
}

export default App
