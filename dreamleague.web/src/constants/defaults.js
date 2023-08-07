import React from 'react'

import CirculeIcon from '@mui/icons-material/Brightness1'
import icoTrophy from 'assets/header/ico-trophy.svg'
import icoGun from 'assets/header/ico-gun.svg'
import icoStars from 'assets/header/ico-stars.svg'
import icoFriends from 'assets/header/ico-friends.svg'

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

export const MENUS = [
  {
    route: '/lobby',
    label: 'Lobby',
    subLabel: 'Lobby',
    icon: icoGun,
  },
  {
    route: '/championship',
    label: 'Campeonatos',
    subLabel: 'Campeonatos',
    icon: icoTrophy,
  },
  {
    route: '/pontuation',
    label: 'Pontuação',
    subLabel: 'Pontuação',
    icon: icoStars,
  },
  {
    route: '/friends',
    label: 'Amigos',
    subLabel: 'Amigos',
    icon: icoFriends,
  },
]

export default contants
