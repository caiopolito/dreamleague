import { createTheme, responsiveFontSizes } from '@mui/material/styles'

export const colors = {
  primary: {
    main: '#39074d',
  },
  secondary: {
    main: '#D3D3D3',
  },
  tertiary: {
    main: '#01c8d0',
  },
  text: {
    main: '#cfcfcf',
    secundary: '#000000cc',
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
    close: '#f44336',
  },
  login: {
    login: '#3f51b5',
  },
  notification: {
    color: '#ed143dbd',
  },
}

const fontFamily = ['Poppins', 'sans-serif'].join(',')

const textStyle = {
  fontFamily,
  fontWeight: 600,
  color: '#595959',
}

export const typography = {
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
            backgroundImage: 'linear-gradient(to right, #40207a 0%, #2a0845 51%, #27144a 100%)',
            backgroundSize: '200% auto',
            transition: '.5s',
            '&:hover': {
              backgroundPosition: 'right center',
              color: '#fff',
              textDecoration: 'none',
            },
          },
          outlined: {
            border: `1px solid ${colors.secondary.main}`,
            color: colors.secondary.main,
            transition: '.5s',
            '&:hover': {
              border: `1px solid ${colors.secondary.main}`,
            },
          },
          text: {
            color: colors.secondary.main,
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
      MuiArowBackIcon: {
        styleOverrides: {
          root: {
            color: '#c3c3c3',
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
      MuiTextField: {
        styleOverrides: {
          root: {
            input: {
              color: '#C3C3C3',
            },
            label: {
              color: '#C3C3C3',
            },
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
      MuiSkeleton: {
        styleOverrides: {
          root: {
            backgroundColor: 'rgb(103 58 183 / 11%)',
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
