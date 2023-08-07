import React, { useState } from 'react'
import PropTypes from 'prop-types'
import { useHistory } from 'react-router-dom'
import {
  Box,
  Menu,
  Drawer,
  Avatar,
  AppBar,
  Hidden,
  Divider,
  useTheme,
  MenuItem,
  Typography,
  IconButton,
} from '@mui/material'

import {
  Menu as MenuIcon,
  Close as CloseIcon,
} from '@mui/icons-material'
import { colors } from 'theme'
import useSecurity from 'security/useSecurity'
import { useSecurityAction } from 'security/store/ducks/security'

import logo from 'assets/header/logo.svg'

import useStyles from './style'
import Notifications from './Notifications'
import MenuItems from './MenuItems'
import ActionsMobile from './ActionsMobile'

const MainLayout = (props) => {
  const { children } = props

  const [anchorEl, setAnchorEl] = useState(null)
  const openMenu = Boolean(anchorEl)
  const [menuAnchor, setMenuAnchor] = useState(false)

  const theme = useTheme()
  const history = useHistory()
  const classes = useStyles()
  const { user } = useSecurity()
  const { cleanUser } = useSecurityAction()

  const handleOpenMenu = (event) => setAnchorEl(event.currentTarget)

  const handleCloseMenu = () => setAnchorEl(null)

  const handleSeeSteamProfile = () => {
    window.open(user?.profileUrl, '_blank', 'noopener,noreferrer')
    setAnchorEl(null)
  }

  const handleSeeProfile = () => {
    history.push(`/profile/${user?.steamId}`)
    setAnchorEl(null)
  }

  const handleLogout = () => {
    cleanUser()
    history.push('/')
  }

  const toggleDrawer = (open) => setMenuAnchor(open)

  return (
    <>
      <Box px={4} className={classes.container}>
        <AppBar position="fixed" className={classes.appBar}>
          <Box display="flex" alignItems="center" justifyContent="space-between">
            <Box display="flex" alignItems="center" height={theme.sizes.header}>
              <Box display="flex" p={3}>
                <Hidden lgDown>
                  <Box display="flex" alignItems="center" mx={1} onClick={() => history.push('/lobby')} style={{ cursor: 'pointer' }}>
                    <Typography fontWeight="bold" variant="h5">
                      DreamLeague
                    </Typography>
                  </Box>
                </Hidden>

                <Box>
                  <img src={logo} width={50} alt="DreamLeague" />
                </Box>
              </Box>

              <Hidden lgDown>
                <Box className={classes.nav}>
                  <MenuItems classes={classes} isMobile={false} />
                </Box>
              </Hidden>
            </Box>

            <Box>
              <Box display="flex" alignItems="center" px={3} height={theme.sizes.header}>
                <Notifications />

                <Divider orientation="vertical" variant="middle" flexItem className={classes.divider} />

                <Hidden lgUp>
                  <Box>
                    <IconButton onClick={() => toggleDrawer(true)}>
                      <MenuIcon htmlColor={colors.secondary.main} />
                    </IconButton>
                  </Box>
                </Hidden>

                <Drawer
                  anchor="top"
                  open={menuAnchor}
                  className={classes.menuMobile}
                  onClose={() => toggleDrawer(false)}
                >
                  <Box display="flex" position="relative" justifyContent="center">
                    <Box display="flex" mr={1} alignItems="center" onClick={() => history.push('/lobby')} style={{ cursor: 'pointer' }}>
                      <Typography fontWeight="bold" variant="subtitle1">
                        DreamLeague
                      </Typography>
                    </Box>

                    <Box>
                      <img src={logo} width={40} alt="DreamLeague" />
                    </Box>

                    <Box position="absolute" right={0}>
                      <IconButton onClick={() => toggleDrawer(false)}>
                        <CloseIcon htmlColor={colors.secondary.main} />
                      </IconButton>
                    </Box>
                  </Box>

                  <MenuItems classes={classes} isMobile onSelectItem={toggleDrawer} />

                  <ActionsMobile
                    handleLogout={handleLogout}
                    handleSeeProfile={handleSeeProfile}
                    handleSeeSteamProfile={handleSeeSteamProfile}
                    onSelectItem={toggleDrawer}
                  />
                </Drawer>

                <Hidden lgDown>
                  <Box mx={2} onClick={handleOpenMenu} sx={{ cursor: 'pointer' }}>
                    <Avatar
                      src={user?.avatar}
                      alt="Profile"
                      className={classes.avatar}
                      sx={{ width: theme.sizes.avatar, height: theme.sizes.avatar }}
                    />
                  </Box>

                  <Box>
                    <Box>
                      <Typography color="text">
                        {user?.name}
                      </Typography>
                    </Box>

                    <Typography color="secondary">
                      {user?.rank}
                    </Typography>
                  </Box>
                </Hidden>

                <Menu
                  open={openMenu}
                  id="profile-menu"
                  anchorEl={anchorEl}
                  onClose={handleCloseMenu}
                  transformOrigin={{ horizontal: 'right', vertical: 'top' }}
                  anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
                  MenuListProps={{ 'aria-labelledby': 'profile-button' }}
                >
                  <MenuItem onClick={handleSeeProfile}>
                    <Box width={130} textAlign="center">
                      Perfil
                    </Box>
                  </MenuItem>

                  <MenuItem onClick={handleSeeSteamProfile}>
                    <Box width={130} textAlign="center">
                      Perfil Steam
                    </Box>
                  </MenuItem>

                  <MenuItem onClick={handleLogout}>
                    <Box width={130} textAlign="center">
                      Logout
                    </Box>
                  </MenuItem>
                </Menu>
              </Box>
            </Box>
          </Box>
        </AppBar>

        <Box display="flex" width="100%" mt={11.2}>
          <Box width={1}>
            {children}
          </Box>
        </Box>
      </Box>

      <Box textAlign="center" className={classes.footer} component="footer">
        <Typography
          variant="caption"
          fontSize={10}
          lineHeight={0}
          fontWeight={200}
        >
          DreamLeague. Brazil 2022 @
        </Typography>
      </Box>
    </>
  )
}

MainLayout.propTypes = {
  children: PropTypes.element.isRequired,
}

export default MainLayout
