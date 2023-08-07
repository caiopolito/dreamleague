import React, { useEffect, useState } from 'react'
import PropTypes from 'prop-types'
import {
  Box,
  Button,
  Typography,
  CircularProgress,
} from '@mui/material'
import { makeStyles } from '@mui/styles'
import SVG from 'react-inlinesvg'

import ModalInfo from 'components/ModalInfo/ModalInfo'
import userFilled from 'assets/lobby/user-filled.svg'
import userEmpty from 'assets/lobby/user-empty.svg'
import useSecurity from 'security/useSecurity'

import { colors } from 'theme/index'
import { useAlert } from 'components/Alert'

const useStyles = makeStyles(() => ({
  modal: {
    '& .MuiPaper-root': {
      width: 'min-content',
      minHeight: 250,
    },
  },
}))

const QueueModal = ({
  open,
  queue,
  setQueue,
  setOpenQueue,
  connection,
  matchStarted,
  setMatchStarted,
  ipAddress,
  setIpAddress,
}) => {
  const classes = useStyles()
  const [minPlayers, setMinPlayers] = useState(0)
  const { user } = useSecurity()
  const onCloseQueue = async () => {
    await connection.invoke('LeaveQueueAsync')
    setQueue(0)
    setOpenQueue(false)
    setIpAddress('')
    setMatchStarted(false)
  }
  const arrayFilled = Array.from({ length: queue })
  const arrayEmpty = Array.from({ length: minPlayers - queue })
  const { addMsgSuccess } = useAlert()

  useEffect(() => {
    async function enterQueueAsync() {
      connection.on('QueueInProgress', (amount, mininumPlayers) => {
        setQueue(amount)
        setMinPlayers(mininumPlayers)
      })

      connection.on('MatchStart', (ip) => {
        setIpAddress(ip)
        setMatchStarted(true)
      })

      connection.on('MatchEnd', () => {
        addMsgSuccess('Partida finalizada!')
        onCloseQueue()
      })

      await connection.invoke('EnterQueueAsync', user?.steamId, user?.name)
    }
    if (!matchStarted) { enterQueueAsync() }
    // eslint-disable-next-line
  }, [connection, setQueue])

  return (
    <ModalInfo
      close={!matchStarted}
      open={open}
      onClose={onCloseQueue}
      className={classes.modal}
    >
      {!matchStarted && (
        <Box mt={4} minWidth={600}>
          <Box display="flex" flexDirection="column" alignItems="center">
            <Box mb={3}>
              <Typography fontWeight="bold" color="primary" variant="h6">
                Aguardando outros jogadores...
              </Typography>
            </Box>

            <Box mb={3}>
              <CircularProgress color="primary" />
            </Box>

            <Box display="flex" m={3}>
              {arrayFilled.map((_, index) => (
                <Box key={index} width={55}>
                  <SVG
                    width={50}
                    height={50}
                    src={userFilled}
                    fill={colors.primary.main}
                    stroke={colors.primary.main}
                    color={colors.primary.main}
                  />
                </Box>
              ))}

              {arrayEmpty.map((_, index) => (
                <Box key={index} width={55}>
                  <SVG
                    width={45}
                    height={45}
                    src={userEmpty}
                    fill={colors.primary.main}
                    stroke={colors.primary.main}
                    color={colors.primary.main}
                  />
                </Box>
              ))}
            </Box>

            <Box>
              <Button
                color="primary"
                variant="text"
                onClick={onCloseQueue}
              >
                Cancelar
              </Button>
            </Box>
          </Box>
        </Box>
      )}
      {matchStarted && (
        <Box mt={4} minWidth={600}>
          <Box display="flex" flexDirection="column" alignItems="center">
            <Box mb={3}>
              <Typography fontWeight="bold" color="primary" variant="h6">
                Partida iniciada!!!
              </Typography>
            </Box>

            <Box mb={2} display="flex" flexDirection="column" alignItems="center">
              <Box mb={3}>
                <Typography fontWeight={100} color="primary" variant="h6">
                  {ipAddress}
                </Typography>
              </Box>
              <Box>
                <Button
                  variant="contained"
                  color="primary"
                  onClick={() => { navigator.clipboard.writeText(ipAddress) }}
                >
                  Copiar IP
                </Button>
              </Box>
            </Box>
          </Box>
        </Box>
      )}
    </ModalInfo>
  )
}

QueueModal.propTypes = {
  open: PropTypes.bool.isRequired,
  queue: PropTypes.number.isRequired,
  setOpenQueue: PropTypes.func.isRequired,
  setQueue: PropTypes.func.isRequired,
  connection: PropTypes.object.isRequired,
  matchStarted: PropTypes.bool,
  setMatchStarted: PropTypes.func.isRequired,
  ipAddress: PropTypes.string,
  setIpAddress: PropTypes.func.isRequired,
}

QueueModal.defaultProps = {
  matchStarted: false,
  ipAddress: '',
}

export default QueueModal
