/* eslint-disable no-dupe-keys */
import React, { useEffect, useState } from 'react'
import PropTypes from 'prop-types'
import {
  Box,
  Divider,
  Typography,
  LinearProgress,
} from '@mui/material'
import SVG from 'react-inlinesvg'

import useSecurity from 'security/useSecurity'

import icoCoins from 'assets/header/ico-coins.svg'
import Header from 'components/Header/Header'

import useStyles from './style'

const MainLayout = (props) => {
  const { children } = props

  const [progress, setProgress] = useState(0)

  const classes = useStyles()
  const { getUser } = useSecurity()
  const user = getUser()

  const lvlTwoPoints = 2000

  useEffect(() => {
    const currentPoints = user?.points || 0
    setProgress((currentPoints / lvlTwoPoints) * 100)
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [])

  return (
    <>
      <Box px={4} className={classes.container}>
        <Header />

        <Box width={1} className={classes.subBar}>
          <Box display="flex" alignItems="center" mr={2}>
            <Box mr={2}>
              <SVG
                width={30}
                height={30}
                src={icoCoins}
              />
            </Box>

            <Box>
              <Typography color="text">
                {user?.coins}
              </Typography>
            </Box>
          </Box>

          <Divider orientation="vertical" variant="middle" flexItem className={classes.dividerSubBar} />

          <Box sx={{ width: 300 }} mr={2}>
            <Box display="flex" alignItems="center">
              <Box mx={2}>
                <Typography color="secondary">
                  lvl 1
                </Typography>
              </Box>

              <Box display="flex" flexDirection="column">

                <Box textAlign="center">
                  <Typography color="text">
                    {user?.points}

                    {' / '}

                    {lvlTwoPoints}
                  </Typography>
                </Box>

                <LinearProgress
                  className={classes.lineProgress}
                  color="secondary"
                  variant="determinate"
                  value={progress}
                />
              </Box>

              <Box mx={2}>
                <Typography color="secondary">
                  lvl 2
                </Typography>
              </Box>
            </Box>
          </Box>
        </Box>

        <Box display="flex" width="100%" mt={10}>
          <Box width={1}>
            {children}
          </Box>
        </Box>
      </Box>

      <Box textAlign="center" className={classes.footer} component="footer">
        <Typography
          variant="caption"
          fontSize={10}
          lineHeight={0}
          fontWeight={200}
        >
          Copiadores de código Ltda. Brooklyn 1984 @
        </Typography>
      </Box>
    </>
  )
}

MainLayout.propTypes = {
  children: PropTypes.element.isRequired,
}

export default MainLayout
