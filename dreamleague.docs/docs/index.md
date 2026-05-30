---
id: index
title: DreamLeague
sidebar_position: 0
slug: /
---

# DreamLeague

DreamLeague is a self-hosted, third-party matchmaking platform for CS2. Players authenticate via Steam, queue for matches, get assigned to a dedicated CS2 game server, and match stats are recorded automatically via the MatchZy plugin.

## Components

| Component | Description |
|---|---|
| `dreamleague.api` | ASP.NET Core REST API + SignalR hubs |
| `dreamleague.app` | React SPA |
| `dreamleague.gameserver` | Dockerized CS2 server (SteamCMD + Metamod + CounterStrikeSharp + MatchZy) |
| `dreamleague.main.db` | MSSQL database |
| `dreamleague.matches.db` | MongoDB (match documents consumed by the CS2 plugin) |

## Quick links

- [Getting Started](/getting-started)
- [Architecture](/architecture)
- [Configuration](/configuration)
- [GitHub](https://github.com/caiopolito/dreamleague)
