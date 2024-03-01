import React from 'react'
import PropTypes from 'prop-types'
import {
  Box,
  Grid,
  Button,
  Dialog,
  Backdrop,
  Typography,
  DialogContent,
} from '@mui/material'
import { makeStyles } from '@mui/styles'
import CloseIcon from '@mui/icons-material/Close'

import { colors } from 'theme'
import icoError from '../../assets/ico/ico-alert-danger.svg'
import icoAlert from '../../assets/ico/ico-alert-warning.svg'
import icoSuccess from '../../assets/ico/ico-alert-success.svg'

const useStyles = makeStyles((theme) => ({
  btn: {
    width: '100%',
  },
  boxBtn: {
    margin: '8px !important',
  },
  close: {
    cursor: 'pointer',
    position: 'absolute',
    top: theme.spacing(2),
    right: theme.spacing(2),
  },
  dialog: {
    '& .MuiDialog-paper': {
      borderRadius: '16px',
    },
  },
  modal: {
    paddingBottom: theme.spacing(4),
    overflow: 'auto',
  },
  bottom: {
    margin: '0 !important',
    justifyContent: 'center',
  },
}))

const MessageBox = (props) => {
  const classes = useStyles()
  const {
    opened,
    type,
    title,
    text,
    thumb,
    labelPrimary,
    labelSecondary,
    handleClose,
    handlePrimary,
    handleSecondary,
    children,
    buttonPosition,
    maxWidth,
    hasCloseButton,
  } = props

  return (
    <Dialog
      open={opened}
      maxWidth={maxWidth}
      className={classes.dialog}
      BackdropComponent={Backdrop}
      BackdropProps={{
        timeout: 500,
      }}
      disableEscapeKeyDown
      onClose={(_, reason) => {
        if (reason !== 'backdropClick') { handleClose() }
      }}
      fullWidth
    >
      <DialogContent className={classes.modal}>
        {!!hasCloseButton && (
          <CloseIcon title="Fechar" className={classes.close} onClick={handleClose} />
        )}

        {(thumb || type) && (
          <Box mt={2} mb={3} display="flex" justifyContent="center">
            {type === 'error' && <img src={icoError} alt="thumb" />}
            {type === 'warning' && <img src={icoAlert} alt="thumb" />}
            {type === 'success' && <img src={icoSuccess} alt="thumb" />}
            {type === 'info' && thumb && <img src={thumb} alt="thumb" />}
          </Box>
        )}

        {title && (
          <Box mt={2} mb={3}>
            <Typography align="center" variant="h5" color={colors.text.secundary}>
              {title}
            </Typography>
          </Box>
        )}

        {text && (
          <Box mt={2} mb={3} align="center">
            <Typography
              color={colors.text.secundary}
              align="center"
              dangerouslySetInnerHTML={{ __html: text }}
            />
          </Box>
        )}

        {children}

        {buttonPosition === 'nextTo' && (
          <Grid
            pb={2}
            container
            justify="center"
            className={classes.bottom}
          >
            {handleSecondary && (
              <Grid item lg={5} sm={6} xs={12} className={classes.boxBtn}>
                <Button
                  color="primary"
                  variant="outlined"
                  className={classes.btn}
                  onClick={handleSecondary}
                  title={labelSecondary}
                >
                  {labelSecondary}
                </Button>
              </Grid>
            )}

            {handlePrimary && (
              <Grid item lg={5} sm={6} xs={12} className={classes.boxBtn}>
                <Button
                  color="primary"
                  variant="contained"
                  title={labelPrimary}
                  className={classes.btn}
                  onClick={handlePrimary}
                >
                  {labelPrimary}
                </Button>
              </Grid>
            )}
          </Grid>
        )}

        {buttonPosition === 'below' && (
          <Grid
            pb={3}
            container
            justify="center"
            className={classes.bottom}
          >
            {handlePrimary && (
              <Grid item lg={7} sm={8} xs={12} className={classes.boxBtn}>
                <Button
                  color="primary"
                  variant="contained"
                  title={labelPrimary}
                  className={classes.btn}
                  onClick={handlePrimary}
                >
                  {labelPrimary}
                </Button>
              </Grid>
            )}

            {handleSecondary && (
              <Grid item lg={7} sm={8} xs={12} className={classes.boxBtn}>
                <Button
                  color="primary"
                  variant="outlined"
                  title={labelSecondary}
                  className={classes.btn}
                  onClick={handleSecondary}
                >
                  {labelSecondary}
                </Button>
              </Grid>
            )}
          </Grid>
        )}
      </DialogContent>
    </Dialog>
  )
}

MessageBox.propTypes = {
  opened: PropTypes.bool,
  children: PropTypes.element,
  buttonPosition: PropTypes.oneOf(['nextTo', 'below']),
  maxWidth: PropTypes.oneOf(['lg', 'md', 'sm', 'xl', 'xs']),
  type: PropTypes.oneOf(['success', 'warning', 'error', 'info']),
  handlePrimary: PropTypes.func,
  handleSecondary: PropTypes.func,
  handleClose: PropTypes.func.isRequired,
  thumb: PropTypes.string,
  title: PropTypes.string,
  text: PropTypes.string,
  labelPrimary: PropTypes.string,
  labelSecondary: PropTypes.string,
  hasCloseButton: PropTypes.bool,
}

MessageBox.defaultProps = {
  type: 'info',
  maxWidth: 'sm',
  buttonPosition: 'nextTo',
  opened: false,
  text: undefined,
  thumb: undefined,
  title: undefined,
  labelPrimary: 'Confirmar',
  labelSecondary: 'Cancelar',
  handlePrimary: undefined,
  handleSecondary: undefined,
  children: undefined,
  hasCloseButton: true,
}

export default MessageBox
