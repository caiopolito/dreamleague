import React, { useEffect, useCallback, useState } from 'react'
import EventEmitter from 'events'
import PropTypes from 'prop-types'
import {
  isEmpty, isString, isArray, head,
} from 'lodash'
import Alert from '@mui/lab/Alert'
import { Collapse, Typography } from '@mui/material'
import MessageBox from '../MessageBox/MessageBox'

const emitter = new EventEmitter()

const DEFAULT_ID = 'global'

const AlertEmiter = (props) => {
  const { id } = props
  const [type, setType] = useState()
  const [text, setText] = useState()
  const [opened, setOpened] = useState()
  const [global, setGlobal] = useState(true)
  const [messages, setMessages] = useState()

  const addAlert = useCallback((data, alertType) => {
    setType(alertType)
    setMessages()
    setText()

    if (isString(data)) {
      setText(data)
    } else if (isArray(data)) {
      if (data.length === 1) {
        const value = head(data)
        setText(value.message)
      } else {
        setMessages(data)
      }
    } else {
      setText(data.message)
    }

    setOpened(true)
  }, [])

  useEffect(() => {
    setGlobal(id === DEFAULT_ID)
  }, [id])

  useEffect(() => {
    const addDanger = (data) => addAlert(data, 'error')
    emitter.on(`${id}.alert.danger`, addDanger)

    const addSuccess = (data) => addAlert(data, 'success')
    emitter.on(`${id}.alert.success`, addSuccess)

    const addWarning = (data) => addAlert(data, 'warning')
    emitter.on(`${id}.alert.warning`, addWarning)

    return () => {
      emitter.removeListener(`${id}.alert.danger`, addDanger)
      emitter.removeListener(`${id}.alert.success`, addSuccess)
      emitter.removeListener(`${id}.alert.warning`, addWarning)
    }
  }, [id, addAlert])

  const handleClose = () => {
    setOpened(false)
  }

  const messagesRender = () => (
    <Typography variant="body2" component="div">
      <ul>
        {messages.filter((item) => !isEmpty(item.message)).map(
          (item, index) => (
            <li key={index}>
              <span>{item.message}</span>
            </li>
          ),
        )}
      </ul>
    </Typography>
  )

  return global ? (
    <MessageBox
      type={type}
      text={text}
      opened={opened}
      labelPrimary="Voltar"
      handleClose={handleClose}
      handlePrimary={handleClose}
      maxWidth="xs"
      dataTestId="modal-alert-alertEmitter"
    >
      {messages && messagesRender()}
    </MessageBox>
  ) : (
    <Collapse in={opened}>
      <Alert
        severity={type}
        closeText="Fechar"
        onClose={handleClose}
      >
        {messages && messagesRender()}
        {!isEmpty(text) && (
          <span>{text}</span>)}
      </Alert>
    </Collapse>
  )
}

export const useAlert = (id = DEFAULT_ID) => {
  const addMsg = useCallback((data, event) => {
    if (isEmpty(data)) {
      return
    }

    emitter.emit(event, data)
  }, [])

  const addMsgSuccess = useCallback((data) => {
    addMsg(data, `${id}.alert.success`)
  }, [id, addMsg])

  const addMsgWarning = useCallback((data) => {
    addMsg(data, `${id}.alert.warning`)
  }, [id, addMsg])

  const addMsgDanger = useCallback((data) => {
    addMsg(data, `${id}.alert.danger`)
  }, [id, addMsg])

  return {
    addMsgDanger,
    addMsgWarning,
    addMsgSuccess,
  }
}

AlertEmiter.propTypes = {
  id: PropTypes.string,
}

AlertEmiter.defaultProps = {
  id: DEFAULT_ID,
}

export default AlertEmiter
