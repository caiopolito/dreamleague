import React, {
  useState, useEffect,
} from 'react'
import PropTypes from 'prop-types'
import {
  Box,
  Button,
  Typography,
  TextField,
  Select,
  InputLabel,
  MenuItem,
} from '@mui/material'
import { makeStyles } from '@mui/styles'

import { useAlert } from 'components/Alert'

import { DesktopDatePicker, LocalizationProvider } from '@mui/lab'

import ModalInfo from 'components/ModalInfo/ModalInfo'
import { AdapterMoment } from '@mui/x-date-pickers/AdapterMoment'
import moment from 'moment/moment'
import useChampionshipClient from 'clients/ChampionshipClient/useChampionshipClient'

const useStyles = makeStyles(() => ({
  modal: {
    '& .MuiPaper-root': {
      width: 'min-content',
    },
  },
}))

const NewChampionshipModal = ({
  open,
  setOpen,
  championshipId,
}) => {
  const classes = useStyles()

  const client = useChampionshipClient()
  const { addMsgDanger, addMsgSuccess } = useAlert()
  const [title, setTitle] = useState('')
  const [description, setDescription] = useState('')
  const [minTeams, setMinTeams] = useState(1)
  const [playersOnTeam, setplayersOnTeam] = useState(1)
  const [startDate, setStartDate] = useState(moment().format('L'))

  const handleSave = async () => {
    const payload = {
      id: championshipId,
      name: title,
      description,
      startDate,
      minTeams,
      playersOnTeam,
    }
    if (championshipId) {
      client().updateChampionship(payload).then(
        () => {
          addMsgSuccess('Campeonato atualizado com sucesso!')
        },
        (error) => {
          // eslint-disable-next-line
          console.log(error)
          addMsgDanger('Erro ao atualizar campeonato!')
        },
      ).then(() => {
        setOpen(false)
      })
    } else {
      client().createChampionship(payload).then(
        () => {
          addMsgSuccess('Campeonato criado com sucesso!')
        },
        (error) => {
          // eslint-disable-next-line
          console.log(error)
          addMsgDanger('Erro ao criar campeonato!')
        },
      ).then(() => {
        setOpen(false)
      })
    }
  }

  const onDateChange = (date) => {
    setStartDate(date)
  }

  useEffect(() => {
    if (championshipId) {
      client().getChampionshipDetail(championshipId).then(
        (res) => {
          setTitle(res.data.name)
          setDescription(res.data.description)
          setStartDate(res.data.startDate)
          setMinTeams(res.data.minTeams)
          setplayersOnTeam(res.data.playersOnTeam)
        },
        (error) => {
          addMsgDanger(error)
        },
      )
    }
  }, [championshipId, client, addMsgDanger])

  return (
    <ModalInfo
      close
      open={open}
      className={classes.modal}
      onClose={() => {
        setOpen(false)
      }}
    >
      <>
        <Box mt={4} minWidth={600}>
          <Box display="flex" flexDirection="column" alignItems="center">
            <Box mb={3}>

              <Typography fontWeight={100} color="primary" variant="h6">
                Novo Campeonato
              </Typography>
            </Box>

            <Box mb={3} width={300}>
              <TextField
                fullWidth
                label="Título do Campeonato"
                variant="outlined"
                value={title}
                onChange={(event) => {
                  const str = event.target.value

                  setTitle(str)
                }}
              />
            </Box>

            <Box mb={3} width={300}>
              <TextField
                fullWidth
                label="Descrição do Campeonato"
                variant="outlined"
                value={description}
                onChange={(event) => {
                  const str = event.target.value

                  setDescription(str)
                }}
              />
            </Box>

            <Box mb={3} width={300}>
              <LocalizationProvider dateAdapter={AdapterMoment}>
                <DesktopDatePicker
                  label="Data de início do campeonato"
                  inputFormat="DD/MM/YYYY"
                  value={startDate}
                  variant="outlined"
                  onChange={onDateChange}
                  disablePast
                  renderInput={(params) => <TextField {...params} />}
                />
              </LocalizationProvider>
            </Box>

            <Box mb={3} width={300}>
              <InputLabel id="min-teams">Quantidade de times</InputLabel>
              <Select
                fullWidth
                labelId="min-teams"
                id="min-teams-select"
                value={minTeams}
                label="Quantidade de times"
                onChange={(event) => {
                  const teams = event.target.value

                  setMinTeams(teams)
                }}
              >
                <MenuItem value={1}>1</MenuItem>
                <MenuItem value={2}>2</MenuItem>
                <MenuItem value={8}>8</MenuItem>
                <MenuItem value={16}>16</MenuItem>
              </Select>
            </Box>

            <Box mb={3} width={300}>
              <InputLabel id="players-on-team">Quantidade de players por time</InputLabel>
              <Select
                fullWidth
                labelId="players-on-team"
                id="players-on-team-select"
                value={playersOnTeam}
                label="Quantidade de players"
                onChange={(event) => {
                  const teams = event.target.value

                  setplayersOnTeam(teams)
                }}
              >
                <MenuItem value={1}>1</MenuItem>
                <MenuItem value={2}>2</MenuItem>
                <MenuItem value={3}>3</MenuItem>
                <MenuItem value={4}>4</MenuItem>
                <MenuItem value={5}>5</MenuItem>
              </Select>
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

NewChampionshipModal.propTypes = {
  open: PropTypes.bool.isRequired,
  setOpen: PropTypes.func.isRequired,
  championshipId: PropTypes.string,
}

NewChampionshipModal.defaultProps = {
  championshipId: null,
}

export default NewChampionshipModal
