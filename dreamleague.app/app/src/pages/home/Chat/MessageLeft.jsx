import React from 'react'
import PropTypes from 'prop-types'

import {
  Grid,
  ListItem,
  ListItemText,
} from '@mui/material'

const MessageLeft = ({ styles, message, hour }) => (
  <ListItem key="1">
    <Grid container>
      <Grid item xs={12}>
        <ListItemText
          align="right"
          primary={message}
          className={styles.messageLeft}
        />
      </Grid>
      <Grid item xs={12}>
        <ListItemText align="left" secondary={hour} />
      </Grid>
    </Grid>
  </ListItem>
)

MessageLeft.propTypes = {
  message: PropTypes.string.isRequired,
  hour: PropTypes.string.isRequired,
  styles: PropTypes.object.isRequired,
}

export default MessageLeft
