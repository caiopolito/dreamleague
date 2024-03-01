import React from 'react'

import CirculeIcon from '@mui/icons-material/Brightness1'
import icoTrophy from 'assets/header/ico-trophy.svg'
import icoGun from 'assets/header/ico-gun.svg'
import icoFriends from 'assets/header/ico-friends.svg'
import icoTeams from 'assets/header/ico-shield.svg'
import icoStars from 'assets/header/ico-stars.svg'

const contants = {}

export const PHONE_MASKS = {
  auto: '(99) 9999-99999',
  phone: '(99) 9999-9999',
  cellphone: '(99) 99999-9999',
}

export const DEFAULT_CONFIG_CURRENCY = {
  locale: 'pt-BR',
  formats: {
    number: {
      BRL: {
        currency: 'BRL',
        style: 'currency',
        minimumFractionDigits: 2,
        maximumFractionDigits: 2,
      },
    },
  },
}

const POLICY_KEY_ACTUAL = 1
const POLICY_KEY_OVER = 2
const POLICY_KEY_ISSUED = 3

export const POLICY_STATUS_VIEW = {
  [POLICY_KEY_ACTUAL]: {
    label: 'Vigente',
    icon: () => (<CirculeIcon htmlColor="#4caf50" />),
  },
  [POLICY_KEY_OVER]: {
    label: 'Vencida',
    icon: () => (<CirculeIcon htmlColor="#f44336" />),
  },
  [POLICY_KEY_ISSUED]: {
    label: 'Emitida',
    icon: () => (<CirculeIcon htmlColor="#ffc107" />),
  },
}

export const MENUS = [
  {
    route: '/lobby',
    label: 'Lobby',
    subLabel: 'Lobby',
    icon: icoGun,
  },
  {
    route: '/friends',
    label: 'Amigos',
    subLabel: 'Amigos',
    icon: icoFriends,
  },
  {
    route: '/community',
    label: 'Comunidade',
    subLabel: 'Comunidade',
    icon: icoFriends,
  },
  {
    route: '/championships',
    label: 'Campeonatos',
    subLabel: 'Campeonatos',
    icon: icoTrophy,
  },
  {
    route: '/teams',
    label: 'Meus Times',
    subLabel: 'Meus Times',
    icon: icoTeams,
  },
  {
    route: '/history',
    label: 'Histórico',
    subLabel: 'Histórico',
    icon: icoStars,
  },
]

export const TIME_TO_RELOAD = 6000

export default contants
