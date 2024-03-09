eval bash "${STEAMCMDDIR}/steamcmd.sh" +force_install_dir "${STEAMAPPDIR}" \
				+login anonymous \
				+app_update "${STEAMAPPID}" "${STEAMAPPVALIDATE}"\
				+quit

# steamclient.so fix
mkdir -p ~/.steam/sdk64 \
    && ln -sfT ${STEAMCMDDIR}/linux64/steamclient.so ~/.steam/sdk64/steamclient.so

cp /etc/server.cfg "${STEAMAPPDIR}"/game/csgo/cfg/server.cfg

# update gameinfo.gi
LOWVIOLENCE_LINE=$(grep -m 1 -n 'Game_LowViolence' "${CSGOFOLDERDIR}/gameinfo.gi" | cut -f1 -d:) && LOWVIOLENCE_LINE=$(expr $LOWVIOLENCE_LINE + 1) \
    && sed -i "$LOWVIOLENCE_LINE i Game csgo/addons/metamod" "${CSGOFOLDERDIR}/gameinfo.gi"

if [[ -z $CS2_IP ]]; then
    CS2_IP_ARGS=""
else
    CS2_IP_ARGS="-ip ${CS2_IP}"
fi

# Rewrite Config Files

sed -i -e "s/{{SERVER_HOSTNAME}}/${CS2_SERVERNAME}/g" \
       -e "s/{{SERVER_CHEATS}}/${CS2_CHEATS}/g" \
       -e "s/{{SERVER_HIBERNATE}}/${CS2_SERVER_HIBERNATE}/g" \
       -e "s/{{SERVER_PW}}/${CS2_PW}/g" \
       -e "s/{{SERVER_RCON_PW}}/${CS2_RCONPW}/g" \
       -e "s/{{TV_ENABLE}}/${TV_ENABLE}/g" \
       -e "s/{{TV_PORT}}/${TV_PORT}/g" \
       -e "s/{{TV_AUTORECORD}}/${TV_AUTORECORD}/g" \
       -e "s/{{TV_PW}}/${TV_PW}/g" \
       -e "s/{{TV_RELAY_PW}}/${TV_RELAY_PW}/g" \
       -e "s/{{TV_MAXRATE}}/${TV_MAXRATE}/g" \
       -e "s/{{TV_DELAY}}/${TV_DELAY}/g" \
       -e "s/{{SERVER_LOG}}/${CS2_LOG}/g" \
       -e "s/{{SERVER_LOG_MONEY}}/${CS2_LOG_MONEY}/g" \
       -e "s/{{SERVER_LOG_DETAIL}}/${CS2_LOG_DETAIL}/g" \
       -e "s/{{SERVER_LOG_ITEMS}}/${CS2_LOG_ITEMS}/g" \
       "${STEAMAPPDIR}"/game/csgo/cfg/server.cfg


cd "${STEAMAPPDIR}/game/bin/linuxsteamrt64"

eval "./cs2" -dedicated "${CS2_IP_ARGS}" -port "${CS2_PORT}" -console -usercon -maxplayers "${CS2_MAXPLAYERS}" +game_alias competitive +mapgroup mg_active +map de_inferno +rcon_password "${CS2_RCONPW}" +sv_lan 0



