FROM ubuntu:20.04

ENV DEBIAN_FRONTEND=noninteractive

# Runtime identity — matches what the rest of the toolchain expects
ENV USER=steam \
    HOMEDIR=/home/steam \
    STEAMCMDDIR=/home/steam/steamcmd

ENV STEAMAPPID=730 \
    STEAMAPP="cs2" \
    STEAMAPPDIR=/home/steam/cs2server \
    CSGOFOLDERDIR=/home/steam/cs2server/game/csgo

# Plugin versions — override these env vars in docker-compose to update
# plugins without rebuilding the image
ENV METAMODURL="https://github.com/alliedmodders/metamod-source/releases/download/2.0.0.1401/mmsource-2.0.0-git1401-linux.tar.gz" \
    MATCHZYURL="https://github.com/caiopolito/MatchZy/releases/download/0.8.15/MatchZy-0.8.15-with-cssharp-linux.zip"

# MongoDB connection — override in docker-compose for non-default setups
ENV DREAMLEAGUE_MATCH_DATABASE="mongodb://root:changeme@dreamleague-database-matches-1:27017" \
    DREAMLEAGUE_MATCH_DATABASE_NAME="dreamleague" \
    DREAMLEAGUE_MATCH_DATABASE_COLLECTION="matches"

# CS2 server runtime configuration — all overridable via docker-compose environment
ENV CS2_SERVERNAME="[DREAMLEAGUE] #1 Private Matchmaking" \
    CS2_CHEATS=0 \
    CS2_IP=0.0.0.0 \
    CS2_SERVER_HIBERNATE=0 \
    CS2_PORT=27015 \
    CS2_MAXPLAYERS=10 \
    CS2_RCONPW="changeme" \
    CS2_PW="changeme" \
    CS2_MAPGROUP="mg_active" \
    CS2_STARTMAP="de_inferno" \
    CS2_GAMETYPE=0 \
    CS2_GAMEMODE=1 \
    CS2_LAN=0 \
    TV_AUTORECORD=0 \
    TV_ENABLE=0 \
    TV_PORT=27020 \
    TV_PW="changeme" \
    TV_RELAY_PW="changeme" \
    TV_MAXRATE=0 \
    TV_DELAY=0 \
    SRCDS_TOKEN="" \
    CS2_CFG_URL="" \
    CS2_LOG="on" \
    CS2_LOG_MONEY=0 \
    CS2_LOG_DETAIL=0 \
    CS2_LOG_ITEMS=0 \
    CS2_ADDITIONAL_ARGS="" \
    CS2_FORCE_UPDATE=0 \
    CS2_INSTALL_PLUGINS=1

# i386 arch required for the 32-bit steamcmd bootstrap binary
RUN dpkg --add-architecture i386 \
    && apt-get update \
    && apt-get install -y --no-install-recommends \
        lib32gcc-s1 \
        ca-certificates \
        wget \
        curl \
        libicu-dev \
        unzip \
        procps \
        lib32z1 \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

RUN useradd -m -u 1000 -s /bin/bash "${USER}"

# Install SteamCMD and pre-create the CS2 install directory so Docker
# initialises the named volume with steam ownership on first mount.
# Without this, Docker creates the mount point as root and the steam
# user cannot write the CS2 download into it.
RUN mkdir -p "${STEAMCMDDIR}" "${STEAMAPPDIR}" \
    && wget -qO- 'https://steamcdn-a.akamaihd.net/client/installer/steamcmd_linux.tar.gz' \
       | tar xvzf - -C "${STEAMCMDDIR}" \
    && chown -R "${USER}:${USER}" "${HOMEDIR}"

# Metamod resolves its plugin folder as "/addons/metamod" (empty gamedir quirk).
# The symlink target lives in the named volume — it doesn't need to exist at build time.
RUN ln -sfT "${STEAMAPPDIR}/game/csgo/addons" /addons

COPY entrypoint.sh "${HOMEDIR}/entrypoint.sh"
COPY server.cfg "/etc/server.cfg"

# Run as root so the entrypoint can fix volume ownership before dropping
# to the steam user. Docker does not reliably propagate image directory
# ownership to named volume mount points across all versions.
WORKDIR ${HOMEDIR}
CMD ["bash", "entrypoint.sh"]

EXPOSE 27015/tcp \
    27015/udp \
    27020/udp
