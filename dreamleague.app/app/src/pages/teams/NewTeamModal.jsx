import React, {
  useState, useEffect,
} from 'react'
import PropTypes from 'prop-types'
import {
  Box,
  Button,
  Typography,
  TextField,
} from '@mui/material'
import { makeStyles } from '@mui/styles'

import ModalInfo from 'components/ModalInfo/ModalInfo'
import { useAlert } from 'components/Alert/Alert'
import useTeamClient from 'clients/TeamClient/useTeamClient'

import { colors } from 'theme/index'

const useStyles = makeStyles(() => ({
  modal: {
    '& .MuiPaper-root': {
      width: 'min-content',
      minHeight: 250,
    },
  },
  buttonIcon: {
    border: `1px solid ${colors.secondary.main} !important`,
  },
  notActive: {
    border: '1px solid #80808052 !important',
    color: 'initial !important',
  },
  active: {
    border: `2px solid ${colors.primary.main} !important`,
    color: `${colors.primary.main} !important`,
  },
  tipNames: {
    borderRadius: '30px !important',
    padding: '9px !important',
    paddingBottom: '12px !important',
    display: 'flex !important',
    alignItems: 'center !important',
    justifyContent: 'center !important',
    textAlign: 'center !important',
    marginRight: '10px !important',
    marginBottom: '10px !important',
    width: 'max-content !important',
    whiteSpace: 'nowrap !important',
  },
  filter: {
    backgroundColor: '#fff',
    width: '100%',

    '& .MuiInputAdornment-root': {
      color: '#c7c7c7',
    },
    '& .MuiInputBase-input': {
      paddingTop: 14,
      paddingBottom: 14,
      paddingLeft: 8,
      fontSize: 14,
    },
  },
}))

const NewTeamModal = ({
  open,
  setOpen,
  teamObject,
}) => {
  const classes = useStyles()
  const teamClient = useTeamClient()
  const { addMsgWarning, addMsgDanger, addMsgSuccess } = useAlert()
  const [title, setTitle] = useState('')

  const handleSave = () => {
    if (title === '') {
      addMsgWarning('Um nome pro seu time deve ser informado!')
      return
    }
    const payload = {
      id: teamObject,
      name: title,
    }

    if (teamObject) {
      teamClient().updateTeam(payload).then(
        () => {
          addMsgSuccess('Time atualizado com sucesso!')
        },
        () => {
          addMsgDanger('Erro ao atualizar time!')
        },
      ).then(() => {
        setOpen(false)
      })
    } else {
      teamClient().createTeam(payload).then(
        () => {
          addMsgSuccess('Time criado com sucesso!')
        },
        () => {
          addMsgDanger('Erro ao criar time!')
        },
      ).then(() => {
        setOpen(false)
      })
    }
  }

  useEffect(() => {
    if (teamObject) {
      teamClient().getTeamDetail(teamObject).then(
        (res) => {
          const { name } = res.data
          setTitle(name)
        },
        (error) => {
          addMsgDanger(error)
        },
      )
    }
  }, [teamObject, teamClient, addMsgDanger])

  return (
    <ModalInfo
      close
      open={open}
      className={classes.modal}
      onClose={() => setOpen(false)}
    >
      <>
        <Box mt={4} minWidth={600}>
          <Box display="flex" flexDirection="column" alignItems="center">
            <Box mb={3}>
              <Typography fontWeight={100} color="primary" variant="h6">
                Novo Time
              </Typography>
            </Box>
            <Box mb={3} width={300}>
              <TextField
                fullWidth
                label="Nome do Time"
                variant="outlined"
                value={title}
                onChange={(event) => {
                  const str = event.target.value

                  setTitle(str)
                }}
              />
            </Box>
            <Box width={300}>
              <Button
                fullWidth
                color="primary"
                variant="contained"
                onClick={handleSave}
              >
                Salvar
              </Button>
            </Box>
          </Box>
        </Box>
      </>
    </ModalInfo>
  )
}

NewTeamModal.propTypes = {
  open: PropTypes.bool.isRequired,
  setOpen: PropTypes.func.isRequired,
  teamObject: PropTypes.string,
}

NewTeamModal.defaultProps = {
  teamObject: null,
}

export default NewTeamModal
