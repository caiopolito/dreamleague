import React from 'react'
import { Switch, Route, Redirect } from 'react-router-dom'

import ChampionshipDetail from 'pages/championships/ChampionshipDetail'
import Lobby from './pages/home/Lobby'
import Friends from './pages/friends/Friends'
import Community from './pages/community/Community'
import History from './pages/history/History'
import ChampionsShip from './pages/championships/ChampionsShip'
import Teams from './pages/teams/Teams'
import Login from './pages/login/Login'
import Profile from './pages/profile/ProfilePage'
import SteamCallback from './pages/callbacks/SteamCallback'
import MainLayout from './layouts/MainLayout'
import LoginLayout from './layouts/LoginLayout'
import PrivateRoute from './security/PrivateRoute'
import TeamDetail from './pages/teams/TeamDetail'

const Routes = () => (
  <Switch>
    <PrivateRoute exact layout={MainLayout} component={Lobby} path="/lobby" />

    <PrivateRoute exact layout={MainLayout} component={ChampionsShip} path="/championships" />

    <PrivateRoute exact layout={MainLayout} component={Teams} path="/teams" />

    <PrivateRoute exact layout={MainLayout} component={Friends} path="/friends" />

    <PrivateRoute exact layout={MainLayout} component={Community} path="/community" />

    <PrivateRoute exact layout={MainLayout} component={History} path="/history" />

    <PrivateRoute exact layout={MainLayout} component={Profile} path="/profile/:id" />

    <PrivateRoute exact layout={MainLayout} component={TeamDetail} path="/team/:id" />

    <PrivateRoute exact layout={MainLayout} component={ChampionshipDetail} path="/championship/:id" />

    <Route path="/callback" component={SteamCallback} />

    <Route exact path="/login">
      <LoginLayout>
        <Login />
      </LoginLayout>
    </Route>

    <Route exact path="/">
      <Redirect to="/lobby" />
    </Route>
  </Switch>
)

export default Routes
