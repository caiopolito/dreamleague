import React, { useState, useEffect } from 'react'
import { HubConnectionBuilder } from '@microsoft/signalr'

import {
  Box,
  Popover,
  Typography,
  Divider,
  IconButton,
} from '@mui/material'
import SVG from 'react-inlinesvg'
import { makeStyles } from '@mui/styles'

import icoNotifications from 'assets/header/ico-notifications.svg'
import { colors } from 'theme'

import CheckIcon from '@mui/icons-material/Check'
import DoNotDisturbIcon from '@mui/icons-material/DoNotDisturb'

import useSecurity from 'security/useSecurity'
import { useAlert } from 'components/Alert'

const useStyles = makeStyles({
  tip: {
    width: 20,
    height: 18,
    textAlign: 'center',
    backgroundColor: colors.notification.color,
    borderRadius: 19,
    paddingBottom: 3,
    fontSize: 14,
    position: 'absolute',
    top: -11,
    right: -10,
  },
  cursor: {
    cursor: 'pointer',
  },
  buttonNotify: {
    '&:hover': {
      backgroundColor: '#d3d3d366',
      transition: 'background-color 300ms cubic-bezier(0.4, 0, 0.2, 1) 0ms',
    },
  },
  popOverNotify: {
    '& .MuiPaper-root': {
      width: 400,
    },
  },
})

const Notifications = () => {
  const classes = useStyles()
  const { user } = useSecurity()
  const { addMsgSuccess } = useAlert()

  const [connection, setConnection] = useState(null)
  const [hasNotification, setHasNotification] = useState(false)
  const [notifications, setNotifications] = useState()

  const [anchorEl, setAnchorEl] = useState(null)
  const open = Boolean(anchorEl)

  const seeNotification = async () => {
    await connection.invoke('SetNotificationsAsSeenAsync', user.steamId)
  }

  async function openConnection() {
    const newConnection = new HubConnectionBuilder()
      .withUrl(`${process.env.REACT_APP_URL_HUB}api/notification`)
      .build()

    newConnection.onclose(() => {
      setConnection(null)
      setNotifications([])
    })
    newConnection.on('ReceiveNotifications', (notificationsResponse) => {
      setHasNotification(notificationsResponse.teamNotifications.length > 0)
      setNotifications(notificationsResponse)
    })

    newConnection.on('UpdateNotifications', () => {
      newConnection.invoke('GetNotificationsAsync', user?.steamId)
    })

    await newConnection.start()
    await newConnection.invoke('GetNotificationsAsync', user?.steamId)
    setConnection(newConnection)
  }

  const handlePopoverOpen = (event) => {
    setAnchorEl(event.currentTarget)
    seeNotification()
  }

  const handlePopoverClose = () => {
    setAnchorEl(null)
    setNotifications([])
  }

  const acceptNotification = async (notificationId) => {
    await connection.invoke('AcceptNotificationAsync', user.steamId, notificationId)
    addMsgSuccess('Solicitação aceitada com sucesso!')
    handlePopoverClose()
  }

  const refuseNotification = async (notificationId) => {
    await connection.invoke('RefuseNotificationAsync', user.steamId, notificationId)
    addMsgSuccess('Solicitação recusada com sucesso!')
    handlePopoverClose()
  }

  useEffect(() => {
    openConnection()
    // eslint-disable-next-line
  }, [setNotifications, setHasNotification])

  return (
    <>
      <Box
        mx={2}
        position="relative"
        className={`${classes.cursor}`}
        onClick={handlePopoverOpen}
      >
        <SVG
          width={20}
          height={25}
          src={icoNotifications}
          fill={colors.secondary.main}
        />

        {notifications?.notSeen > 0 && (
          <Box className={classes.tip}>
            {notifications?.notSeen}
          </Box>
        )}
      </Box>

      <Popover
        open={open}
        anchorEl={anchorEl}
        onClose={handlePopoverClose}
        className={classes.popOverNotify}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}
        transformOrigin={{ vertical: 'top', horizontal: 'right' }}
      >
        <Box px={2} py={1} textAlign="center">
          <Typography variant="h6" color="WindowText">
            Notificações
          </Typography>
        </Box>

        <Divider orientation="horizontal" variant="middle" flexItem className={classes.divider} />

        {!hasNotification
          && (
            <Box px={2} py={1} my={2} textAlign="center">
              <Typography variant="h7" color="WindowText">
                Você não possui notificações.
              </Typography>
            </Box>
          )}

        {notifications?.teamNotifications?.map((item, index) => (
          <Box
            key={index}
            px={3}
            py={2}
            index={index}
            display="flex"
            alignItems="center"
            borderBottom="1px solid lightgray"
            className={`${classes.cursor} ${classes.buttonNotify}`}
          >
            <Box color="red" mr={2}>
              &#9679;
            </Box>

            <Box>
              <Box>
                <Typography fontWeight="bold" fontSize="1em" variant="subtitle2" color="primary">
                  CONVITE
                </Typography>
              </Box>

              <Box>
                <Typography variant="body1" color="WindowText">
                  {`Alguém te chamou para fazer parte de uma equipe! Deseja participar de ${item?.teamName}?`}
                </Typography>
              </Box>
            </Box>

            <Box>
              <Box>
                <IconButton color="success" onClick={() => acceptNotification(item.id)}>
                  <CheckIcon />
                </IconButton>
              </Box>

              <Box>
                <IconButton color="error" onClick={() => refuseNotification(item.id)}>
                  <DoNotDisturbIcon />
                </IconButton>
              </Box>
            </Box>
          </Box>
        ))}
      </Popover>
    </>
  )
}

export default Notifications
