import { makeStyles } from '@mui/styles'
import { colors } from 'theme/index'

export default makeStyles((theme) => ({
  container: {
    display: 'flex',
    maxWidth: '100vw',
    minHeight: '100vh',
    backgroundColor: colors.background.main,
  },
  avatar: {
    '&.MuiAvatar-root': {
      border: `2px solid ${colors.secondary.main}`,
    },
  },
  navIcon: {
    '&.MuiListItemIcon-root': {
      minWidth: 40,
      display: 'flex',
      flexDirection: 'column',
      alignItems: 'center',
    },
  },
  navItem: {
    width: '130px !important',
    alignItems: 'center !important',
    justifyContent: 'center !important',
    '&.MuiButtonBase-root': {
      borderRadius: 0,
      height: theme.sizes.header,
      '&:after': {
        content: '""',
        position: 'absolute',
        height: 2,
        width: '0%',
        left: 0,
        bottom: 0,
        borderRadius: 6,
        backgroundColor: colors.secondary.main,
        transition: 'width 100ms ease-in',
      },
      '&:hover:after': {
        width: '100%',
      },
    },
    '&.MuiButtonBase-root.active': {
      borderBottom: `1px solid ${colors.secondary.main}`,
    },
  },
  appBar: {
    display: 'flex',
    justifyContent: 'center',
    height: theme.sizes.header,
  },
  nav: {
    height: theme.sizes.header,
    '& .MuiList-root': {
      padding: '0px !important',
      display: 'flex',
    },
  },
  divider: {
    background: colors.secondary.main,
    height: '50px !important',
    marginTop: '15px !important',
    opacity: '0.3',
  },
  dividerSubBar: {
    background: colors.secondary.main,
    height: '38px !important',
    marginTop: '4px !important',
    opacity: '0.3',
  },
  subBar: {
    position: 'fixed',
    right: 0,
    borderRadius: 0,
    background: '#0705204a',
    padding: 14,
    color: '#fff',
    height: 40,
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'end',
    marginTop: 80,
    flexWrap: 'wrap',
  },
  lineProgress: {
    width: 180,
    backgroundColor: 'rgb(255 87 34 / 45%) !important',
  },
  flexAlignCenter: {
    display: 'flex',
    alignItems: 'center',
  },
  footer: {
    position: 'absolute',
    bottom: 0,
    background: 'linear-gradient(137deg, rgba(5,4,31,1) 21%, rgb(55 6 74) 100%, rgb(5 5 20) 100%)',
    width: '100%',
    height: 30,
    color: '#fff',
  },
}))
