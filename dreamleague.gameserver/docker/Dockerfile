FROM ghcr.io/caiopolito/dreamleague-cs2-base:latest

ARG METAMODURL=https://mms.alliedmods.net/mmsdrop/2.0/mmsource-2.0.0-git1374-linux.tar.gz
ARG MATCHZYURL=https://github.com/caiopolito/MatchZy/releases/download/0.8.15/MatchZy-0.8.15-with-cssharp-linux.zip
ARG DREAMLEAGUE_MATCH_DATABASE=mongodb://root:Password1234!@dreamleague-database-matches-1:27017
ARG DREAMLEAGUE_MATCH_DATABASE_NAME=dreamleague
ARG DREAMLEAGUE_MATCH_DATABASE_COLLECTION=matches

# CS2 server runtime configuration — all overridable via docker-compose environment:
ENV CS2_SERVERNAME="[DREAMLEAGUE] #1 Private Matchmaking" \
    CS2_CHEATS=0 \
    CS2_IP=0.0.0.0 \
    CS2_SERVER_HIBERNATE=0 \
    CS2_PORT=27015 \
    CS2_RCON_PORT="" \
    CS2_MAXPLAYERS=10 \
    CS2_RCONPW="Password1234!" \
    CS2_PW="changeme" \
    CS2_MAPGROUP="mg_active" \
    CS2_STARTMAP="de_inferno" \
    CS2_GAMEALIAS="" \
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
    # Set to 1 to force a steamcmd update on next container start
    CS2_FORCE_UPDATE=0

# Install Metamod, CounterStrikeSharp, and MatchZy into the CS2 installation
# that was baked into the base image.
USER root
RUN set -x \
    && cd "${STEAMAPPDIR}" \
    && curl -fsSL "${METAMODURL}" | tar xvzf - -C ./game/csgo/ \
    && cd ./game/csgo \
    && wget -O matchzy.zip "${MATCHZYURL}" && unzip -o matchzy.zip \
    && rm -f matchzy.zip \
    && grep -rl 'MatchZy' ./cfg/MatchZy/config.cfg | xargs sed -i 's/MatchZy/DreamLeague/g' \
    && chown -R "${USER}:${USER}" "${STEAMAPPDIR}"

# Inject MongoDB connection details into MatchZy config at build time
RUN sed -i -e "s|{{DREAMLEAGUE_MATCH_DATABASE}}|${DREAMLEAGUE_MATCH_DATABASE}|g" \
        -e "s|{{DREAMLEAGUE_MATCH_DATABASE_NAME}}|${DREAMLEAGUE_MATCH_DATABASE_NAME}|g" \
        -e "s|{{DREAMLEAGUE_MATCH_DATABASE_COLLECTION}}|${DREAMLEAGUE_MATCH_DATABASE_COLLECTION}|g" \
        "${STEAMAPPDIR}/game/csgo/cfg/MatchZy/config.cfg"

# Metamod resolves its plugin folder as "/addons/metamod" (empty gamedir quirk).
# Create a symlink at the root so it finds the right path at runtime.
RUN ln -sfT "${STEAMAPPDIR}/game/csgo/addons" /addons

COPY entrypoint.sh "${HOMEDIR}/entrypoint.sh"
COPY server.cfg "/etc/server.cfg"

USER ${USER}

WORKDIR ${HOMEDIR}

CMD [ "bash", "entrypoint.sh" ]

# CS2 game port (TCP+UDP) and SourceTV
EXPOSE 27015/tcp \
    27015/udp \
    27020/udp
