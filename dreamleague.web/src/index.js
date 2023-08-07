import React from 'react'
import ReactDOM from 'react-dom'
import { Provider } from 'react-redux'
import { BrowserRouter } from 'react-router-dom'
import { ThemeProvider } from '@mui/material/styles'

import './index.css'
import { PersistGate } from 'redux-persist/integration/react'
import { store, persist } from 'security/store'
import App from './App'
import { customTheme } from './theme'

const theme = customTheme()

const Main = () => (
  <>
    <Provider store={store}>
      <PersistGate persistor={persist}>
        <ThemeProvider theme={theme}>
          <BrowserRouter>
            <App />
          </BrowserRouter>
        </ThemeProvider>
      </PersistGate>
    </Provider>
  </>
)

ReactDOM.render(<Main />, document.getElementById('root'))
