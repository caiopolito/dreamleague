---
id: getting-started
title: Getting Started
sidebar_position: 1
slug: /getting-started
---

# Getting Started

DreamLeague is a self-hosted, third-party matchmaking platform for CS2. Players authenticate via Steam, queue for matches, get assigned to a dedicated CS2 game server, and match stats are recorded automatically via the MatchZy plugin's webhook callbacks.

## What's included

| Component | Description |
|---|---|
| `dreamleague.api` | ASP.NET Core 6 REST API + SignalR hubs |
| `dreamleague.app` | React 17 SPA |
| `dreamleague.gameserver` | Dockerized CS2 server (SteamCMD + Metamod + CounterStrikeSharp + MatchZy) |
| `dreamleague.main.db` | MSSQL database |
| `dreamleague.matches.db` | MongoDB (match documents consumed by the CS2 plugin) |

## Prerequisites

- [Docker](https://docs.docker.com/get-docker/) and Docker Compose
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) (for running the API locally)
- [Node.js 16+](https://nodejs.org/) and [Yarn](https://yarnpkg.com/) (for running the frontend locally)
- A [Steam Web API key](https://steamcommunity.com/dev/apikey)
- A CS2 game server token from [Valve's Game Server Account page](https://steamcommunity.com/dev/managegameservers)

## Next steps

Follow the [Setup guide](./setup) for step-by-step installation instructions.
