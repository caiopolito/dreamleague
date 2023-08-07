import { useCallback } from 'react'
import produce from 'immer'
import { useDispatch } from 'react-redux'
import { persistReducer } from 'redux-persist'
import storage from 'redux-persist/lib/storage'

const INITIAL_STATE = {
  user: {},
}

export const Types = {
  USER: 'security/USER',
  USER_STEAM_ID: 'security/USER_STEAM_ID',
}

const reducer = (state = INITIAL_STATE, action) => {
  switch (action.type) {
    case Types.USER: {
      const { user } = action.payload

      return produce(state, (draft) => {
        draft.user = user
      })
    }

    case Types.USER_STEAM_ID: {
      const { steamid } = action.payload

      return produce(state, (draft) => {
        draft.steamid = steamid
      })
    }

    default: {
      return state
    }
  }
}

export const setUserStore = (user) => ({
  type: Types.USER,
  payload: { user },
})

export const useSecurityAction = () => {
  const dispatch = useDispatch()
  const setUser = useCallback(
    (user) => dispatch({
      type: Types.USER,
      payload: { user },
    }),
    [dispatch],
  )

  const setUserSteamId = useCallback(
    (steamid) => dispatch({
      type: Types.USER_STEAM_ID,
      payload: { steamid },
    }),
    [dispatch],
  )

  const cleanUser = useCallback(
    () => dispatch({
      type: Types.USER,
      payload: { user: {} },
    }),
    [dispatch],
  )

  return {
    setUser,
    setUserSteamId,
    cleanUser,
  }
}

export default persistReducer(
  {
    key: 'security',
    storage,
  },
  reducer,
)
