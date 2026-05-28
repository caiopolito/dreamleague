#!/bin/bash
set -e

# ---------------------------------------------------------------------------
# CS2 initialization
#
# On the very first start the named volume (dreamleague-gameserver) is empty.
# Docker copies the image contents at ${STEAMAPPDIR} into the volume, so
# CS2 + all plugins are already present — no download needed.
#
# A marker file is written after the first successful start. On subsequent
# starts we skip the steamclient.so fixup that re-runs the heavy init path.
#
# To force a steamcmd update (e.g. after a CS2 patch): set CS2_FORCE_UPDATE=1
# via docker-compose environment, then restart the container. Remove the flag
# after the update completes.
# ---------------------------------------------------------------------------

if [ "${CS2_FORCE_UPDATE}" = "1" ]; then
    echo "[DreamLeague] CS2_FORCE_UPDATE=1 — running steamcmd update..."
    eval bash "${STEAMCMDDIR}/steamcmd.sh" \
        +force_install_dir "${STEAMAPPDIR}" \
        +login anonymous \
        +app_update "${STEAMAPPID}" \
        +quit
    echo "[DreamLeague] steamcmd update complete."
fi

# steamclient.so fix (needed after every steamcmd run or on fresh volume)
mkdir -p ~/.steam/sdk64
ln -sfT "${STEAMCMDDIR}/linux64/steamclient.so" ~/.steam/sdk64/steamclient.so

# Apply server.cfg (always overwritten so config changes take effect on restart)
cp /etc/server.cfg "${STEAMAPPDIR}/game/csgo/cfg/server.cfg"

# Patch gameinfo.gi to load Metamod — idempotent, safe to run every start
if ! grep -q "Game csgo/addons/metamod" "${STEAMAPPDIR}/game/csgo/gameinfo.gi"; then
    echo "[DreamLeague] Patching gameinfo.gi for Metamod..."
    LOWVIOLENCE_LINE=$(grep -m 1 -n 'Game_LowViolence' "${STEAMAPPDIR}/game/csgo/gameinfo.gi" | cut -f1 -d:)
    LOWVIOLENCE_LINE=$((LOWVIOLENCE_LINE + 1))
    sed -i "${LOWVIOLENCE_LINE} i\\\\t\\t\\t\\tGame csgo/addons/metamod" "${STEAMAPPDIR}/game/csgo/gameinfo.gi"
fi

# Expand server.cfg template variables
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
       "${STEAMAPPDIR}/game/csgo/cfg/server.cfg"

# Build IP argument
if [[ -z "${CS2_IP}" ]]; then
    CS2_IP_ARGS=""
else
    CS2_IP_ARGS="-ip ${CS2_IP}"
fi

echo "[DreamLeague] Starting CS2 dedicated server..."

exec "${STEAMAPPDIR}/game/bin/linuxsteamrt64/cs2" \
    --graphics-provider "" -- -dedicated \
    -game csgo \
    ${CS2_IP_ARGS} \
    -port "${CS2_PORT}" \
    -console \
    -usercon \
    -maxplayers "${CS2_MAXPLAYERS}" \
    +game_alias competitive \
    +mapgroup mg_active \
    +map de_inferno \
    +rcon_password "${CS2_RCONPW}" \
    +sv_lan 0 \
    ${CS2_ADDITIONAL_ARGS}
