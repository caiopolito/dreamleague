import React from 'react'
import { makeStyles } from '@mui/styles'
import {
  Box, Typography, Paper,
} from '@mui/material'

import FriendsHome from 'pages/home/Friends'

const useStyles = makeStyles(() => ({
  paper: {
    minHeight: 300,
    color: '#fff !important',
    padding: 16,
    backgroundColor: '#0705204a !important',
  },
}))

const Friends = () => {
  const classes = useStyles()

  return (
    <Box>
      <Box textAlign="center" mb={2} mt={1}>
        <Typography fontWeight="bold" variant="h5">
          Amigos:
        </Typography>
      </Box>

      <Paper className={classes.paper}>
        <Box mb={2}>
          <FriendsHome />
        </Box>
      </Paper>
    </Box>
  )
}

export default Friends
