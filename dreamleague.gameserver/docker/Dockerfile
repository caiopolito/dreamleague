FROM cm2network/steamcmd:root as build

ENV STEAMAPPID 730
ENV STEAMAPP cs2
ENV STEAMAPPDIR "${HOMEDIR}/${STEAMAPP}server"
ENV CSGOFOLDERDIR "${STEAMAPPDIR}/game/csgo"
ENV STEAMAPPVALIDATE 0
ENV METAMODURL "https://mms.alliedmods.net/mmsdrop/2.0/mmsource-2.0.0-git1282-linux.tar.gz"
ENV CSSHARPURL "https://github.com/roflmuffin/CounterStrikeSharp/releases/download/v179/counterstrikesharp-with-runtime-build-179-linux-12485be.zip"
ENV MATCHZYURL "https://github.com/shobhit-pathak/MatchZy/releases/download/0.7.0/matchzy-0.7.0.zip"

COPY entry.sh "${HOMEDIR}/entry.sh"
COPY server.cfg "/etc/server.cfg"

RUN ls \
    && set -x \
	&& apt-get update \
	&& apt-get install -y --no-install-recommends --no-install-suggests \
		wget \
		ca-certificates \
		lib32z1 \
        simpleproxy \
        libicu-dev \
        unzip \
	&& mkdir -p "${STEAMAPPDIR}" \
    && mkdir -p "${CSGOFOLDERDIR}" \
	&& chmod +x "${HOMEDIR}/entry.sh" \
    && cd "${STEAMAPPDIR}" \
    && curl -fsSL "${METAMODURL}" | tar xvzf - -C ./game/csgo/ \
    && cd ./game/csgo \
    && wget -O cssharp.zip "${CSSHARPURL}" && unzip -o cssharp.zip \
    && wget -O matchzy.zip "${MATCHZYURL}" && unzip -o matchzy.zip \
	&& chown -R "${USER}:${USER}" "${HOMEDIR}/entry.sh" "${STEAMAPPDIR}" \
        && apt-get clean \
        && find /var/lib/apt/lists/ -type f -delete

RUN set -x \
        && chown -R "${USER}:${USER}" "${STEAMAPPDIR}" \
        && chmod 0777 "${STEAMAPPDIR}"

ENV CS2_SERVERNAME="cs2 private server" \
    CS2_CHEATS=0 \
    CS2_IP=0.0.0.0 \
    CS2_SERVER_HIBERNATE=0 \
    CS2_PORT=27015 \
    CS2_RCON_PORT="" \
    CS2_MAXPLAYERS=10 \
    CS2_RCONPW="changeme" \
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

USER ${USER}

WORKDIR ${HOMEDIR}

CMD ["bash", "entry.sh"]

# Expose ports
EXPOSE 27015/tcp \
	27015/udp \
	27020/udp