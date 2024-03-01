import React, { useCallback, useMemo, useState } from 'react'
import { makeStyles } from '@mui/styles'
import {
  Box, Typography, Paper, Avatar,
} from '@mui/material'

import useTeamClient from 'clients/TeamClient/useTeamClient'
import Resolve from 'components/Resolve/Resolve'

import { useHistory } from 'react-router-dom'

const useStyles = makeStyles((theme) => ({
  paper: {
    minHeight: 300,
    color: '#fff !important',
    padding: 16,
    backgroundColor: '#0705204a !important',
  },
  itemList: {
    transition: `all .3s ${theme.transitions.easing.easeInOut}`,
    borderRadius: 4,
    cursor: 'pointer',
    textDecoration: 'none',
    '&:hover': {
      backgroundColor: 'rgba(1, 200, 208, 0.04)',
    },
    '& img': {
      width: 50,
      borderRadius: '50%',
    },
  },
}))

const Community = () => {
  const classes = useStyles()
  const teamClient = useTeamClient()
  const history = useHistory()

  const [players, setPlayers] = useState([])

  const handleResolve = useMemo(
    () => ({
      players: () => new Promise((resolve, reject) => {
        teamClient().getAllPlayers({ name: '' }).then(
          (response) => {
            resolve(response.data)
          },
          () => {
            reject()
          },
        )
      }),
    }),
    // eslint-disable-next-line react-hooks/exhaustive-deps
    [teamClient],
  )

  const handleLoaded = useCallback((data, resolve) => {
    setPlayers(data.players.players ?? [])
    resolve()
  }, [setPlayers])

  return (
    <Box>
      <Box textAlign="center" mb={2} mt={1}>
        <Typography fontWeight="bold" variant="h5">
          Jogadores DREAMLEAGUE:
        </Typography>
      </Box>

      <Paper className={classes.paper}>
        <Box mb={2}>
          <Resolve onLoaded={handleLoaded} resolve={handleResolve}>
            <>
              {players?.map((item, index) => (
                <React.Fragment key={index}>
                  <Box
                    display="flex"
                    justifyContent="space-between"
                    className={classes.itemList}
                  >
                    <Box
                      px={1}
                      my={1}
                      display="flex"
                      alignItems="center"
                      width="100%"
                      onClick={() => history.push(`/profile/${item?.steamId}`)}
                    >
                      <Box display="flex" alignItems="center" justifyContent="center">
                        <Box display="flex" alignItems="center">
                          <Box mr={2}>
                            <Avatar
                              src={item.avatar}
                              alt="Avatar"
                              className={classes.avatar}
                              sx={{}}
                            />
                          </Box>

                          <Box mr={2}>
                            <Typography fontWeight={100} variant="body2">
                              {item?.name}
                            </Typography>
                          </Box>
                        </Box>
                      </Box>
                    </Box>
                  </Box>
                </React.Fragment>
              ))}
            </>
          </Resolve>
        </Box>
      </Paper>
    </Box>
  )
}

export default Community
