import React from 'react'
import { Switch, Route, Redirect } from 'react-router-dom'

import Profile from './pages/profile/Profile'
import Lobby from './pages/home/Lobby'
import Login from './pages/login/Login'
import SteamCallback from './pages/callbacks/SteamCallback'
import MainLayout from './layouts/MainLayout'
import LoginLayout from './layouts/LoginLayout'
import PrivateRoute from './security/PrivateRoute'

const Routes = () => (
  <Switch>
    <Route exact path="/login">
      <LoginLayout>
        <Login />
      </LoginLayout>
    </Route>
    <Route component={SteamCallback} path="/callback" />

    <PrivateRoute exact layout={MainLayout} component={Lobby} path="/lobby" />
    <PrivateRoute exact layout={MainLayout} component={Lobby} path="/championship" />
    <PrivateRoute exact layout={MainLayout} component={Lobby} path="/pontuation" />
    <PrivateRoute exact layout={MainLayout} component={Lobby} path="/friends" />
    <PrivateRoute exact layout={MainLayout} component={Profile} path="/profile" />

    <Route exact path="/" render={() => <Redirect to="/lobby" />} />
    <Redirect to="lobby" />
  </Switch>
)

export default Routes
