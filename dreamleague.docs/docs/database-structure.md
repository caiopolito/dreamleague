---
id: database-structure
title: Database Structure
sidebar_position: 4
---

# Database Structure

DreamLeague uses two databases: **MSSQL** for relational platform data and **MongoDB** for match documents consumed by the CS2 plugin.

## MSSQL tables

### Players & authentication

| Table | Description |
|---|---|
| `players` | Steam users registered on the platform (`steam_id` PK, name, avatar, coins, points) |

### Servers

| Table | Description |
|---|---|
| `servers` | CS2 servers available for matchmaking (IP, port, RCON password, `in_use` flag, server password) |

### Matches

| Table | Description |
|---|---|
| `matches` | Top-level match record |
| `team_match` | The two teams in a match |
| `team_match_players` | Players assigned to each team |
| `map_stats` | Per-map statistics (score, winner) |
| `player_stats` | Per-player per-map statistics (kills, deaths, assists, etc.) |

### Teams & championships

| Table | Description |
|---|---|
| `teams` | Persistent DreamLeague teams |
| `team_players` | Team membership |
| `championships` | Tournament/championship records |
| `championship_teams` | Teams registered to a championship |

### Communication

| Table | Description |
|---|---|
| `chats` | Chat sessions |
| `messages` | Chat messages |
| `chat_players` | Players in a chat session |
| `team_notifications` | Team invite notifications (seen/responded state) |

## MongoDB

MongoDB stores `RconMatch` documents — the match JSON consumed by the CS2 MatchZy plugin to load a match.

- **Collection:** `matches`
- **Document:** mirrors the GET5 match schema (legacy naming retained for compatibility)

When a match is created, the API inserts a document into this collection and sends `dreamleague_loadmatch {matchId}` via RCON to the CS2 server. MatchZy fetches the document from MongoDB by ID and loads the match configuration.
