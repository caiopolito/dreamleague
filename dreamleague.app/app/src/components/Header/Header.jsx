import React from 'react'
import { NavLink } from 'react-router-dom'
import {
  Box,
  List,
  Avatar,
  AppBar,
  Divider,
  ListItem,
  useTheme,
  Typography,
  ListItemIcon,
} from '@mui/material'
import SVG from 'react-inlinesvg'

import { MENUS } from 'constants/defaults'
import { colors } from 'theme'
import useSecurity from 'security/useSecurity'

import logo from 'assets/header/logo.svg'
// import profile from 'assets/header/profile.png'
import icoMessage from 'assets/header/ico-message.svg'
import icoNotifications from 'assets/header/ico-notifications.svg'

import useStyles from './style'

const Header = () => {
  const theme = useTheme()
  const classes = useStyles()
  const { getUser } = useSecurity()
  const user = getUser()

  return (
    <>
      <Box px={4} className={classes.container}>
        <AppBar position="fixed" className={classes.appBar}>
          <Box display="flex" alignItems="center" justifyContent="space-between">
            <Box display="flex" alignItems="center" height={theme.sizes.header}>
              <Box display="flex" p={3}>
                <Box display="flex" alignItems="center" mr={1}>
                  <Typography fontWeight={100} variant="h5">
                    DreamLeague
                  </Typography>
                </Box>

                <Box>
                  <img src={logo} width={50} alt="DreamLeague" />
                </Box>
              </Box>

              <Box className={classes.nav}>
                <List>
                  {MENUS.map((menu, index) => (
                    <React.Fragment key={index}>
                      <ListItem
                        button
                        to={menu.route}
                        component={NavLink}
                        key={`menu-${index}`}
                        activeClassName="active"
                        className={classes.navItem}
                      >
                        <ListItemIcon className={classes.navIcon}>
                          <SVG
                            width={24}
                            height={34}
                            src={menu.icon}
                            fill={colors.secondary.main}
                            stroke={colors.secondary.main}
                          />

                          <Typography color="secondary">
                            {menu.label}
                          </Typography>
                        </ListItemIcon>
                      </ListItem>

                      <Divider orientation="vertical" variant="middle" flexItem className={classes.divider} />
                    </React.Fragment>
                  ))}
                </List>
              </Box>
            </Box>

            <Box>
              <Box display="flex" alignItems="center" px={2} height={theme.sizes.header}>
                <Box mx={2}>
                  <SVG
                    width={20}
                    height={25}
                    src={icoMessage}
                    stroke={colors.secondary.main}
                    fill={colors.secondary.main}
                  />
                </Box>

                <Divider orientation="vertical" variant="middle" flexItem className={classes.divider} />

                <Box mx={2}>
                  <SVG
                    width={20}
                    height={25}
                    src={icoNotifications}
                    fill={colors.secondary.main}
                  />
                </Box>

                <Divider orientation="vertical" variant="middle" flexItem className={classes.divider} />

                <Box mx={2}>
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
              </Box>
            </Box>
          </Box>
        </AppBar>
      </Box>
    </>
  )
}

export default Header
