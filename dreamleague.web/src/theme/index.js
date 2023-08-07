import { createTheme, responsiveFontSizes } from '@mui/material/styles'

export const colors = {
  primary: {
    main: '#39074d',
  },
  secondary: {
    main: '#01c8d0',
    contrastText: '#8B7676',
  },
  text: {
    main: '#cfcfcf',
  },
  background: {
    main: '#181b25',
  },
  backgroundSecundary: {
    main: 'linear-gradient(137deg, rgba(5,4,31,1) 21%, rgb(38 13 48) 100%, rgb(5 5 20) 100%);',
  },
  lobby: {
    create: '#4caf50',
    enter: '#ffc107',
    invite: '#3f51b5',
  },
  login: {
    login: '#3f51b5',
  },
}

const fontFamily = ['Poppins', 'sans-serif'].join(',')

const textStyle = {
  fontFamily,
  fontWeight: 600,
  color: '#595959',
}

const typography = {
  fontFamily: ['Open Sans', 'sans-serif'].join(','),
  h1: textStyle,
  h2: textStyle,
  h3: textStyle,
  h4: textStyle,
  h5: textStyle,
  h6: textStyle,
  subtitle1: {
    fontFamily,
  },
  subtitle2: {
    fontFamily,
    fontSize: window.innerWidth < 1300 ? '.8rem' : '.875rem',
  },
  body1: {
    fontSize: '.85rem',
  },
  body2: {
    fontSize: window.innerWidth < 1300 ? '.75rem' : '.85rem',
  },
}

export const customTheme = () => {
  const theme = createTheme({
    palette: {
      ...colors,
    },
    sizes: {
      header: 80,
      avatar: 56,
    },
    typography,
    components: {
      MuiButton: {
        styleOverrides: {
          root: {
            height: 45,
            fontFamily,
            fontWeight: 100,
            textTransform: 'none',
          },
          contained: {
            backgroundImage: 'linear-gradient(to right, #6441A5 0%, #2a0845 51%, #6441A5 100%)',
            backgroundSize: '200% auto',
            transition: '.5s',
            '&:hover': {
              backgroundPosition: 'right center',
              color: '#fff',
              textDecoration: 'none',
            },
          },
          sizeLarge: {
            padding: '16px 22px',
          },
        },
      },
      MuiPaper: {
        styleOverrides: {
          root: {
            boxShadow: '3px 3px 10px #00000029',
            borderRadius: 14,
          },
        },
      },
      MuiAppBar: {
        styleOverrides: {
          root: {
            background: colors.backgroundSecundary.main,
            borderRadius: 0,
          },
        },
      },
      MuiFormLabel: {
        styleOverrides: {
          root: {
            fontSize: 14,
          },
          colorSecondary: {
            '&.Mui-focused': {
              color: '#595959',
            },
          },
        },
      },
      MuiTypography: {
        styleOverrides: {
          root: {
            color: colors.text.main,
          },
        },
      },
      MuiInput: {
        styleOverrides: {
          underline: {
            '&:hover:not($disabled):not($focused):not($error):before': {
              borderBottomColor: '#949494',
            },
          },
          colorSecondary: {
            '&$focused::after': {
              borderBottomColor: '#595959',
            },
            '&.MuiInput-underline::after': {
              borderBottomColor: '#595959',
            },
          },
        },
      },
      MuiOutlinedInput: {
        styleOverrides: {
          root: {
            '&.Mui-focused': {
              '& .MuiOutlinedInput-notchedOutline': {
                border: '1px solid #208BFF',
              },
            },
          },
        },
      },
      MuiFormHelperText: {
        styleOverrides: {
          root: {
            margin: 0,
          },
        },
      },
      MuiFormControl: {
        styleOverrides: {
          root: {
            width: '100%',
          },
        },
      },
    },
    breakpoints: {
      values: {
        xs: 0,
        sm: 600,
        md: 960,
        lg: 1280,
        xl: 1600,
      },
    },
  })

  return responsiveFontSizes(theme)
}

export default customTheme
