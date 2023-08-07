import React from 'react'
import PropTypes from 'prop-types'
import { styled } from '@mui/material/styles'
import { colors } from 'theme'
import { makeStyles } from '@mui/styles'

import {
  Box,
  Paper,
  Table,
  Avatar,
  useTheme,
  TableRow,
  TableCell,
  TableHead,
  TableBody,
  Typography,
  TableContainer,
  Tooltip,
} from '@mui/material'

const useStyles = makeStyles(() => ({
  avatar: {
    '&.MuiAvatar-root': {
      border: `2px solid ${colors.primary.main}`,
      width: 40,
      height: 40,
    },
  },
  tableContainer: {
    '&.MuiTableCell-root': {
      color: '#fff !important',
    },

    backgroundColor: 'unset !important',
    color: '#fff !important',
  },
  vsIcon: {
    fontSize: '30px',
    fontWeight: 900,
    color: 'cornsilk',
    border: '1px solid #80808070',
    borderRadius: 35,
    width: 58,
    display: 'flex',
    justifyContent: 'center',
    padding: 8,
    alignItems: 'center',
  },
}))

const HistoryDetail = ({ matchObject }) => {
  const theme = useTheme()
  const classes = useStyles()

  const StyledTableRow = styled(TableRow)(() => ({
    '&:nth-of-type(odd)': {
      backgroundColor: 'rgb(36 12 47)',
    },
    // hide last border
    '&:last-child td, &:last-child th': {
      border: 0,
    },
  }))

  const styleCell = { color: '#fff' }

  const TableCellColor = (text, tooltip) => (
    <Tooltip title={tooltip}>
      <TableCell align="right" sx={styleCell}>
        {text}
      </TableCell>
    </Tooltip>
  )

  return (
    <Box borderTop="1px solid gray">
      <Box p={3} textAlign="center">
        <Typography variant="h4">
          {matchObject?.teamOne}
        </Typography>
      </Box>

      <TableContainer component={Paper} className={classes.tableContainer}>
        <Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
          <TableHead>
            <TableRow>
              <TableCell />
              {TableCellColor('K', 'Kills')}
              {TableCellColor('A', 'Assistências')}
              {TableCellColor('D', 'Mortes')}
              {TableCellColor('DIFF', 'Diferença entre kills e mortes')}
              {TableCellColor('ADR', 'Dano médio por round')}
              {TableCellColor('KDR', 'Kill/Death ratio')}
              {TableCellColor('S', 'Sobreviveu ao round')}
            </TableRow>
          </TableHead>

          <TableBody>
            {matchObject?.playersTeamOne?.map((rowOne, indexPlayerOne) => (
              <StyledTableRow
                key={`player-one-${indexPlayerOne}`}
                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
              >
                <TableCell component="th" scope="row" sx={styleCell}>
                  <Box display="flex" alignItems="center">
                    <Avatar
                      src={rowOne.avatar}
                      alt="Avatar"
                      className={classes.avatar}
                      sx={{ width: theme.sizes.avatar, height: theme.sizes.avatar }}
                    />

                    <Box mx={0.5} />

                    {rowOne.name}
                  </Box>
                </TableCell>
                {TableCellColor(rowOne.kills)}
                {TableCellColor(rowOne.assists)}
                {TableCellColor(rowOne.deaths)}
                {TableCellColor(rowOne.diff)}
                {TableCellColor(rowOne.adr)}
                {TableCellColor(rowOne.kdr)}
                {TableCellColor(rowOne.s)}
              </StyledTableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>

      <Box display="flex" justifyContent="center" mt={3}>
        <Box className={classes.vsIcon}>
          VS
        </Box>
      </Box>

      <Box p={3} textAlign="center">
        <Typography variant="h4">
          {matchObject?.teamTwo}
        </Typography>
      </Box>

      <TableContainer component={Paper} className={classes.tableContainer}>
        <Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
          <TableHead>
            <TableRow>
              <TableCell />
              {TableCellColor('K', 'Kills')}
              {TableCellColor('A', 'Assistências')}
              {TableCellColor('D', 'Mortes')}
              {TableCellColor('DIFF', 'Diferença entre kills e mortes')}
              {TableCellColor('ADR', 'Dano médio por round')}
              {TableCellColor('KDR', 'Kill/Death ratio')}
              {TableCellColor('S', 'Sobreviveu ao round')}
            </TableRow>
          </TableHead>

          <TableBody>
            {matchObject?.playersTeamTwo?.map((rowOne, indexPlayerOne) => (
              <StyledTableRow
                key={`player-one-${indexPlayerOne}`}
                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
              >
                <TableCell component="th" scope="row" sx={styleCell}>
                  <Box display="flex" alignItems="center">
                    <Avatar
                      src={rowOne.avatar}
                      alt="Avatar"
                      className={classes.avatar}
                      sx={{ width: theme.sizes.avatar, height: theme.sizes.avatar }}
                    />

                    <Box mx={0.5} />

                    {rowOne.name}
                  </Box>
                </TableCell>
                {TableCellColor(rowOne.kills)}
                {TableCellColor(rowOne.assists)}
                {TableCellColor(rowOne.deaths)}
                {TableCellColor(rowOne.diff)}
                {TableCellColor(rowOne.adr)}
                {TableCellColor(rowOne.kdr)}
                {TableCellColor(rowOne.s)}
              </StyledTableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </Box>
  )
}

HistoryDetail.propTypes = {
  matchObject: PropTypes.object.isRequired,
}

export default HistoryDetail
