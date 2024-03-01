import React, { useMemo, useCallback, useState } from 'react'
import Resolve from 'components/Resolve/Resolve'
import { makeStyles } from '@mui/styles'
import {
  Box, Button, Typography, Paper, Avatar, useTheme, IconButton,
} from '@mui/material'
import ClearIcon from '@mui/icons-material/Clear'
import SVG from 'react-inlinesvg'

import icoArrow from 'assets/ico-arrow.svg'
import { colors } from 'theme/index'
import useTeamClient from 'clients/TeamClient/useTeamClient'
import { useParams, useHistory } from 'react-router-dom'
import useSecurity from 'security/useSecurity'
import ArrowBackIcon from '@mui/icons-material/ArrowBack'
import GradeIcon from '@mui/icons-material/Grade'
import MessageBox from 'components/MessageBox/MessageBox'
import { useAlert } from 'components/Alert'
import InviteModal from './InviteModal'

const useStyles = makeStyles(() => ({
  button: {
    height: 'auto !important',
    color: '#fff !important',
  },
  buttonIcon: {
    border: `1px solid ${colors.secondary.main} !important`,
    '&:hover': {
      backgroundColor: '#2AB27B!important',
    },
  },
  favIcon: {
    border: `1px solid ${colors.secondary.main} !important`,
    cursor: 'unset !important',
  },
  create: {
    border: `1px solid ${colors.lobby.create} !important`,
  },
  paper: {
    minHeight: 300,
    color: '#fff !important',
    padding: 16,
    backgroundColor: '#0705204a !important',
  },
  paperHeader: {
    display: 'grid',
    alignItems: 'center',
    gridTemplateColumns: '25px 1fr 25px',
  },
  arrowBackIcon: {
    cursor: 'pointer',
    '&:hover': {
      transition: 'all ease-in-out .2s',
      color: '#2AB27B',
    },
  },
}))

const TeamDetail = () => {
  const { id } = useParams()
  const { user } = useSecurity()
  const theme = useTheme()
  const classes = useStyles()
  const spanLayout = [0, 1, 2, 3]
  const teamClient = useTeamClient()
  const [team, setTeam] = useState([])
  const [openInvite, setOpenInvite] = useState(false)
  const history = useHistory()
  const [openDelete, setOpenDelete] = useState(false)
  const [playerToDelete, setPlayerToDelete] = useState({})
  const { addMsgSuccess, addMsgDanger } = useAlert()

  const handleResolve = useMemo(
    () => ({
      t: () => new Promise((resolve, reject) => {
        teamClient().getTeamDetail(id).then(
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
    [teamClient, openDelete],
  )

  const handleLoaded = useCallback((data, resolve) => {
    setTeam(data.t ?? [])
    resolve()
  }, [setTeam])

  const handleCloseDelete = () => {
    setOpenDelete(false)
    setPlayerToDelete({})
  }

  const confirmDeleteTeam = () => {
    // eslint-disable-next-line
    teamClient().removePlayer({ teamId: team.id, steamId: playerToDelete.steamId }).then(
      () => {
        addMsgSuccess('Jogador removido com sucesso!')
      },
      () => {
        addMsgDanger('Erro ao remover jogador!')
      },
    ).then(() => {
      setPlayerToDelete({})
      setOpenDelete(false)
    })
  }

  return (
    <>
      <Box display="flex" alignItems="center" justifyContent="center">
        <Box py={3} width={1}>
          <Paper className={classes.paper}>
            <Box className={classes.paperHeader} mb={2}>
              <Box>
                <ArrowBackIcon color="secondary" onClick={() => history.push('/teams')} className={classes.arrowBackIcon} />
              </Box>
              <Box textAlign="center">
                <Typography fontWeight="bold" variant="h5">
                  Participantes do time
                </Typography>
              </Box>
            </Box>

            <Resolve onLoaded={handleLoaded} resolve={handleResolve}>
              <Box>
                <Box my={2} mt={4}>
                  {team.players?.map((item, index) => (
                    <Box
                      key={`team-${index}`}
                      my={1}
                      pb={2}
                      className={classes.itemList}
                    >
                      <Box display="flex" justifyContent="space-between" alignItems="center">
                        <Box display="flex" alignItems="center">
                          <Avatar
                            src={item.avatar}
                            alt="Avatar"
                            className={classes.avatar}
                            sx={{ width: theme.sizes.avatar, height: theme.sizes.avatar }}
                          />

                          <Box mx={0.5} />

                          {item.name}
                        </Box>

                        <Box display="flex" justifyContent="space-between">
                          {item.isCaptain && (
                            <Box>
                              <IconButton
                                className={classes.favIcon}
                                title="Este jogador é capitão da equipe."
                              >
                                <GradeIcon htmlColor="#ffdf00" />
                              </IconButton>
                            </Box>
                          )}
                          {(item.steamId !== user.steamId)
                            && team.players.some((x) => (x.steamId === user.steamId) && x.isCaptain)
                            && (
                              <Box ml={1}>
                                <Box>
                                  <IconButton
                                    className={classes.buttonIcon}
                                    onClick={() => {
                                      setPlayerToDelete(item)
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
                    </Box>
                  ))}
                </Box>

                <Box mb={2}>
                  {team?.players?.some((x) => (x.steamId === user.steamId) && x.isCaptain) && (
                    <Button
                      fullWidth
                      color="primary"
                      variant="outlined"
                      onClick={() => ''}
                      className={`${classes.button} ${classes.create} buttonLobby`}
                    >
                      {spanLayout.map((_, index) => (<span key={index} />))}

                      <Box p={1} display="flex" alignItems="center" onClick={() => setOpenInvite(true)}>
                        <Box mr={1}>
                          <SVG
                            width={30}
                            height={30}
                            src={icoArrow}
                            stroke={colors.lobby.create}
                          />
                        </Box>
                        Convidar novos jogadores
                      </Box>
                    </Button>
                  )}
                </Box>
              </Box>
            </Resolve>
          </Paper>
        </Box>
      </Box>

      {openInvite && (
        <InviteModal
          open={openInvite}
          setOpen={setOpenInvite}
          teamId={id}
        />
      )}

      {openDelete && (
        <MessageBox
          opened={openDelete}
          handleClose={handleCloseDelete}
          title="Remover Equipe"
          text={`Deseja realmente remover o jogador "${playerToDelete?.name}" da equipe “${team?.name}”?`}
          handleSecondary={handleCloseDelete}
          handlePrimary={confirmDeleteTeam}
        />
      )}
    </>
  )
}

export default TeamDetail
