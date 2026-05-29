---
id: server-commands
title: Server Commands
sidebar_position: 7
---

# Server Commands

DreamLeague extends the CS2 dedicated server with custom RCON commands provided by the MatchZy fork.

## Custom commands

| Command | Description |
|---|---|
| `dreamleague_loadmatch <matchId>` | Fetches the match document from MongoDB by ID and loads it into MatchZy. This is sent by the API automatically when a match is created. |

## Standard CS2 RCON commands

You can send any standard CS2 server command via RCON. Use the RCON password configured in `CS2_RCONPW`.

```bash
# Example using rcon-cli
rcon -a 127.0.0.1:27015 -p Password1234! "status"
rcon -a 127.0.0.1:27015 -p Password1234! "mp_restartgame 1"
```

## Managing servers

Additional CS2 servers must be manually inserted into the `servers` SQL table:

```sql
INSERT INTO servers (ip, port, rcon_password, in_use, password)
VALUES ('your.server.ip', 27015, 'your_rcon_password', 0, 'your_server_password');
```

The platform seeds a single server entry pointing to `172.20.128.1:27015` (the Docker bridge IP) on first run.

## Forcing a CS2 update

To apply a CS2 patch without wiping the server volume:

```bash
CS2_FORCE_UPDATE=1 docker compose up -d gameserver
```

Remove `CS2_FORCE_UPDATE` after the update completes. To apply a major update (after the base image is rebuilt):

```bash
docker compose pull gameserver
docker volume rm dreamleague-gameserver
docker compose up -d gameserver
```
