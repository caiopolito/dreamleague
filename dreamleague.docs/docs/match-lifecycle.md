---
id: match-lifecycle
title: Match Lifecycle
sidebar_position: 6
---

# Match Lifecycle

A full DreamLeague match goes through several stages, coordinated between the platform and the CS2 server.

## 1. Queueing

Players connect to the `QueueHub` SignalR hub (`/api/queue`) and call `EnterQueueAsync`. The hub holds an in-memory dictionary of queued players. When the number of queued players reaches `Queue.MinPlayers`, match creation is triggered automatically.

## 2. Match creation

`CreateMatchService` runs when the queue threshold is met:

1. Finds an available CS2 server in the `servers` table (one where `in_use = false`)
2. Creates `team_match` records in MSSQL — one per team, with players assigned
3. Inserts a `RconMatch` JSON document into MongoDB
4. Sends `dreamleague_loadmatch {matchId}` via RCON to the CS2 server
5. Marks the server as `in_use = true`
6. Pushes the server connection string to all queued players via SignalR

## 3. Players connect to CS2

Players receive the server IP and password via SignalR and connect to the CS2 dedicated server. MatchZy loads the match from MongoDB and manages the knife round, team sides, and warmup.

## 4. Live match callbacks

As the match progresses, MatchZy calls back into the API via HTTP POST. Each callback updates the MSSQL records:

| Endpoint | Service | Description |
|---|---|---|
| `POST /api/match/{id}/map/{n}/start` | `StartMatchMapService` | Map has started |
| `POST /api/match/{id}/map/{n}/update` | `UpdateMatchMapService` | Score update |
| `POST /api/match/{id}/map/{n}/player/{steamId}/update` | `UpdateMatchMapPlayerService` | Per-player stats update |
| `POST /api/match/{id}/map/{n}/finish` | `FinishMatchMapService` | Map finished |
| `POST /api/match/{id}/finish` | `FinishMatchService` | Full match finished, server freed |

## 5. Match end

`FinishMatchService` sets `in_use = false` on the server, making it available for the next match. Stats are now available in the platform.

:::note Player stats form fields
`UpdateMatchMapPlayer` reads special numeric-prefixed fields (`1kill_rounds` through `5kill_rounds`) directly from `Request.Form` because model binding doesn't support numeric-prefixed keys.
:::
