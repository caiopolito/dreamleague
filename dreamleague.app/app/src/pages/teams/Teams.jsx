import React, { useCallback, useMemo, useState } from 'react'
import { makeStyles } from '@mui/styles'
import {
  Box, Typography, Paper, Button, IconButton,
} from '@mui/material'
import ShieldIcon from '@mui/icons-material/LocalPolice'
import BrushIcon from '@mui/icons-material/Brush'
import ClearIcon from '@mui/icons-material/Clear'

import useSecurity from 'security/useSecurity'
import useTeamClient from 'clients/TeamClient/useTeamClient'
import Resolve from 'components/Resolve/Resolve'
import MessageBox from 'components/MessageBox/MessageBox'
import { colors } from 'theme'
import { useAlert } from 'components/Alert'
import { useHistory } from 'react-router-dom'
import NewTeamModal from './NewTeamModal'

const useStyles = makeStyles(() => ({
  paper: {
    minHeight: 300,
    color: '#fff !important',
    padding: 16,
    backgroundColor: '#0705204a !important',
  },
  title: {
    color: `${colors.secondary.main} !important`,
  },
  buttonIcon: {
    border: `1px solid ${colors.secondary.main} !important`,
    '&:hover': {
      backgroundColor: '#2AB27B!important',
    },
  },
  itemList: {
    display: 'flex',
    justifyContent: 'space-between',
    cursor: 'pointer',
    borderRadius: 4,
    textDecoration: 'none',
    '&:hover': {
      backgroundColor: 'rgba(1, 200, 208, 0.04)',
    },
  },
  itemGridAward: {
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
}))

const Teams = () => {
  const classes = useStyles()
  const { user } = useSecurity()
  const teamClient = useTeamClient()
  const history = useHistory()

  const { addMsgDanger, addMsgSuccess } = useAlert()

  const [teams, setTeams] = useState([])
  const [openNew, setOpenNew] = useState(false)
  const [teamObject, setTeamObject] = useState(null)

  const [openDelete, setOpenDelete] = useState(false)
  const [teamToDelete, setTeamToDelete] = useState({})

  const handleResolve = useMemo(
    () => ({
      teams: () => new Promise((resolve, reject) => {
        teamClient().getTeams().then(
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
    [teamClient, openNew, openDelete],
  )

  const handleLoaded = useCallback((data, resolve) => {
    const { teams: teamsResponse } = data

    setTeams(teamsResponse?.teams ?? [])
    resolve()
  }, [setTeams])

  const handleCloseDelete = () => {
    setTeamToDelete({})
    setOpenDelete(false)
  }

  const confirmDeleteTeam = () => {
    teamClient().deleteTeam(teamToDelete?.id).then(
      () => {
        addMsgSuccess('Time deletado com sucesso!')
      },
      () => {
        addMsgDanger('Erro ao deletar time!')
      },
    ).then(() => {
      setOpenDelete(false)
    })
  }

  return (
    <Box>
      <Box textAlign="center" mb={2} mt={1}>
        <Typography fontWeight="bold" variant="h5">
          Times:
        </Typography>
      </Box>

      <Paper className={classes.paper}>
        <Box mb={2}>
          <Box display="flex" justifyContent="flex-end">
            <Button
              variant="contained"
              color="primary"
              onClick={() => {
                setTeamObject(null)
                setOpenNew(true)
              }}
            >
              Criar time
            </Button>
          </Box>

          <Resolve onLoaded={handleLoaded} resolve={handleResolve}>
            <Box mt={2}>
              {teams?.map((item, index) => (
                <Box
                  className={classes.itemList}
                  key={index}
                >
                  <Box
                    px={1}
                    my={2}
                    display="flex"
                    width="100%"
                    alignItems="center"
                    justifyContent="space-between"
                    onClick={() => history.push(`/team/${item.id}`)}
                  >
                    <Box className={classes.itemGridAward}>
                      <Box>
                        <ShieldIcon fontSize="large" htmlColor="#d59d13" />
                      </Box>
                      <Box mx={1} />
                      <Box>
                        <Typography variant="h5" className={classes.title}>
                          <b>{item?.name}</b>
                        </Typography>
                      </Box>
                    </Box>
                  </Box>

                  <Box display="flex" justifyContent="flex-end" alignItems="center">
                    {item.players.some((x) => (x.steamId === user.steamId) && x.isCaptain) && (
                      <Box display="flex" justifyContent="flex-end" alignItems="center" mr={1}>
                        <Box>
                          <IconButton
                            className={classes.buttonIcon}
                            onClick={() => {
                              setTeamObject(item?.id)
                              setOpenNew(true)
                            }}
                          >
                            <BrushIcon htmlColor={colors.lobby.invite} />
                          </IconButton>
                        </Box>

                        <Box mx={1} />

                        <Box>
                          <IconButton
                            className={classes.buttonIcon}
                            onClick={() => {
                              setTeamToDelete(item)
                              setOpenDelete(true)
                            }}
                          >
                            <ClearIcon htmlColor={colors.lobby.close} />
                          </IconButton>
                        </Box>
                      </Box>
                    )}
                  </Box>
                </Box>
              ))}
            </Box>
          </Resolve>

          {openDelete && (
            <MessageBox
              opened={openDelete}
              handleClose={handleCloseDelete}
              title="Excluir Time"
              text={`Deseja realmente excluir o time “${teamToDelete?.name}”?`}
              handleSecondary={handleCloseDelete}
              handlePrimary={confirmDeleteTeam}
            />
          )}

          {openNew && (
            <NewTeamModal
              open={openNew}
              setOpen={setOpenNew}
              teamObject={teamObject}
            />
          )}
        </Box>
      </Paper>
    </Box>
  )
}

export default Teams
