import React from 'react'
import { makeStyles } from '@mui/styles'
import {
  Box, Grid, Button, Typography,
} from '@mui/material'
import SVG from 'react-inlinesvg'

import './button.scss'
import icoCalibur from 'assets/ico-calibur.svg'
import icoMolotov from 'assets/ico-molotov.svg'
import icoInvite from 'assets/ico-arrow.svg'

import { colors } from 'theme/index'

const useStyles = makeStyles(() => ({
  button: {
    width: 230,
    height: '160px !important',
  },
  create: {
    border: `1px solid ${colors.lobby.create} !important`,
  },
  enter: {
    border: `1px solid ${colors.lobby.enter} !important`,
  },
  invite: {
    border: `1px solid ${colors.lobby.invite} !important`,
  },
}))

const Lobby = () => {
  const classes = useStyles()
  const spanLayout = [0, 1, 2, 3]

  return (
    <Box display="flex" alignItems="center" justifyContent="center" height={1}>
      <Box>
        <Grid container spacing={4}>
          <Grid item xs={4}>
            <Button variant="outlined" color="secondary" className={`${classes.button} ${classes.create} buttonLobby`}>
              {spanLayout.map((_, index) => (<span key={index} />))}

              <Box>
                <Box>
                  <SVG
                    width={60}
                    height={60}
                    src={icoCalibur}
                    stroke={colors.lobby.create}
                  />
                </Box>

                <Box>
                  <Typography fontWeight={100} variant="h6">
                    Criar partida
                  </Typography>
                </Box>
              </Box>
            </Button>
          </Grid>

          <Grid item xs={4}>
            <Button variant="outlined" color="secondary" className={`${classes.button} ${classes.enter} buttonLobby`}>
              {spanLayout.map((_, index) => (<span key={index} />))}

              <Box>
                <Box>
                  <SVG
                    width={60}
                    height={60}
                    src={icoMolotov}
                    stroke={colors.lobby.enter}
                  />
                </Box>

                <Box>
                  <Typography fontWeight={100} variant="h6">
                    Entrar numa partida
                  </Typography>
                </Box>
              </Box>
            </Button>
          </Grid>

          <Grid item xs={4}>
            <Button variant="outlined" color="secondary" className={`${classes.button} ${classes.invite} buttonLobby`}>
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
                    Convidar
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

export default Lobby
