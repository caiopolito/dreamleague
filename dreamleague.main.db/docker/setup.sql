SET xact_abort ON

CREATE DATABASE dreamleague
GO
USE [dreamleague]
GO

BEGIN TRANSACTION T1
IF OBJECT_ID(N'dbo.players', N'U') IS NULL
BEGIN
CREATE TABLE players (
	steam_id VARCHAR(50) PRIMARY KEY NOT NULL,
	coins INTEGER NOT NULL DEFAULT 50,
	points INTEGER NOT NULL DEFAULT 0,
	name VARCHAR(200) NOT NULL,
	is_admin BIT NOT NULL DEFAULT 0,
	avatar VARCHAR(200) 
)
END



IF OBJECT_ID(N'dbo.chats', N'U') IS NULL
BEGIN
CREATE TABLE chats (
	id UNIQUEIDENTIFIER PRIMARY KEY
)
END

IF OBJECT_ID(N'dbo.messages', N'U') IS NULL
BEGIN
CREATE TABLE messages (
	id UNIQUEIDENTIFIER PRIMARY KEY,
	chat_id UNIQUEIDENTIFIER FOREIGN KEY REFERENCES chats(id),
	sender_steamid VARCHAR(50) NOT NULL,
	receiver_steamid VARCHAR(50) NOT NULL,
	message VARCHAR(200) NOT NULL,
	message_time DATETIME
)
END

IF OBJECT_ID(N'dbo.chat_players', N'U') IS NULL
BEGIN
CREATE TABLE chat_players (
	chat_id UNIQUEIDENTIFIER FOREIGN KEY REFERENCES chats(id),
	player_id VARCHAR(50) FOREIGN KEY REFERENCES players (steam_id)
)
END


IF OBJECT_ID(N'dbo.championships', N'U') IS NULL
BEGIN
CREATE TABLE championships (
	id UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	name VARCHAR(200) NOT NULL,
	description VARCHAR(200),
	start_date DATETIME NOT NULL,
	end_date DATETIME NOT NULL,
	min_teams INTEGER NOT NULL
)
END

IF OBJECT_ID(N'dbo.team_match', N'U') IS NULL
BEGIN
CREATE TABLE team_match (
	id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	name VARCHAR(50),
	tag VARCHAR(50) DEFAULT '',
	flag VARCHAR(4) DEFAULT '',
	logo VARCHAR(10) DEFAULT ''
)
END

IF OBJECT_ID(N'dbo.team_match_players', N'U') IS NULL
BEGIN
CREATE TABLE team_match_players (
	team_match_id UNIQUEIDENTIFIER FOREIGN KEY REFERENCES team_match(id),
	player_id VARCHAR(50) FOREIGN KEY REFERENCES players(steam_id)
)
END

IF OBJECT_ID(N'dbo.servers', N'U') IS NULL
BEGIN
CREATE TABLE servers (
	id UNIQUEIDENTIFIER PRIMARY KEY,
	display_name VARCHAR(50) NOT NULL,
	ip_string VARCHAR(32) NOT NULL,
	port INTEGER NOT NULL,
	rcon_password VARCHAR(32) NOT NULL,
	in_use BIT DEFAULT 0,
	password VARCHAR(50) NOT NULL
)
END

IF OBJECT_ID(N'dbo.matches', N'U') IS NULL
BEGIN
CREATE TABLE matches (
	matchid INTEGER identity(1,1) PRIMARY KEY,
	match_title VARCHAR(60) DEFAULT '',
	server_id UNIQUEIDENTIFIER FOREIGN KEY REFERENCES servers(id),
	team1_id UNIQUEIDENTIFIER FOREIGN KEY REFERENCES team_match(id),
	team2_id UNIQUEIDENTIFIER FOREIGN KEY REFERENCES team_match(id),
	team1_string VARCHAR(50) DEFAULT '',
	team2_string VARCHAR(50) DEFAULT '',
	winner UNIQUEIDENTIFIER FOREIGN KEY REFERENCES team_match(id),
	plugin_version VARCHAR(50) DEFAULT 'unknown',
	forfeit BIT DEFAULT 0,
	cancelled BIT DEFAULT 0,
	start_time DATETIME,
	end_time DATETIME,
	max_maps INTEGER DEFAULT 1,
	skip_veto BIT,
	api_key VARCHAR(50),
	team1_score INTEGER DEFAULT 0,
	team2_score INTEGER DEFAULT 0
)
END

