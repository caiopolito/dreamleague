import React, { useState, useEffect } from 'react'
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'
import { makeStyles } from '@mui/styles'
import {
  Box, Grid, Button, Typography, Paper,
} from '@mui/material'
import SVG from 'react-inlinesvg'
import { useHistory } from 'react-router-dom'

import './button.scss'
import icoCalibur from 'assets/ico-calibur.svg'
import { colors } from 'theme/index'
import useSecurity from 'security/useSecurity'
import Friends from './Friends'
import QueueModal from './QueueModal'

import { useLobbyContext } from './LobbyContext'

const useStyles = makeStyles(() => ({
  button: {
    height: 'auto !important',
    color: '#fff !important',
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
  paper: {
    minHeight: 300,
    color: '#fff !important',
    padding: 16,
    backgroundColor: '#0705204a !important',
  },
}))

const Lobby = () => {
  const [openQueue, setOpenQueue] = useState(false)

  const [connection, setConnection] = useState(null)

  const { user } = useSecurity()

  const classes = useStyles()
  const spanLayout = [0, 1, 2, 3]
  const history = useHistory()
  const {
    queue, setQueue, matchStarted, setMatchStarted, ipAddress, setIpAddress,
  } = useLobbyContext()

  useEffect(() => {
    async function openConnection() {
      const newConnection = new HubConnectionBuilder()
        .withUrl(`${process.env.REACT_APP_URL_HUB}api/queue`)
        .configureLogging(LogLevel.Information)
        .build()

      newConnection.onclose(() => {
        setConnection()
      })

      newConnection.on('MatchGoingOn', (ip) => {
        setIpAddress(ip)
        setMatchStarted(true)
        setOpenQueue(true)
      })

      await newConnection.start()
      setConnection(newConnection)
      await newConnection.invoke('OnConnectAsync', user?.steamId)
    }
    openConnection()
  }, [setConnection, setMatchStarted, user, setIpAddress])

  return (
    <>
      <Box display="flex" alignItems="center" justifyContent="center">
        <Box py={3} width={1}>
          <Grid container spacing={4} justifyContent="center">
            <Grid item xs={12} md={4}>
              <Paper className={classes.paper}>
                <Box textAlign="center" mb={2}>
                  <Typography fontWeight="bold" variant="h5">
                    Jogar agora!
                  </Typography>
                </Box>

                <Box mb={2}>
                  <Button
                    fullWidth
                    color="primary"
                    variant="outlined"
                    onClick={() => setOpenQueue(true)}
                    className={`${classes.button} ${classes.create} buttonLobby`}
                  >
                    {spanLayout.map((_, index) => (<span key={index} />))}

                    <Box p={1} display="flex" alignItems="center">
                      <Box mr={1}>
                        <SVG
                          width={30}
                          height={30}
                          src={icoCalibur}
                          stroke={colors.lobby.create}
                        />
                      </Box>

                      Encontrar partida
                    </Box>
                  </Button>
                </Box>
              </Paper>
            </Grid>

            <Grid item xs={12} md={4}>
              <Paper className={classes.paper}>
                <Box textAlign="center" mb={2}>
                  <Typography fontWeight="bold" variant="h5">
                    Amigos:
                  </Typography>
                </Box>

                <Box mb={2}>
                  <Friends isPanel />
                </Box>

                <Box>
                  <Button color="secondary" fullWidth variant="text" onClick={() => history.push('/friends')}>
                    Ver todos...
                  </Button>
                </Box>
              </Paper>
            </Grid>
          </Grid>
        </Box>

        {openQueue && (
          <QueueModal
            open={openQueue}
            queue={queue}
            setOpenQueue={setOpenQueue}
            setQueue={setQueue}
            connection={connection}
            matchStarted={matchStarted}
            setMatchStarted={setMatchStarted}
            ipAddress={ipAddress}
            setIpAddress={setIpAddress}
          />
        )}
      </Box>
    </>
  )
}

export default Lobby
