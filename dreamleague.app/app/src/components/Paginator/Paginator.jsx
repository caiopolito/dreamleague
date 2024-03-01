import React, { useState, useEffect } from 'react'
import PropTypes from 'prop-types'
import { Box, Pagination } from '@mui/material'
import { makeStyles } from '@mui/styles'

const useStyles = makeStyles((theme) => ({
  paginacao: {
    '& .MuiPaginationItem-page.Mui-selected': {
      backgroundColor: '#4a4a4a',
      color: '#fff',
    },
    '& .MuiPaginationItem-icon': {
      color: theme.palette.primary.main,
    },

    '& .MuiPaginationItem-page.Mui-disabled ': {
      '& .MuiPaginationItem-icon': {
        color: '#4a4a4a',
      },
    },
  },
}))

const Paginator = ({
  totalResults,
  changePaginator,
  page,
  showFirstButton,
  showLastButton,
}) => {
  const RPP = 10
  const classes = useStyles()

  const [pagePagination, setPagePagination] = useState(page)
  const handleChange = (_, value) => {
    setPagePagination(value)
    changePaginator(value)
  }

  useEffect(() => {
    setPagePagination(page)
  }, [page])

  return totalResults > RPP ? (
    <Box display="flex" justifyContent="center" alignItems="center" my={4}>
      <Pagination
        count={Math.ceil(totalResults / RPP)}
        className={classes.paginacao}
        page={pagePagination}
        onChange={handleChange}
        showFirstButton={showFirstButton}
        showLastButton={showLastButton}
        justify="center"
      />
    </Box>
  ) : (
    ''
  )
}
Paginator.propTypes = {
  totalResults: PropTypes.number.isRequired,
  changePaginator: PropTypes.func.isRequired,
  page: PropTypes.number,
  showFirstButton: PropTypes.bool,
  showLastButton: PropTypes.bool,
}

Paginator.defaultProps = {
  page: 1,
  showFirstButton: false,
  showLastButton: false,
}

export default Paginator
