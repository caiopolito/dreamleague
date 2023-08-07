import React from 'react'
import PropTypes from 'prop-types'
import {
  Avatar,
  Box,
  Card,
  CardContent,
  CardActions,
  Divider,
  Typography,
  Grid,
  Button,
  Tooltip,
} from '@mui/material'

import { makeStyles } from '@mui/styles'
import BorderLinearProgress from 'components/BorderLinearProgress/BorderLinearProgress'
import SVG from 'react-inlinesvg'
import icoSteam from 'assets/ico/ico-steam.svg'

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
const openProfile = (url) => {
  window.open(url, '_blank', 'noopener,noreferrer')
}

const AccountProfile = ({
  user,
}) => {
  const classes = useStyles()

  return (
    <Card className={classes.card}>
      <CardContent>
        <Box>
          <Box
            my={2}
            sx={{
              alignItems: 'center',
              display: 'flex',
              flexDirection: 'column',
            }}
          >
            <Avatar
              src={user?.avatar}
              sx={{
                height: 64,
                mb: 2,
                width: 64,
              }}
            />
            <Typography
              className={classes.texts}
              gutterBottom
              variant="h5"
            >
              {user.name}
            </Typography>
          </Box>

          <Divider color="#C3C3C3" />

          <Box mx={2}>
            <Grid container my={3} display="flex" alignItems="center" justifyContent="space-between">
              <Grid item md={2}>
                <Box>
                  <Typography
                    className={classes.texts}
                    variant="body2"
                    fontWeight="bold"
                  >
                    {user?.rank}
                  </Typography>
                </Box>
              </Grid>

              <Grid item md={8}>
                <Box sx={{
                  width: '100%',
                }}
                >
                  <Tooltip title={user?.points}>
                    <BorderLinearProgress
                      variant="determinate"
                      value={user?.points % 100}
                    />
                  </Tooltip>
                </Box>
              </Grid>

              <Grid item md={2}>
                <Box
                  sx={{ minWidth: 35 }}
                  display="flex"
                  justifyContent="center"
                  alignItems="center"
                >
                  <Typography
                    className={classes.texts}
                    variant="body2"
                    fontWeight="bold"
                  >
                    {user?.nextRank}
                  </Typography>
                </Box>
              </Grid>
            </Grid>
          </Box>
          <Divider color="#C3C3C3" />
        </Box>
      </CardContent>
      <CardActions>
        <Button fullWidth onClick={() => openProfile(user?.profileUrl)}>
          <SVG src={icoSteam} />
        </Button>
      </CardActions>
    </Card>
  )
}

AccountProfile.propTypes = {
  user: PropTypes.object.isRequired,
}

export default AccountProfile
