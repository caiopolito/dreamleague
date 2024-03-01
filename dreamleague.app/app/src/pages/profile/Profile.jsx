import React from 'react'
import {
  Box, Avatar,
} from '@mui/material'
import useSecurity from 'security/useSecurity'
import { useTheme } from '@emotion/react'
import useStyles from '../../layouts/style'

const Profile = () => {
  const { user } = useSecurity()
  const classes = useStyles()
  const theme = useTheme()

  return (
    <Box display="flex" alignItems="center" justifyContent="center" height={1}>
      <Box mx={2}>
        <Avatar
          src={user?.avatar}
          alt="Profile"
          className={classes.avatar}
          sx={{ width: theme.sizes.avatar, height: theme.sizes.avatar }}
        />
      </Box>
    </Box>
  )
}

export default Profile
