# DreamLeague

A self-hosted, third-party matchmaking platform for CS2. Players authenticate via Steam, queue for matches, get assigned to a dedicated CS2 game server, and match stats are recorded automatically via the MatchZy plugin's webhook callbacks.

**[Documentation](https://caiopolito.github.io/dreamleague/)**

---

## Components

| Directory | Description |
|---|---|
| `dreamleague.api/` | ASP.NET Core 6 REST API + SignalR hubs |
| `dreamleague.app/` | React 17 SPA |
| `dreamleague.gameserver/` | Dockerized CS2 server (SteamCMD + Metamod + CounterStrikeSharp + MatchZy) |
| `dreamleague.main.db/` | MSSQL — players, teams, matches, stats |
| `dreamleague.matches.db/` | MongoDB — match documents consumed by the CS2 plugin |
| `dreamleague.docs/` | Docusaurus documentation site |

## Prerequisites

- [Docker](https://docs.docker.com/get-docker/) and Docker Compose
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Node.js 16+](https://nodejs.org/) and [Yarn](https://yarnpkg.com/)
- A [Steam Web API key](https://steamcommunity.com/dev/apikey)
- A GitHub account with `read:packages` scope (to restore the [`dreamleague.common`](https://github.com/caiopolito/dreamleague.common) NuGet package)

## Quick start

### 1. Clone and configure

```bash
git clone https://github.com/caiopolito/dreamleague.git
cd dreamleague
cp .env.example .env
```

Edit `.env` and fill in all required values (Steam API key, database passwords, certificate password).

### 2. Configure the API

```bash
cp dreamleague.api/api/src/dreamleague.api/appsettings.Example.json \
   dreamleague.api/api/src/dreamleague.api/appsettings.json
```

Update `appsettings.json` with your Steam API key and passwords matching `.env`.

### 3. Authenticate with GitHub Packages

The API depends on [`dreamleague.common`](https://github.com/caiopolito/dreamleague.common), published to GitHub Packages. Set your GitHub personal access token (with `read:packages` scope) before restoring:

```bash
export GITHUB_TOKEN=ghp_your_token_here
```

### 4. Start the infrastructure

```bash
docker compose up -d
```

This starts the CS2 game server, MSSQL, and MongoDB. On first run, CS2 is downloaded into a named volume (~35 GB) — this is a one-time operation.

### 5. Run the API and frontend

```bash
# API (hot reload)
cd dreamleague.api/api/src/dreamleague.api
dotnet run
# Swagger UI → http://localhost:5000/api-docs

# Frontend (hot reload)
cd dreamleague.app/app
yarn install && yarn start
# App → http://localhost:3000
```

## Architecture

```
Browser  ──►  React SPA  ──►  ASP.NET Core API  ──►  MSSQL
                  │                  │                MongoDB
                  │            SignalR hubs
                  │           (queue / chat)
                  │
              CS2 server  ◄──  RCON  ──  API
                  │
              MatchZy plugin  ──►  HTTP callbacks  ──►  API
```

The matchmaking flow: players join the queue via SignalR → when enough players are present the API picks an available CS2 server → sends `dreamleague_loadmatch` via RCON → the MatchZy plugin loads the match from MongoDB → match events are POSTed back to the API as they happen.

Full architecture details in the [docs](https://caiopolito.github.io/dreamleague/architecture).

## Development

```bash
# Run all tests (API)
cd dreamleague.api/api
dotnet test

# Lint (frontend)
cd dreamleague.app/app
yarn lint

# Force a CS2 update without wiping the volume
CS2_FORCE_UPDATE=1 docker compose up -d gameserver
```

See the [docs](https://caiopolito.github.io/dreamleague/) for the full configuration reference, database schema, and server command list.

## License

MIT — see [LICENSE](LICENSE).
