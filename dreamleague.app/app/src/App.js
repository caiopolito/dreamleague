import React, { useMemo, useState } from 'react'
import { createBrowserHistory } from 'history'
import { Box, ThemeProvider } from '@mui/material'

import './helpers/yup'
import Loader from 'components/Loader/Loader'
import AxiosSetting from 'helpers/axios'
import AlertEmiter from 'components/Alert/Alert'
import Chat from 'pages/home/Chat/Chat'
import customTheme from 'theme'
import Routes from './Routes'
import LobbyContext from './pages/home/LobbyContext'

const history = createBrowserHistory()

const App = () => {
  const { Provider } = LobbyContext

  const [loading, setLoading] = useState(false)
  const [queue, setQueue] = useState(0)
  const [chat, setChat] = useState(null)
  const [matchStarted, setMatchStarted] = useState(false)
  const [ipAddress, setIpAddress] = useState('')
  const theme = customTheme()

  const handleError = useMemo(
    () => ({
      403: () => history.replace('/'),
    }),
    [],
  )
  return (
    <>
      <ThemeProvider theme={theme}>
        <AxiosSetting
          handleError={handleError}
          onStartRequest={() => setLoading(true)}
          onStopRequest={() => setLoading(false)}
        />

        <AlertEmiter />
        <Loader show={loading} />

        <Provider
          value={{
            queue,
            setQueue,
            chat,
            setChat,
            matchStarted,
            setMatchStarted,
            ipAddress,
            setIpAddress,
          }}
        >
          <Routes />

          {chat && chat?.show && (
            <Box position="fixed" zIndex={1} bottom={20} right={20} width={400}>
              <Chat />
            </Box>
          )}
        </Provider>
      </ThemeProvider>
    </>
  )
}

export default App
