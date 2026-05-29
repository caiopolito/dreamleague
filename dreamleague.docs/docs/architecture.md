---
id: architecture
title: Architecture Overview
sidebar_position: 3
---

# Architecture Overview

DreamLeague is a monorepo with five independently deployable components that communicate over a shared Docker network.

## Component diagram

```
Browser
  │
  ▼
dreamleague.app  (React SPA, port 3000)
  │  REST + SignalR
  ▼
dreamleague.api  (ASP.NET Core 6, port 5001)
  ├── MSSQL  ──►  dreamleague.main.db   (player, team, match records)
  ├── MongoDB ──► dreamleague.matches.db (match JSON for CS2 plugin)
  └── RCON   ──► dreamleague.gameserver  (CS2 server, port 27015)
                        │
                        │ HTTP callbacks (MatchZy webhook)
                        └────────────────► dreamleague.api
```

## API layer (`dreamleague.api`)

The API follows a strict layered architecture across six C# projects:

| Project | Role |
|---|---|
| `dreamleague.api` | ASP.NET Core host, DI wiring, controllers, SignalR hubs |
| `dreamleague.domain` | Interfaces, aggregates (Request/Response), adapters |
| `dreamleague.services` | Business logic |
| `dreamleague.infrastructure` | Dapper + SQL, MongoDB, RCON, HTTP repositories |
| `dreamleague.hubs` | SignalR hub implementations |
| `dreamleague.shared` | Base classes (`GenericService`, `ApplicationConfig`) |

### SignalR hubs

| Hub | Path | Purpose |
|---|---|---|
| `QueueHub` | `/api/queue` | Manages the in-memory matchmaking queue, triggers match creation |
| `ChatHub` | `/api/chat` | Real-time in-platform chat |
| `NotificationHub` | `/api/notification` | Team invite notifications |

## Game server (`dreamleague.gameserver`)

The CS2 server uses a two-image build strategy to avoid a ~35 GB Steam download on every start:

- **`Dockerfile.base`** — downloads CS2 via SteamCMD at image build time (rebuilt weekly)
- **`Dockerfile`** — installs Metamod, CounterStrikeSharp, and MatchZy on top of the base

On the first `docker compose up`, Docker copies the CS2 files from the image into a named volume. Subsequent starts reuse the volume instantly with no download.

## Authentication

Players authenticate via **Steam OpenID**. The login page redirects to Steam, which redirects back to `/callback` where the Steam ID is extracted and the player profile is fetched from the API.
