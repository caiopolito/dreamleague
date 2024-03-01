import React, { useState, useMemo, useCallback } from 'react'
import PropTypes from 'prop-types'

import {
  Box,
  Typography,
  Skeleton,
  Avatar,
  IconButton,
} from '@mui/material'
import { makeStyles } from '@mui/styles'
import ChatIcon from '@mui/icons-material/Chat'
import SVG from 'react-inlinesvg'
import icoSteam from 'assets/ico/ico-steam.svg'

import Resolve from 'components/Resolve/Resolve'
import { useHistory } from 'react-router-dom'
import useSecurity from 'security/useSecurity'
import useClient from 'clients/Client/useClient'
import { useAlert } from 'components/Alert/Alert'
import { useLoader } from 'components/Loader/Loader'
import { colors } from 'theme'
import { useLobbyContext } from './LobbyContext'

const useStyles = makeStyles((theme) => ({
  itemFriend: {
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
  buttonIcon: {
    border: `1px solid ${colors.secondary.main} !important`,
    '&:hover': {
      backgroundColor: '#2AB27B!important',
    },
  },
  icons: {
    cursor: 'pointer',
  },
}))

const Friends = ({ isPanel }) => {
  const [friends, setFriends] = useState([])
  const [loading, setLoading] = useState(true)
  const history = useHistory()

  const { user } = useSecurity()
  const appClient = useClient()
  const { addMsgDanger } = useAlert()
  const { disableLoader, enableLoader } = useLoader()
  const classes = useStyles()
  const { setChat } = useLobbyContext()

  const loopSkeleton = [0, 1, 2, 3, 4, 5, 6]

  const handleResolve = useMemo(
    () => ({
      friendsIds: () => new Promise((resolve, reject) => {
        disableLoader()
        setLoading(true)
        appClient().getUserFriends(user?.steamId).then(
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
    [appClient, user],
  )

  const handleLoaded = useCallback((data, resolve) => {
    const { friendsIds } = data

    const arrFriends = friendsIds.friends ?? []
    const arrPayload = arrFriends.map((item) => item?.steamid)

    appClient().getUsers(arrPayload).then(
      (res) => {
        const friendsToShow = res?.data?.players ?? []

        const arrPanel = friendsToShow.slice(0, 6)

        setFriends(isPanel ? arrPanel : friendsToShow)
        enableLoader()
        setLoading(false)
        resolve()
      },
      (error) => {
        enableLoader()
        setLoading(false)
        addMsgDanger(error)
      },
    )
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [appClient, addMsgDanger])

  const openProfile = (url) => {
    window.open(url, '_blank', 'noopener,noreferrer')
  }

  const openChat = (friend) => {
    setChat({
      show: true,
      receiver: friend,
      emitter: user?.steamId,
    })
  }

  return (
    <>
      {loading && (
        <>
          {loopSkeleton.map((index) => (
            <Box display="flex" key={index} mb={2}>
              <Skeleton variant="circular" animation="pulse" width={25} height={25} />
              <Box mx={1} />
              <Skeleton variant="rounded" animation="pulse" width="100%" height={25} />
            </Box>
          ))}
        </>
      )}

      <Resolve onLoaded={handleLoaded} resolve={handleResolve}>
        <>
          {!loading && friends?.map((item, index) => (
            <React.Fragment key={index}>
              <Box
                display="flex"
                justifyContent="space-between"
                className={classes.itemFriend}
                title="Ver Perfil"
              >
                <Box
                  px={1}
                  my={1}
                  display="flex"
                  alignItems="center"
                  justifyContent="space-between"
                  width="100%"
                  onClick={() => history.push(`/profile/${item?.steamid}`)}
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
                          {item?.personaname}
                        </Typography>
                      </Box>
                    </Box>
                  </Box>
                </Box>
                {!isPanel && (
                  <Box display="flex" justifyContent="center" alignItems="center" mr={1}>
                    <IconButton className={classes.buttonIcon}>
                      <ChatIcon onClick={() => openChat(item)} color="secondary" />
                    </IconButton>

                    <Box mx={1} />

                    <IconButton className={classes.buttonIcon}>
                      <SVG
                        width={30}
                        height={30}
                        src={icoSteam}
                        title="Ver Perfil Steam"
                        onClick={() => openProfile(item?.profileurl)}
                        fill="#D3D3D3"
                      />
                    </IconButton>
                  </Box>
                )}
              </Box>
            </React.Fragment>
          ))}
        </>
      </Resolve>
    </>
  )
}

Friends.propTypes = {
  isPanel: PropTypes.bool,
}

Friends.defaultProps = {
  isPanel: false,
}

export default Friends
