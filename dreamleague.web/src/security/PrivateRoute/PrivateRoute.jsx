import React from 'react'
import PropTypes from 'prop-types'
import { Redirect, Route } from 'react-router-dom'
import useSecurity from 'security/useSecurity'

const PrivateRoute = (props) => {
  const {
    layout: Layout,
    component: Component,
    ...rest
  } = props
  const { isLogged } = useSecurity()
  if (!isLogged()) { return <Redirect to="/login" /> }

  return (
    <Route
      {...rest}
      render={() => (
        <Layout>
          <Component />
        </Layout>
      )}
    />
  )
}

PrivateRoute.propTypes = {
  functionality: PropTypes.string || PropTypes.array,
  component: PropTypes.any,
  layout: PropTypes.any.isRequired,
}

PrivateRoute.defaultProps = {
  component: undefined,
  functionality: undefined,
}

export default PrivateRoute
