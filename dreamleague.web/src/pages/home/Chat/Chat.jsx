/* eslint-disable no-console */
import React, { useEffect, useState, useRef } from 'react'
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'

import {
  Box,
  Fab,
  List,
  Grid,
  Paper,
  Divider,
  TextField,
  IconButton,
  Typography,
} from '@mui/material'
import { makeStyles } from '@mui/styles'
import SendIcon from '@mui/icons-material/Send'
import CloseIcon from '@mui/icons-material/Close'

import useSecurity from 'security/useSecurity'

import MessageLeft from './MessageLeft'
import MessageRight from './MessageRight'
import { useLobbyContext } from '../LobbyContext'

const useStyles = makeStyles({
  table: {
    minWidth: 650,
  },
  chatSection: {
    width: '100%',
    height: '80vh',
    maxHeight: 580,
  },
  headBG: {
    backgroundColor: '#e0e0e0',
  },
  borderRight500: {
    borderRight: '1px solid #e0e0e0',
  },
  messageArea: {
    height: '70vh',
    overflowY: 'auto',
    maxHeight: 390,
  },
  messageRight: {
    display: 'flex',
    justifyContent: 'flex-end',

    '& span': {
      color: '#fff',
      padding: 8,
      backgroundColor: '#d2bdff',
      borderRadius: 10,
    },
  },
  messageLeft: {
    display: 'flex',
    justifyContent: 'start',

    '& span': {
      color: '#fff',
      padding: 8,
      backgroundColor: '#39074d',
      borderRadius: 10,
    },
  },
})

const Chat = () => {
  const classes = useStyles()
  const messageRef = useRef()
  const { chat, setChat } = useLobbyContext()
  const { user } = useSecurity()

  const [connection, setConnection] = useState(null)
  const [messages, setMessages] = useState(null)

  const [currentMessage, setCurrentMessage] = useState('')

  useEffect(() => {
    if (messageRef && messageRef.current) {
      const { scrollHeight, clientHeight } = messageRef.current
      messageRef.current.scrollTo({ left: 0, top: scrollHeight - clientHeight, behavior: 'smooth' })
    }
  }, [messages])

  const sendMessage = async (receiver, emitter) => {
    if (!currentMessage) { return }
    if (connection) {
      try {
        await connection.invoke('SendMessageAsync', currentMessage, receiver.steamid, emitter)
      } catch (e) {
        console.log(`Chat - ${e}`)
      }
    } else {
      console.log('Chat - no connection yet')
    }
    setCurrentMessage('')
  }

  const handleChange = (e) => {
    const { value } = e?.target || 1
    setCurrentMessage(value)
  }

  const handleCloseChat = async () => {
    try {
      await connection.stop()
    } catch (e) {
      console.log(e)
    }
  }

  const handleKeypress = (e) => {
    if (e.charCode === 13) {
      sendMessage(chat?.receiver, chat?.emitter)
    }
  }

  useEffect(() => {
    async function openConnection() {
      const newConnection = new HubConnectionBuilder()
        .withUrl(`${process.env.REACT_APP_URL_HUB}api/chat`)
        .configureLogging(LogLevel.Information)
        .build()

      newConnection.onclose(() => {
        setConnection()
        setMessages([])
        setChat(null)
      })

      newConnection.on('ReceiveMessage', (chatInfo) => {
        setMessages(chatInfo.messages)
      })

      await newConnection.start()
      await newConnection.invoke('EnterChatAsync', chat.emitter, chat.receiver.steamid)

      setConnection(newConnection)
    }
    openConnection()
  }, [chat, setChat])

  return (
    <Box>
      <Grid container component={Paper} className={classes.chatSection}>
        <Grid container style={{ padding: '10px' }} color="primary">
          <Grid item xs={6}>
            <Box display="flex" alignItems="center" justifyContent="flex-start">
              <Box mr={2}>
                <img style={{ borderRadius: 4 }} src={chat.receiver?.avatarfull} width={50} alt="Avatar" />
              </Box>

              <Box mr={2}>
                <Typography fontWeight="bold" fontSize="1em" variant="body1" color="primary">
                  {chat.receiver?.personaname}
                </Typography>
              </Box>
            </Box>
          </Grid>
          <Grid item xs={6}>
            <Box display="flex" alignItems="center" justifyContent="flex-end">
              <IconButton onClick={handleCloseChat}>
                <CloseIcon />
              </IconButton>
            </Box>
          </Grid>
        </Grid>

        <Grid item xs={12}>
          <Divider />
        </Grid>

        <Grid item xs={12}>
          <List ref={messageRef} className={classes.messageArea}>
            {messages?.map((item, index) => (
              <React.Fragment key={index}>
                {item?.sender === user?.steamId && (
                  <MessageRight
                    styles={classes}
                    message={item?.content}
                    hour={item?.messageTimeFormatted}
                  />
                )}

                {item?.sender !== user?.steamId && (
                  <MessageLeft
                    styles={classes}
                    message={item?.content}
                    hour={item?.messageTimeFormatted}
                  />
                )}
              </React.Fragment>
            ))}
          </List>

          <Divider />

          <Grid container style={{ padding: '20px' }}>
            <Grid item xs={10}>
              <TextField
                label="Digite sua mensagem"
                fullWidth
                value={currentMessage}
                onChange={handleChange}
                onKeyPress={handleKeypress}
              />
            </Grid>

            <Grid item xs={2} align="right">
              <Fab color="primary" aria-label="add" onClick={async () => sendMessage(chat?.receiver, chat?.emitter)}>
                <SendIcon />
              </Fab>
            </Grid>
          </Grid>
        </Grid>
      </Grid>
    </Box>
  )
}

export default Chat
