import React, { useCallback, useMemo, useState } from 'react'
import { makeStyles } from '@mui/styles'
import {
  Box, Typography, Paper, Button, IconButton,
} from '@mui/material'
import useChampionshipClient from 'clients/ChampionshipClient/useChampionshipClient'
import Resolve from 'components/Resolve/Resolve'
import MessageBox from 'components/MessageBox/MessageBox'
import { colors } from 'theme'
import useSecurity from 'security/useSecurity'
import { useAlert } from 'components/Alert'
import {
  BrushSharp, ClearSharp, EmojiEventsSharp,
} from '@mui/icons-material'
import { useHistory } from 'react-router-dom'
import NewChampionshipModal from './NewChampionshipModal'

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
  itemGridAward: {
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    width: '100%',
  },
  itemList: {
    display: 'flex',
    justifyContent: 'space-between',
    cursor: 'pointer',
    borderRadius: 4,
    '&:hover': {
      backgroundColor: 'rgba(1, 200, 208, 0.04)',
    },
  },
}))

const ChampionsShip = () => {
  const classes = useStyles()
  const championshipClient = useChampionshipClient()
  const history = useHistory()
  const { user } = useSecurity()

  const { addMsgDanger, addMsgSuccess } = useAlert()

  const [championship, setChampionship] = useState([])
  const [championshipId, setChampionshipId] = useState(null)
  const [openNew, setOpenNew] = useState(false)

  const [openDelete, setOpenDelete] = useState(false)
  const [champToDelete, setChampToDelete] = useState({})

  const handleResolve = useMemo(
    () => ({
      championships: () => new Promise((resolve, reject) => {
        championshipClient().getChampionships().then(
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
    [championshipClient, openNew, openDelete],
  )

  const handleLoaded = useCallback((data, resolve) => {
    const { championships } = data
    setChampionship(championships.championships ?? [])
    resolve()
    // eslint-disable-next-line
  }, [setChampionship])

  const handleCloseDelete = () => {
    setOpenDelete(false)
    setChampToDelete({})
  }

  const confirmDeleteChamp = () => {
    championshipClient().deleteChampionship(champToDelete.id).then(
      () => {
        addMsgSuccess('Campeonato deletado com sucesso!')
      },
      () => {
        addMsgDanger('Erro ao deletar campeonato!')
      },
    )
    setChampToDelete({})
    setOpenDelete(false)
  }

  return (
    <Box>
      <Box textAlign="center" mb={2} mt={1}>
        <Typography fontWeight="bold" variant="h5">
          Campeonatos:
        </Typography>
      </Box>

      <Paper className={classes.paper}>
        <Box mb={2}>
          {user.isAdmin && (
            <Box display="flex" justifyContent="flex-end">
              <Button
                variant="contained"
                color="primary"
                onClick={() => {
                  setChampionshipId(null)
                  setOpenNew(true)
                }}
              >
                Criar campeonato
              </Button>
            </Box>
          )}

          <Resolve onLoaded={handleLoaded} resolve={handleResolve}>
            <Box my={2} mt={4}>
              {championship?.map((item, index) => (
                <Box key={index} className={classes.itemList} padding={1} onClick={() => history.push(`/championship/${item.id}`)}>
                  <Box className={classes.itemGridAward}>
                    <Box display="flex" alignItems="center">
                      <Box>
                        <EmojiEventsSharp fontSize="large" htmlColor="#d59d13" />
                      </Box>

                      <Box mr={2} />

                      <Box>
                        <Box display="flex" flexDirection="column">
                          <Box mb={1}>
                            <Typography variant="h5" className={classes.title}>
                              <b>{item?.name}</b>
                            </Typography>
                          </Box>

                          <Box>
                            <Typography fontWeight={100} variant="body2">
                              {item?.description}
                            </Typography>
                          </Box>
                        </Box>
                      </Box>
                    </Box>
                  </Box>

                  {user.isAdmin && (
                    <Box display="flex" justifyContent="flex-end" alignItems="center">
                      <IconButton
                        className={classes.buttonIcon}
                        onClick={() => {
                          setChampionshipId(item?.id)
                          setOpenNew(true)
                        }}
                      >
                        <BrushSharp htmlColor={colors.lobby.invite} />
                      </IconButton>

                      <Box mx={1} />

                      <IconButton
                        className={classes.buttonIcon}
                        onClick={() => {
                          setChampToDelete(item)
                          setOpenDelete(true)
                        }}
                      >
                        <ClearSharp htmlColor={colors.lobby.close} />
                      </IconButton>
                    </Box>
                  )}
                </Box>
              ))}
            </Box>
          </Resolve>
        </Box>
      </Paper>

      {openDelete && (
        <MessageBox
          opened={openDelete}
          handleClose={handleCloseDelete}
          title="Excluir Campeonato"
          text={`Deseja realmente excluir o campeonato “${champToDelete?.name}”?`}
          handleSecondary={handleCloseDelete}
          handlePrimary={confirmDeleteChamp}
        />
      )}

      {openNew && (
        <NewChampionshipModal
          open={openNew}
          setOpen={setOpenNew}
          championshipId={championshipId}
        />
      )}
    </Box>
  )
}

export default ChampionsShip
