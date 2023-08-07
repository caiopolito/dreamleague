import { createStore } from 'redux'
import { persistStore } from 'redux-persist'

import reducer from './ducks'
/* eslint-disable no-underscore-dangle */
/* eslint-disable max-len */
const store = createStore(reducer, { security: 'security' }, window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__())
/* eslint-enable */
const persist = persistStore(store)

export { store, persist }
