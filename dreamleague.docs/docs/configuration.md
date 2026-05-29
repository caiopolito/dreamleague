---
id: configuration
title: Configuration
sidebar_position: 5
---

# Configuration

## Environment variables (`.env`)

The `.env` file at the repo root is loaded automatically by Docker Compose.

| Variable | Description |
|---|---|
| `STEAM_API_KEY` | Your Steam Web API key — get one at [steamcommunity.com/dev/apikey](https://steamcommunity.com/dev/apikey) |

Copy `.env.example` to `.env` to get started.

## API (`appsettings.json`)

Copy `appsettings.Example.json` to `appsettings.json` and edit the values below.

| Key | Description |
|---|---|
| `Authentication.SteamApiKey` | Same Steam API key as above |
| `ConnectionStrings.DefaultConnection` | MSSQL connection string (points to the Docker container by default) |
| `ConnectionStrings.MongoConnection` | MongoDB connection string (points to the Docker container by default) |
| `ConnectionStrings.MongoDatabaseName` | MongoDB database name (`dreamleague`) |
| `Queue.MinPlayers` | Total players required to start a match |
| `Queue.MinPlayersPerTeam` | Players per team |
| `CorsConfig` | Array of allowed origins for CORS |

## Frontend (`.env`)

Located at `dreamleague.app/app/.env`.

| Variable | Description |
|---|---|
| `REACT_APP_URL_API` | API base URL (e.g. `https://localhost:5001/`) |
| `REACT_APP_URL_HUB` | SignalR hub base URL (same as API) |
| `REACT_APP_URL` | Frontend URL (e.g. `http://localhost:3000/`) |

## Game server (Docker Compose / environment variables)

The CS2 server is configured entirely via environment variables passed through Docker Compose. All variables have sensible defaults in the `Dockerfile`.

| Variable | Default | Description |
|---|---|---|
| `CS2_SERVERNAME` | `[DREAMLEAGUE] #1 Private Matchmaking` | Server name shown in the browser |
| `CS2_PORT` | `27015` | Game port (TCP + UDP) |
| `CS2_RCONPW` | `changeme` | RCON password — **change this in production** |
| `CS2_PW` | `changeme` | Server join password |
| `CS2_MAXPLAYERS` | `10` | Maximum player slots |
| `CS2_FORCE_UPDATE` | `0` | Set to `1` to trigger a SteamCMD update on next start |
| `TV_ENABLE` | `0` | Enable SourceTV |
| `SRCDS_TOKEN` | _(empty)_ | Game server token from Valve |
