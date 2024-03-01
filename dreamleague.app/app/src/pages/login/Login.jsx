import React from 'react'
import { makeStyles } from '@mui/styles'
import {
  Box, Grid, Button, Typography,
} from '@mui/material'
import SVG from 'react-inlinesvg'
import icoInvite from 'assets/ico-arrow.svg'
import { colors } from 'theme/index'
import Steam from 'extensions/steam'

const useStyles = makeStyles(() => ({
  button: {
    width: 230,
    height: '160px !important',
  },
  login: {
    border: `1px solid ${colors.login.login} !important`,
  },
}))

const steam = new Steam()

const Login = () => {
  const spanLayout = [0, 1, 2, 3]
  const classes = useStyles()

  return (
    <Box display="flex" alignItems="center" justifyContent="center" height={1}>
      <Box>
        <Grid container spacing={4}>
          <Grid item xs="auto">
            <Button href={steam.getSteamLoginUrl(`${process.env.REACT_APP_URL}/callback`)} variant="outlined" color="secondary" className={`${classes.button} ${classes.login} buttonLobby`}>
              {spanLayout.map((_, index) => (<span key={index} />))}
              <Box>
                <Box>
                  <SVG
                    width={60}
                    height={60}
                    src={icoInvite}
                    fill={colors.lobby.invite}
                    stroke={colors.lobby.invite}
                    color={colors.lobby.invite}
                  />
                </Box>
                <Box>
                  <Typography fontWeight={100} variant="h6">
                    Login with Steam
                  </Typography>
                </Box>
              </Box>
            </Button>
          </Grid>
        </Grid>
      </Box>
    </Box>
  )
}

export default Login
