import React from 'react'
import PropTypes from 'prop-types'
import { Route, useHistory } from 'react-router-dom'
import useSecurity from 'security/useSecurity'

const PrivateRoute = (props) => {
  const {
    layout: Layout,
    component: Component,
    ...rest
  } = props
  const { user } = useSecurity()
  const history = useHistory()

  if (!user) {
    history.push('/login')

    return <div />
  }

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
  path: PropTypes.string.isRequired,
}

PrivateRoute.defaultProps = {
  component: undefined,
  functionality: undefined,
}

export default PrivateRoute
