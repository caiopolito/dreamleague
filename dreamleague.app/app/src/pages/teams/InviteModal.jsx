import React, {
  useState, useMemo, useCallback,

} from 'react'
import Resolve from 'components/Resolve/Resolve'
import PropTypes from 'prop-types'
import {
  Box,
  Button,
  Typography,
  FormControl,
  OutlinedInput,
  InputAdornment,
} from '@mui/material'
import { makeStyles } from '@mui/styles'
import { find, filter } from 'lodash'

import icoSearch from 'assets/pages/ico-option-search.svg'
import icoClose from 'assets/pages/ico-option-close.svg'

import ModalInfo from 'components/ModalInfo/ModalInfo'

import { colors } from 'theme/index'
import useTeamClient from 'clients/TeamClient/useTeamClient'
import { useAlert } from 'components/Alert'

const useStyles = makeStyles(() => ({
  modal: {
    '& .MuiPaper-root': {
      width: 'min-content',
      minHeight: 250,
    },
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

const InviteModal = ({
  open, setOpen, teamId,
}) => {
  const classes = useStyles()
  const [search, setSearch] = useState('')
  const [playersStock, setPlayersStock] = useState([])
  const [players, setPlayers] = useState([])
  const [selectPlayers, setSelectPlayers] = useState([])
  const teamClient = useTeamClient()
  const { addMsgDanger, addMsgSuccess, addMsgWarning } = useAlert()

  const handleResolve = useMemo(
    () => ({
      players: () => new Promise((resolve, reject) => {
        teamClient().getAllPlayers({ name: search, teamId }).then(
          (response) => {
            resolve(response.data)
          },
          () => {
            reject()
          },
        )
      }),
    }),
    // eslint-disable-next-line
    [teamClient, search],
  )

  const handleLoaded = useCallback((data, resolve) => {
    const { players: playersResponse } = data.players

    const playersMapped = playersResponse.map((x) => ({ steamId: x.steamId, name: x.name }))
    setPlayers(playersMapped)
    setPlayersStock(playersMapped)
    resolve()
  }, [])

  const handleSelectPlayer = (item) => {
    const alreadyAdd = find(selectPlayers, (player) => player.steamId === item.steamId)

    if (alreadyAdd) {
      const index = selectPlayers.findIndex((player) => player.steamId === item.steamId)

      const newList = [...selectPlayers]
      newList.splice(index, 1)

      setSelectPlayers(newList)
    } else {
      setSelectPlayers([...selectPlayers, item])
    }
  }

  const handleInvite = () => {
    if (!selectPlayers.length) {
      addMsgWarning('Pelo menos um jogador deve ser selecionado!')
      return
    }
    const payload = {
      teamId,
      steamIds: selectPlayers.map((x) => (x.steamId)),
    }

    teamClient().inviteToTeam(payload).then(
      () => {
        addMsgSuccess('Jogadores convidados com sucesso!')
      },
      () => {
        addMsgDanger('Erro ao convidar jogadores!')
      },
    ).then(() => {
      setOpen(false)
    })
  }

  const handleChange = (e) => {
    const { value } = e.target
    setSearch(value)

    const filtered = filter(playersStock, (context) => context?.name?.toLowerCase()
      .includes(value.toLowerCase()))

    setPlayers(filtered)
  }

  const handleClear = () => {
    setSearch('')
    setPlayers(playersStock)
  }

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
              <Typography fontWeight="Bold" color="primary" variant="h6">
                Convidar jogadores
              </Typography>
            </Box>

            <Box mb={1}>
              <Typography fontWeight={100} color="primary" variant="subtitle1">
                Selecione os players para o time:
              </Typography>
            </Box>

            <Box my={2} width={250}>
              <FormControl variant="outlined" className={classes.filter}>
                <OutlinedInput
                  startAdornment={(
                    <InputAdornment position="end">
                      <img src={icoSearch} width={16} alt="Busque aqui por um player" />
                    </InputAdornment>
                  )}
                  endAdornment={(
                    <InputAdornment position="end">
                      {search !== '' && (
                        <Box onClick={handleClear} sx={{ cursor: 'pointer' }}>
                          <img src={icoClose} width={13} alt="Limpar" />
                        </Box>
                      )}
                    </InputAdornment>
                  )}
                  placeholder="Busque aqui por um player"
                  value={search}
                  onChange={handleChange}
                />
              </FormControl>
            </Box>

            <Box width={700} maxHeight={250} overflow="auto" mb={2}>
              <Resolve resolve={handleResolve} onLoaded={handleLoaded}>
                <Box display="flex" m={3} width={650} flexWrap="wrap" justifyContent="center">
                  {players?.map((item, index) => (
                    <Button
                      key={index}
                      className={`${classes.tipNames} ${selectPlayers.find((x) => x.steamId === item.steamId) ? classes.active : classes.notActive}`}
                      onClick={() => handleSelectPlayer(item)}
                    >
                      <Box>
                        {item?.name}
                      </Box>
                    </Button>
                  ))}
                </Box>
              </Resolve>
            </Box>

            <Box width={300}>
              <Button
                fullWidth
                color="primary"
                variant="contained"
                onClick={handleInvite}
              >
                Convidar
              </Button>
            </Box>
          </Box>
        </Box>
      </>
    </ModalInfo>
  )
}

InviteModal.propTypes = {
  open: PropTypes.bool.isRequired,
  setOpen: PropTypes.func.isRequired,
  teamId: PropTypes.string.isRequired,
}

export default InviteModal
