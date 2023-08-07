import React, { useState, useMemo, useCallback } from 'react'
import useSecurity from 'security/useSecurity'
import {
  Box, Typography, Button, Paper, IconButton,
} from '@mui/material'
import ArrowBack from '@mui/icons-material/ArrowBack'
import Resolve from 'components/Resolve/Resolve'
import { useHistory, useParams } from 'react-router-dom'
import useChampionshipClient from 'clients/ChampionshipClient/useChampionshipClient'
import useClient from 'clients/Client/useClient'
import { colors } from 'theme'
import { Clear } from '@mui/icons-material'
import { makeStyles } from '@mui/styles'
import icoArrow from 'assets/ico-arrow.svg'
import SVG from 'react-inlinesvg'
import ShieldIcon from '@mui/icons-material/LocalPolice'
import MessageBox from 'components/MessageBox/MessageBox'
import { useAlert } from 'components/Alert'
import RegisterTeamModal from './RegisterTeamModal'
// import ChampionshipBrackets from './ChampionshipBrackets'

const useStyles = makeStyles(() => ({
  paper: {
    minHeight: 300,
    color: '#fff !important',
    padding: 16,
    backgroundColor: '#0705204a !important',
  },
  button: {
    height: 'auto !important',
    color: '#fff !important',
  },
  enter: {
    border: `1px solid ${colors.lobby.enter} !important`,
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

const ChampionshipDetail = () => {
  const spanLayout = [1, 2, 3, 4]
  const { id } = useParams()
  const { user } = useSecurity()
  const classes = useStyles()
  const history = useHistory()
  const [championship, setChampionship] = useState([])
  const [teams, setTeams] = useState([])
  const championshipClient = useChampionshipClient()
  const client = useClient()
  const [teamToDelete, setTeamToDelete] = useState({})
  const [openRegister, setOpenRegister] = useState(false)
  const [openDelete, setOpenDelete] = useState(false)
  const [hasTeam, setHasTeam] = useState(false)
  const { addMsgDanger, addMsgSuccess } = useAlert()

  const handleResolve = useMemo(
    () => ({
      ch: () => new Promise((resolve, reject) => {
        championshipClient().getChampionshipDetail(id).then(
          (response) => {
            resolve(response.data)
          },
          () => {
            reject()
          },
        )
      }),
      cht: () => new Promise((resolve, reject) => {
        championshipClient().getChampionshipTeams(id).then(
          (response) => {
            resolve(response.data)
          },
          () => {
            reject()
          },
        )
      }),
      pht: () => new Promise((resolve, reject) => {
        client().checkIfHasTeam(user.steamId).then(
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
    [championshipClient, client, openRegister, openDelete],
  )

  const handleLoaded = useCallback((data, resolve) => {
    setTeams(data.cht.teams ?? [])
    setHasTeam(data.pht.hasTeam ?? false)
    setChampionship(data.ch ?? {})
    resolve()
  }, [])

  const handleCloseDelete = () => {
    setOpenDelete(false)
    setTeamToDelete({})
  }

  const confirmDeleteTeam = () => {
    // eslint-disable-next-line
    championshipClient().removeTeam({ teamId: teamToDelete.id, championshipId: championship.id }).then(
      () => {
        addMsgSuccess('Equipe removida com sucesso!')
      },
      () => {
        addMsgDanger('Erro ao remover equipe!')
      },
    ).then(() => {
      setTeamToDelete({})
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
                <ArrowBack color="secondary" onClick={() => history.push('/championships')} className={classes.arrowBackIcon} />
              </Box>
              <Box textAlign="center">
                <Typography fontWeight="bold" variant="h5">
                  Participantes do campeonato
                  {` ${championship.name}`}
                </Typography>
              </Box>
            </Box>

            <Resolve onLoaded={handleLoaded} resolve={handleResolve}>
              <Box my={2} mt={4}>
                {teams?.map((item, index) => (
                  <Box
                    key={`team-${index}`}
                    my={1}
                    pb={2}
                  >
                    <Box display="flex" justifyContent="space-between" alignItems="center">
                      <Box display="flex" alignItems="center">
                        <ShieldIcon fontSize="large" htmlColor="#d59d13" />

                        <Box mx={0.5} />

                        {item.name}
                      </Box>

                      {(item?.players?.some((x) => (x.steamId === user.steamId) && x.isCaptain))
                        && (
                          <Box>
                            <IconButton
                              className={classes.buttonIcon}
                              onClick={() => {
                                setTeamToDelete(item)
                                setOpenDelete(true)
                              }}
                            >
                              <Clear htmlColor={colors.lobby.close} />
                            </IconButton>
                          </Box>
                        )}
                    </Box>
                  </Box>
                ))}
                <Box mb={5}>
                  {hasTeam && (
                    <Button
                      fullWidth
                      color="primary"
                      variant="outlined"
                      onClick={() => ''}
                      className={`${classes.button} ${classes.enter} buttonLobby`}
                    >
                      {spanLayout.map((_, index) => (<span key={index} />))}

                      <Box
                        p={1}
                        display="flex"
                        alignItems="center"
                        onClick={() => {
                          setOpenRegister(true)
                        }}
                      >
                        <Box mr={1}>
                          <SVG
                            width={30}
                            height={30}
                            src={icoArrow}
                            stroke={colors.lobby.enter}
                          />
                        </Box>
                        Inscrever-se
                      </Box>
                    </Button>
                  )}
                </Box>

                {/* <Box>
                  <Box mb={5} textAlign="center">
                    <Typography fontWeight="bold" variant="h5">
                      Chaveamento
                    </Typography>
                  </Box>
                  <Box display="flex" justifyContent="space-around">
                    <ChampionshipBrackets bracket={teams?.bracket} />
                  </Box>
                </Box> */}
              </Box>
            </Resolve>

          </Paper>
        </Box>
      </Box>

      {
        openRegister && (
          <RegisterTeamModal
            open={openRegister}
            setOpen={setOpenRegister}
            championship={championship}
          />
        )
      }

      {openDelete && (
        <MessageBox
          opened={openDelete}
          handleClose={handleCloseDelete}
          title="Remover Equipe"
          text={`Deseja realmente remover a equipe "${teamToDelete?.name}" do campeonato “${championship?.name}”?`}
          handleSecondary={handleCloseDelete}
          handlePrimary={confirmDeleteTeam}
        />
      )}
    </>
  )
}

export default ChampionshipDetail
