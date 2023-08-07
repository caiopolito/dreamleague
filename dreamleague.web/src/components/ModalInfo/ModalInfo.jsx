import React from 'react'
import PropTypes from 'prop-types'
import {
  Box,
  Backdrop,
  Fade,
  Modal,
  Paper,
} from '@mui/material'
import { makeStyles } from '@mui/styles'
import CloseIcon from '@mui/icons-material/Close'

const useStyles = makeStyles((theme) => ({
  modal: {
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    margin: theme.spacing(1),
    height: `calc(100% - ${theme.spacing(2)})`,
  },
  paper: {
    width: '66%',
    maxHeight: '100%',
    position: 'relative',
    padding: theme.spacing(3),
    [theme.breakpoints.down('sm')]: {
      width: '96%',
    },
  },

  close: {
    cursor: 'pointer',
    position: 'absolute',
    top: theme.spacing(2),
    right: theme.spacing(2),
  },
}))

const ModalInfo = ({
  children, open, onClose, close, id, ...props
}) => {
  const classes = useStyles()
  return (
    <Modal
      closeAfterTransition
      BackdropComponent={Backdrop}
      BackdropProps={{
        timeout: 500,
      }}
      open={open}
      {...props}
    >
      <Fade in={open}>
        <Box id={id} className={classes.modal}>
          <Paper className={classes.paper}>
            {close && (
              <CloseIcon title="Fechar" className={classes.close} onClick={onClose} />
            )}
            {children}
          </Paper>
        </Box>
      </Fade>
    </Modal>
  )
}

ModalInfo.propTypes = {
  open: PropTypes.bool,
  close: PropTypes.bool,
  onClose: PropTypes.func,
  children: PropTypes.oneOfType([PropTypes.element, PropTypes.array])
    .isRequired,
  id: PropTypes.string,
}

ModalInfo.defaultProps = {
  id: undefined,
  open: false,
  close: true,
  onClose: () => { },
}

export default ModalInfo
