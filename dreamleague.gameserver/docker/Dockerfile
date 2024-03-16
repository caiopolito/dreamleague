FROM cm2network/steamcmd:root as build

ARG METAMODURL=https://mms.alliedmods.net/mmsdrop/2.0/mmsource-2.0.0-git1282-linux.tar.gz
ARG CSSHARPURL=https://github.com/roflmuffin/CounterStrikeSharp/releases/download/v193/counterstrikesharp-with-runtime-build-193-linux-36a97bf.zip
ARG MATCHZYURL=https://github.com/caiopolito/MatchZy/releases/download/alpha-v0.1/matchzy-dreamleague-alpha-v0.1.zip
ARG DREAMLEAGUE_MATCH_DATABASE=mongodb://root:Password1234!@dreamleague-database-matches-1:27017
ARG DREAMLEAGUE_MATCH_DATABASE_NAME=dreamleague
ARG DREAMLEAGUE_MATCH_DATABASE_COLLECTION=matches

ENV STEAMAPPID=730 \
    STEAMAPP="cs2"
ENV STEAMAPPDIR="${HOMEDIR}/${STEAMAPP}server"
ENV CSGOFOLDERDIR="${STEAMAPPDIR}/game/csgo" \
    CS2_SERVERNAME="[DREAMLEAGUE] #1 Private Matchmaking" \
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
    CS2_ADDITIONAL_ARGS=""

RUN    set -x \
	&& apt-get update \
	&& apt-get install -y --no-install-recommends --no-install-suggests \
		wget \
		ca-certificates \
		lib32z1 \
        simpleproxy \
        libicu-dev \
        unzip \
        procps \
	&& mkdir -p "${STEAMAPPDIR}" \
    && mkdir -p "${CSGOFOLDERDIR}" \
    && cd "${STEAMAPPDIR}" \
    && curl -fsSL "${METAMODURL}" | tar xvzf - -C ./game/csgo/ \
    && cd ./game/csgo \
    && wget -O cssharp.zip "${CSSHARPURL}" && unzip -o cssharp.zip \
    && wget -O matchzy.zip "${MATCHZYURL}" && unzip -o matchzy.zip \
    && grep -rl 'MatchZy' ./cfg/MatchZy/config.cfg | xargs sed -i 's/MatchZy/DreamLeague/g' \ 
    && apt-get clean \
    && find /var/lib/apt/lists/ -type f -delete \
    && set -x \
    && chown -R "${USER}:${USER}" "${STEAMAPPDIR}" \
    && chmod 0777 "${STEAMAPPDIR}"

RUN sed -i -e "s|{{DREAMLEAGUE_MATCH_DATABASE}}|${DREAMLEAGUE_MATCH_DATABASE}|g" \
        -e "s|{{DREAMLEAGUE_MATCH_DATABASE_NAME}}|${DREAMLEAGUE_MATCH_DATABASE_NAME}|g" \
        -e "s|{{DREAMLEAGUE_MATCH_DATABASE_COLLECTION}}|${DREAMLEAGUE_MATCH_DATABASE_COLLECTION}|g" \
        "${STEAMAPPDIR}"/game/csgo/cfg/MatchZy/config.cfg
        
COPY entrypoint.sh "${HOMEDIR}/entrypoint.sh"
COPY server.cfg "/etc/server.cfg"

USER ${USER}

WORKDIR ${HOMEDIR}

CMD [ "bash", "entrypoint.sh" ]

# Expose ports
EXPOSE 27015/tcp \
	27015/udp \
	27020/udp