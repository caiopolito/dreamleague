import { React } from 'react'
import PropType from 'prop-types'
import {
  Card,
  CardContent,
  CardHeader,
  Grid,
  TextField,
  Typography,
} from '@mui/material'

import { makeStyles } from '@mui/styles'

const useStyles = makeStyles(() => ({
  card: {
    minHeight: 300,
    color: '#fff !important',
    padding: 16,
    backgroundColor: '#0705204a !important',
  },
  texts: {
    color: '#D3D3D3',
  },
}))

const ProfileDetails = ({ user }) => {
  const classes = useStyles()

  return (
    <Card className={classes.card}>
      <CardHeader
        // eslint-disable-next-line
        subheader={<Typography>Visualize aqui as informações do jogador</Typography>}
        title="Informações"
      />
      <CardContent>
        <Grid container spacing={3}>
          <Grid item md={6} xs={12}>
            <TextField
              fullWidth
              label="Nome"
              name="nome"
              InputProps={{
                readOnly: true,
              }}
              value={user?.name}
              variant="standard"
            />
          </Grid>
          <Grid item md={6} xs={12}>
            <TextField
              fullWidth
              label="Steam ID"
              name="steamId"
              InputProps={{
                readOnly: true,
              }}
              value={user?.steamId}
              variant="standard"
            />
          </Grid>
        </Grid>
      </CardContent>
    </Card>
  )
}

ProfileDetails.propTypes = {
  user: PropType.object.isRequired,
}

export default ProfileDetails