IF OBJECT_ID(N'dbo.map_stats', N'U') IS NULL
BEGIN
CREATE TABLE map_stats (
	id UNIQUEIDENTIFIER PRIMARY KEY,
	match_id INTEGER FOREIGN KEY REFERENCES matches(matchid),
	map_number INTEGER,
	map_name VARCHAR(64),
	start_time DATETIME,
	end_time DATETIME,
	winner UNIQUEIDENTIFIER FOREIGN KEY REFERENCES team_match(id),
	team1_score INTEGER DEFAULT 0,
	team2_score INTEGER DEFAULT 0,
)
END

IF OBJECT_ID(N'dbo.player_stats', N'U') IS NULL
BEGIN
CREATE TABLE player_stats (
	id UNIQUEIDENTIFIER PRIMARY KEY,
	match_id INTEGER FOREIGN KEY REFERENCES matches(matchid),
	map_id UNIQUEIDENTIFIER FOREIGN KEY REFERENCES map_stats(id),
	team_id UNIQUEIDENTIFIER FOREIGN KEY REFERENCES team_match(id),
	steam_id VARCHAR(50),
	name VARCHAR(50),
	kills INTEGER DEFAULT 0,
	deaths INTEGER DEFAULT 0,
	roundsplayed INTEGER DEFAULT 0,
	assists INTEGER DEFAULT 0,
	flashbang_assists INTEGER DEFAULT 0,
	teamkills INTEGER DEFAULT 0,
	suicides INTEGER DEFAULT 0,
	headshot_kills INTEGER DEFAULT 0,
	damage INTEGER DEFAULT 0,
	bomb_plants INTEGER DEFAULT 0,
	bomb_defuses INTEGER DEFAULT 0,
	v1 INTEGER DEFAULT 0,
	v2 INTEGER DEFAULT 0,
	v3 INTEGER DEFAULT 0,
	v4 INTEGER DEFAULT 0,
	v5 INTEGER DEFAULT 0,
	k1 INTEGER DEFAULT 0,
	k2 INTEGER DEFAULT 0,
	k3 INTEGER DEFAULT 0,
	k4 INTEGER DEFAULT 0,
	k5 INTEGER DEFAULT 0,
	firstkill_t INTEGER DEFAULT 0,
	firstkill_ct INTEGER DEFAULT 0,
	firstdeath_t INTEGER DEFAULT 0,
	firstdeath_ct INTEGER DEFAULT 0,
)
END

IF OBJECT_ID(N'dbo.teams', N'U') IS NULL
BEGIN
CREATE TABLE teams (
	id UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	name VARCHAR(200) NOT NULL,
)
END

IF OBJECT_ID(N'dbo.team_notifications', N'U') IS NULL
BEGIN
CREATE TABLE team_notifications (
	id UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	steam_id VARCHAR(50) FOREIGN KEY REFERENCES players(steam_id),
	team_id UNIQUEIDENTIFIER FOREIGN KEY REFERENCES teams(id),
	is_responded BIT DEFAULT 0,
	responded_at DATETIME,
	is_seen BIT DEFAULT 0,
	seen_at DATETIME
)
END

IF OBJECT_ID(N'dbo.championship_teams', N'U') IS NULL
BEGIN
CREATE TABLE championship_teams (
	championship_id UNIQUEIDENTIFIER FOREIGN KEY REFERENCES championships(id),
	team_id UNIQUEIDENTIFIER FOREIGN KEY REFERENCES teams(id),
)
END

IF OBJECT_ID(N'dbo.team_players', N'U') IS NULL
BEGIN
CREATE TABLE team_players (
	team_id UNIQUEIDENTIFIER FOREIGN KEY REFERENCES teams(id),
	player_id VARCHAR(50) FOREIGN KEY REFERENCES players(steam_id),
	is_captain BIT NOT NULL
)
END

IF NOT EXISTS (SELECT * FROM dbo.servers WHERE ip_string = '172.20.128.1')
BEGIN
INSERT INTO dbo.servers (id, display_name, ip_string, port, rcon_password, in_use, password)
VALUES (NEWID(), 'test', '172.20.128.1', 27015, 'Password1234!', 0, 'changeme')
END

COMMIT TRANSACTION T1


