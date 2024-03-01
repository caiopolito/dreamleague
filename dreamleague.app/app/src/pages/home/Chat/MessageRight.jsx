import React from 'react'
import PropTypes from 'prop-types'

import {
  Grid,
  ListItem,
  ListItemText,
} from '@mui/material'

const MessageRight = ({ styles, message, hour }) => (
  <ListItem key="1">
    <Grid container>
      <Grid item xs={12}>
        <ListItemText
          align="right"
          primary={message}
          className={styles.messageRight}
        />
      </Grid>
      <Grid item xs={12}>
        <ListItemText align="right" secondary={hour} />
      </Grid>
    </Grid>
  </ListItem>
)

MessageRight.propTypes = {
  message: PropTypes.string.isRequired,
  hour: PropTypes.string.isRequired,
  styles: PropTypes.object.isRequired,
}

export default MessageRight
