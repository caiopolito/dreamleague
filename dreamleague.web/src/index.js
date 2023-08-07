import React from 'react'
import ReactDOM from 'react-dom'
import { Provider } from 'react-redux'
import { BrowserRouter } from 'react-router-dom'

import './index.css'
import { PersistGate } from 'redux-persist/integration/react'
import { store, persist } from 'security/store'
import App from './App'

const Main = () => (
  <>
    <Provider store={store}>
      <PersistGate persistor={persist}>
        <BrowserRouter>
          <App />
        </BrowserRouter>
      </PersistGate>
    </Provider>
  </>
)

ReactDOM.render(<Main />, document.getElementById('root'))
