---
id: setup
title: Setup
sidebar_position: 2
---

# Setup

## 1. Clone the repository

```bash
git clone https://github.com/caiopolito/dreamleague.git
cd dreamleague
```

## 2. Configure environment variables

Copy the example env file and fill in your Steam API key:

```bash
cp .env.example .env
```

Edit `.env`:

```env
STEAM_API_KEY=your_steam_api_key_here
```

## 3. Configure the API

Copy the example config and fill in your values:

```bash
cp dreamleague.api/api/src/dreamleague.api/appsettings.Example.json \
   dreamleague.api/api/src/dreamleague.api/appsettings.json
```

Edit `appsettings.json` and set your `SteamApiKey`. The connection strings already point to the Docker container hostnames and work out of the box.

## 4. Start the infrastructure

```bash
docker compose pull gameserver
docker compose up -d
```

This starts the CS2 game server, MSSQL, and MongoDB. On the first run Docker copies the full CS2 installation from the gameserver image into a named volume — this is a one-time operation and can take a few minutes.

## 5. Run the API and frontend locally

The API and app are intentionally excluded from `docker-compose.yml` so you can run them with hot reload during development.

**API:**
```bash
cd dreamleague.api/api/src/dreamleague.api
dotnet run
```

Swagger UI is available at `http://localhost:5000/api-docs`.

**Frontend:**
```bash
cd dreamleague.app/app
yarn install
yarn start
```

The app runs at `http://localhost:3000`.

## 6. Verify the setup

1. Open `http://localhost:3000` and log in with Steam
2. The lobby page should load and the queue should be joinable
3. Check that the CS2 server appears in the server list (it's seeded automatically pointing to `172.20.128.1:27015`)
