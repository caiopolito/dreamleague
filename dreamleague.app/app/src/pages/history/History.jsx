import React, { useCallback, useMemo, useState } from 'react'
import { makeStyles } from '@mui/styles'
import {
  Box,
  Paper,
  Button,
  Typography,
  Tooltip,
} from '@mui/material'
// import CheckCircleIcon from '@mui/icons-material/CheckCircle'

import useHistoryClient from 'clients/HistoryClient/useHistoryClient'
import Resolve from 'components/Resolve/Resolve'
import { colors } from 'theme'
import { useAlert } from 'components/Alert'
import icoTeam from 'assets/vitoria.png'
import moment from 'moment'
import CheckIcon from '@mui/icons-material/Check'
import HistoryDetail from './HistoryDetail'

const useStyles = makeStyles(() => ({
  paper: {
    minHeight: 300,
    color: '#fff !important',
    padding: 16,
    backgroundColor: '#0705204a !important',
  },
  matchBox: {
    border: '1px solid #80808052',
    borderRadius: '4px',
    overflow: 'hidden',
  },
  matchScore: {
    background:
      'linear-gradient(137deg, rgba(5,4,31,1) 21%, rgb(38 13 48) 100%, rgb(5 5 20) 100%)',
    padding: '16px',
    alignItems: 'center',
  },
  infos: {
    padding: 16,
  },
  score: {
    fontSize: '40px !important',
  },
  rounds: {
    color: `${colors.lobby.create} !important`,
  },
  cancelled: {
    color: `${colors.lobby.close} !important`,
  },
  circle: {
    width: 20,
    height: 18,
    textAlign: 'center',
    backgroundColor: colors.lobby.create,
    borderRadius: 19,
    paddingBottom: 3,
    fontSize: 14,
    position: 'absolute',
    top: 44,
    right: -5,
  },
}))

const Teams = () => {
  const classes = useStyles()
  const historyClient = useHistoryClient()
  const { addMsgDanger } = useAlert()

  const [matches, setMatches] = useState([])
  const [openDetails, setOpenDetails] = useState('')
  const [matchObject, setMatchObject] = useState({})

  const handleResolve = useMemo(
    () => ({
      matches: () => new Promise((resolve, reject) => {
        historyClient().getMatches().then(
          (response) => {
            resolve(response.data)
          },
          () => {
            addMsgDanger('Erro ao buscar histórico de partidas.')
            reject()
          },
        )
      }),
    }),
    // eslint-disable-next-line react-hooks/exhaustive-deps
    [historyClient],
  )

  const handleLoaded = useCallback((data, resolve) => {
    setMatches(data.matches.matches ?? [])
    resolve()
  }, [setMatches])

  const handleSelectMatch = (match, index) => {
    if (`match-${index}` === openDetails) {
      setMatchObject({})
      setOpenDetails('')

      return
    }

    historyClient().getDetailMatch(match?.matchId).then(
      (response) => {
        setMatchObject(response.data ?? {})
        setOpenDetails(`match-${index}`)
      },
      () => {
        addMsgDanger('Erro ao buscar detalhe da partida.')
        setMatchObject({})
        setOpenDetails('')
      },
    )
  }

  return (
    <Box>
      <Box textAlign="center" mb={2} mt={1}>
        <Typography fontWeight="bold" variant="h5">
          Histórico de partidas:
        </Typography>
      </Box>

      <Paper className={classes.paper}>
        <Box mb={2}>
          <Resolve onLoaded={handleLoaded} resolve={handleResolve}>
            <Box my={2} mt={4}>

              {matches?.map((item, index) => (
                <Box key={`match-${index}`} my={4} className={classes.matchBox}>
                  <Box display="flex" justifyContent="space-around" className={classes.matchScore}>
                    <Box>
                      <Typography variant="h6" width={250}>
                        {item?.team1String}
                      </Typography>
                    </Box>

                    <Box position="relative">

                      <img src={icoTeam} alt="Icone Time" width={50} />
                      {item?.winner === item?.team1Id && (
                        <Tooltip title="Ganhador">
                          <Box className={classes.circle}>
                            <CheckIcon fontSize="small" />
                          </Box>
                        </Tooltip>
                      )}
                    </Box>

                    <Box>
                      <Typography variant="h6" className={classes.score}>
                        {item?.team1Score ?? 0}
                      </Typography>
                    </Box>

                    <Box display="flex" flexDirection="column" textAlign="center" width={200}>
                      <Box>
                        <Typography variant="h6" className={item?.cancelled ? classes.cancelled : classes.rounds}>
                          {item?.cancelled ? 'CANCELADA' : item?.team1Score + item?.team2Score}
                        </Typography>
                      </Box>

                      VS
                    </Box>

                    <Box>
                      <Typography variant="h6" className={classes.score}>
                        {item?.team2Score ?? 0}
                      </Typography>
                    </Box>

                    <Box position="relative">
                      <img src={icoTeam} alt="Icone Time" width={50} />
                      {item?.winner === item?.team2Id && (
                        <Tooltip title="Ganhador">
                          <Box className={classes.circle}>
                            <CheckIcon fontSize="small" />
                          </Box>
                        </Tooltip>
                      )}
                    </Box>

                    <Box>
                      <Typography variant="h6" width={250}>
                        {item?.team2String}
                      </Typography>
                    </Box>
                  </Box>

                  {!item?.cancelled && (
                    <Box display="grid" gridTemplateColumns="1fr 50px 1fr" className={classes.infos}>
                      {/* <Box>
                        <CheckCircleIcon htmlColor={colors.lobby.create} fontSize="large" />
                      </Box> */}

                      <Box display="flex" justifyContent="space-evenly">
                        <Box display="flex" flexDirection="column" textAlign="center">
                          <Box>
                            <Typography variant="subtitle2">
                              Data
                            </Typography>
                          </Box>

                          {`${moment(item?.endTime).format('DD/MM/YYYY')} às ${moment(item?.endTime).format('hh:mm:ss')}`}
                        </Box>

                        <Box display="flex" flexDirection="column" textAlign="center">
                          <Box>
                            <Typography variant="subtitle2">
                              Mapa
                            </Typography>
                          </Box>

                          {item?.mapName}
                        </Box>
                      </Box>

                      <Box display="flex" flexDirection="column" textAlign="center">
                        <Box>
                          <Typography variant="subtitle2">
                            Tipo
                          </Typography>
                        </Box>

                        FILA
                      </Box>

                      <Box display="flex" justifyContent="space-evenly">
                        <Box display="flex" flexDirection="column" textAlign="center">
                          <Box>
                            <Typography variant="subtitle2">
                              K/D/A
                            </Typography>
                          </Box>

                          {item?.kills}
                          /
                          {item?.deaths}
                          /
                          {item?.assists}
                        </Box>

                        <Box display="flex" flexDirection="column" textAlign="center">
                          <Button color="primary" variant="outlined" onClick={() => handleSelectMatch(item, index)}>
                            Ver detalhes
                          </Button>
                        </Box>
                      </Box>

                    </Box>
                  )}

                  {openDetails === `match-${index}` && (
                    <HistoryDetail matchObject={matchObject} />
                  )}
                </Box>
              ))}
            </Box>
          </Resolve>
        </Box>
      </Paper>
    </Box>
  )
}

export default Teams
