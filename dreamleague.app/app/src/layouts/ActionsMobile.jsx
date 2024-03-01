import React from 'react'
import PropTypes from 'prop-types'

import {
  Box,
  Typography,
  IconButton,
} from '@mui/material'
import SVG from 'react-inlinesvg'
import {
  PermContactCalendar as ProfileIcon,
  Logout as LogoutIcon,
} from '@mui/icons-material'

import icoSteam from 'assets/ico/ico-steam.svg'
import { colors } from 'theme'

const ActionsMobile = ({
  onSelectItem,
  handleLogout,
  handleSeeProfile,
  handleSeeSteamProfile,
}) => (
  <Box display="flex" justifyContent="center">
    <IconButton onClick={() => {
      onSelectItem()
      handleSeeProfile()
    }}
    >
      <Box
        display="flex"
        alignItems="center"
        flexDirection="column"
        justifyContent="center"
      >
        <ProfileIcon htmlColor={colors.lobby.invite} />

        <Box my={0.5} />

        <Typography fontWeight="bold" variant="body2">
          Perfil
        </Typography>
      </Box>
    </IconButton>

    <Box mx={2} />

    <IconButton
      onClick={() => {
        onSelectItem()
        handleSeeSteamProfile()
      }}
    >
      <Box
        display="flex"
        alignItems="center"
        flexDirection="column"
        justifyContent="center"
      >
        <SVG
          width={25}
          height={25}
          src={icoSteam}
          title="Ver Perfil Steam"
          fill={colors.secondary.main}
        />

        <Box my={0.5} />

        <Typography fontWeight="bold" variant="body2">
          Perfil Steam
        </Typography>
      </Box>
    </IconButton>

    <Box mx={2} />

    <IconButton
      onClick={() => {
        onSelectItem()
        handleLogout()
      }}
    >
      <Box
        display="flex"
        alignItems="center"
        flexDirection="column"
        justifyContent="center"
      >
        <LogoutIcon htmlColor={colors.lobby.close} />

        <Box my={0.5} />

        <Typography fontWeight="bold" variant="body2">
          Logout
        </Typography>
      </Box>
    </IconButton>
  </Box>
)

ActionsMobile.propTypes = {
  handleSeeProfile: PropTypes.func.isRequired,
  handleSeeSteamProfile: PropTypes.func.isRequired,
  handleLogout: PropTypes.func.isRequired,
  onSelectItem: PropTypes.func.isRequired,
}

export default ActionsMobile
