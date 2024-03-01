import React, { useEffect, useState } from 'react'

import {
  Box, Container, Grid, Typography,
} from '@mui/material'
import AccountProfile from 'components/Profile/Profile'
import ProfileDetails from 'components/Profile/ProfileDetails'
import { useParams, useHistory } from 'react-router-dom'
import useClient from 'clients/Client/useClient'
import { useAlert } from 'components/Alert'
import ArrowBackIcon from '@mui/icons-material/ArrowBack'
import { makeStyles } from '@mui/styles'

const useStyles = makeStyles(() => ({
  arrowBackIcon: {
    cursor: 'pointer',
    '&:hover': {
      transition: 'all ease-in-out .2s',
      color: '#2AB27B',
    },
  },
}))

const ProfilePage = () => {
  const { id } = useParams()
  const client = useClient()
  const [user, setUser] = useState(null)
  const { addMsgDanger } = useAlert()
  const history = useHistory()
  const classes = useStyles()

  useEffect(() => {
    client().getUser(id).then(
      (response) => {
        setUser(response.data)
      },
      () => {
        addMsgDanger('Erro ao buscar informações do usuário!')
      },
    )
  }, [id, client, addMsgDanger])
  return (
    <>
      <Box
        component="main"
        sx={{
          flexGrow: 1,
          py: 8,
        }}
      >
        <Container maxWidth="lg">
          <Box mb={3} display="flex" alignItems="center">
            <Box>
              <ArrowBackIcon color="secondary" onClick={() => history.push('/community')} className={classes.arrowBackIcon} />
            </Box>
            <Box mx={1} />
            <Box>
              <Typography
                variant="h4"
              >
                Perfil
              </Typography>
            </Box>
          </Box>
          <Grid
            container
            spacing={3}
          >
            <Grid
              item
              lg={6}
              md={6}
              xs={12}
            >
              {user && (<AccountProfile user={user} />)}
            </Grid>
            <Grid
              item
              lg={6}
              md={6}
              xs={12}
            >
              {user && (<ProfileDetails user={user} />)}
            </Grid>
          </Grid>
        </Container>
      </Box>
    </>
  )
}
export default ProfilePage
