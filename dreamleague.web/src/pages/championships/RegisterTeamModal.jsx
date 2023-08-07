import React, {
  useState, useMemo, useCallback,
} from 'react'
import useTeamClient from 'clients/TeamClient/useTeamClient'
import { makeStyles } from '@mui/styles'
import ModalInfo from 'components/ModalInfo/ModalInfo'
import Resolve from 'components/Resolve/Resolve'
import {
  Box, Typography, Button,
} from '@mui/material'
import PropTypes from 'prop-types'
import { useAlert } from 'components/Alert'
import { colors } from 'theme/index'
import useChampionshipClient from 'clients/ChampionshipClient/useChampionshipClient'

const useStyles = makeStyles(() => ({
  modal: {
    '& .MuiPaper-root': {
      width: 'min-content',
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
}))

const RegisterTeamModal = ({ open, setOpen, championship }) => {
  const classes = useStyles()
  const teamClient = useTeamClient()
  const championshipClient = useChampionshipClient()
  const [teams, setTeams] = useState([])
  const { addMsgDanger, addMsgWarning, addMsgSuccess } = useAlert()
  const [selectedTeam, setSelectedTeam] = useState()

  const handleResolve = useMemo(
    () => ({
      response: () => new Promise((resolve, reject) => {
        teamClient().getTeams(true).then(
          (response) => {
            resolve(response.data)
          },
          () => {
            addMsgDanger('Erro ao buscar times!')
            reject()
          },
        )
      }),
    }),
    // eslint-disable-next-line
    [teamClient],
  )

  const handleLoaded = useCallback((data, resolve) => {
    setTeams(data.response.teams)
    resolve()
  }, [])

  const handleRegister = () => {
    if (!selectedTeam) {
      addMsgWarning('Pelo menos um time deve ser selecionado!')
      return
    }

    championshipClient().registerTeam(championship.id, selectedTeam.id).then(
      () => {
        addMsgSuccess('Time inscrito com sucesso!')
      },
      () => {
        addMsgDanger('Erro ao inscrever equipe!')
      },
    ).then(() => {
      setOpen(false)
    })
  }

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
              <Typography fontWeight="Bold" color="primary" variant="h6">
                Inscrever-se em
                {` ${championship.name}`}
              </Typography>
            </Box>

            <Box mb={3} textAlign="center">
              <Typography fontWeight={100} color="primary" variant="subtitle1">
                Selecione o time que deseja inscrever-se:
              </Typography>
            </Box>

            <Box width={700} maxHeight={250} overflow="auto" mb={2}>
              <Resolve resolve={handleResolve} onLoaded={handleLoaded}>
                <Box display="flex" m={3} width={650} flexWrap="wrap" justifyContent="center">
                  {teams?.map((item, index) => (
                    <Button
                      key={index}
                      className={`${classes.tipNames} ${item.id === selectedTeam?.id ? classes.active : classes.notActive}`}
                      onClick={() => setSelectedTeam(item)}
                    >
                      <Box>
                        {item?.name}
                      </Box>
                    </Button>
                  ))}
                </Box>
              </Resolve>
            </Box>

            <Box mb={3} />

            <Box width={300}>
              <Button
                fullWidth
                color="primary"
                variant="contained"
                onClick={handleRegister}
              >
                Inscrever-se
              </Button>
            </Box>
          </Box>
        </Box>
      </>
    </ModalInfo>
  )
}

RegisterTeamModal.propTypes = {
  open: PropTypes.bool.isRequired,
  setOpen: PropTypes.func.isRequired,
  championship: PropTypes.object.isRequired,
}

export default RegisterTeamModal
