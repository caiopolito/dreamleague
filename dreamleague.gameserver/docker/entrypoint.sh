#!/bin/bash
set -e

# ---------------------------------------------------------------------------
# 0. Drop to steam user (runs once, as root, on every container start)
#    Fixes volume ownership first so steamcmd can write into it.
# ---------------------------------------------------------------------------
if [ "$(id -u)" = "0" ]; then
    chown steam:steam "${STEAMAPPDIR}"
    exec runuser -u steam -- bash "$(readlink -f "$0")"
fi

METAMOD_MARKER="${STEAMAPPDIR}/.metamod_version"
MATCHZY_MARKER="${STEAMAPPDIR}/.matchzy_version"
MATCHZY_CONFIG="${STEAMAPPDIR}/game/csgo/cfg/MatchZy/config.cfg"
MATCHZY_CONFIG_TMPL="${STEAMAPPDIR}/game/csgo/cfg/MatchZy/config.cfg.tmpl"

# ---------------------------------------------------------------------------
# 1. Download CS2 if not present or force update requested
# ---------------------------------------------------------------------------
if [ ! -f "${STEAMAPPDIR}/game/bin/linuxsteamrt64/cs2" ] || [ "${CS2_FORCE_UPDATE}" = "1" ]; then
    echo "[DreamLeague] Downloading CS2 via SteamCMD (this will take a while)..."
    until bash "${STEAMCMDDIR}/steamcmd.sh" \
        +force_install_dir "${STEAMAPPDIR}" \
        +login anonymous \
        +app_update "${STEAMAPPID}" validate \
        +quit; do
        echo "[DreamLeague] Download interrupted (exit $?), retrying in 10s..."
        sleep 10
    done
    echo "[DreamLeague] CS2 download complete."
    echo "[DreamLeague] Game directory structure:"
    ls "${STEAMAPPDIR}/game/" 2>/dev/null || echo "  (game/ not found — listing STEAMAPPDIR instead:)" && ls "${STEAMAPPDIR}/" 2>/dev/null
fi

# ---------------------------------------------------------------------------
# 2. Ensure SteamCMD has self-updated so its 64-bit SDK files exist, then
#    symlink steamclient.so where CS2 expects it. SteamCMD only downloads
#    linux64/steamclient.so on its first run; if CS2 is already in the
#    volume step 1 is skipped and the file would otherwise be missing.
# ---------------------------------------------------------------------------
bash "${STEAMCMDDIR}/steamcmd.sh" +quit || true
mkdir -p ~/.steam/sdk64
ln -sfT "${STEAMCMDDIR}/linux64/steamclient.so" ~/.steam/sdk64/steamclient.so

# ---------------------------------------------------------------------------
# 3-6. Plugins (Metamod + MatchZy)
#      Set CS2_INSTALL_PLUGINS=0 to skip all plugins and run bare CS2.
#      Set MATCHZYURL="" to install Metamod only (useful for diagnostics).
# ---------------------------------------------------------------------------
if [ "${CS2_INSTALL_PLUGINS}" = "0" ]; then
    echo "[DreamLeague] CS2_INSTALL_PLUGINS=0 — bare CS2, no plugins."
    sed -i '/Game csgo\/addons\/metamod/d' "${STEAMAPPDIR}/game/csgo/gameinfo.gi"
else
    # 3. Install Metamod if not installed or version changed
    if [ -n "${METAMODURL}" ]; then
        INSTALLED_METAMOD=$(cat "${METAMOD_MARKER}" 2>/dev/null || echo "")
        if [ "${INSTALLED_METAMOD}" != "${METAMODURL}" ]; then
            echo "[DreamLeague] Installing Metamod..."
            cd "${STEAMAPPDIR}"
            curl -fsSL "${METAMODURL}" | tar xvzf - -C ./game/csgo/
            echo "${METAMODURL}" > "${METAMOD_MARKER}"
            echo "[DreamLeague] Metamod installed."
        fi
    fi

    # 4. Install MatchZy (+ CounterStrikeSharp) if not installed or version changed
    if [ -n "${MATCHZYURL}" ]; then
        INSTALLED_MATCHZY=$(cat "${MATCHZY_MARKER}" 2>/dev/null || echo "")
        if [ "${INSTALLED_MATCHZY}" != "${MATCHZYURL}" ]; then
            echo "[DreamLeague] Installing MatchZy..."
            cd "${STEAMAPPDIR}/game/csgo"
            wget -O /tmp/matchzy.zip "${MATCHZYURL}" \
                && unzip -o /tmp/matchzy.zip \
                && rm -f /tmp/matchzy.zip
            # Rename MatchZy → DreamLeague in config
            sed -i 's/MatchZy/DreamLeague/g' ./cfg/MatchZy/config.cfg 2>/dev/null || true
            # Save a pristine template before MongoDB credentials are injected
            cp "${MATCHZY_CONFIG}" "${MATCHZY_CONFIG_TMPL}"
            echo "${MATCHZYURL}" > "${MATCHZY_MARKER}"
            echo "[DreamLeague] MatchZy installed."
        fi
    fi

    # 5. Inject MongoDB connection into MatchZy config (runs every start so env
    #    var changes are picked up without reinstalling the plugin)
    if [ -f "${MATCHZY_CONFIG_TMPL}" ]; then
        cp "${MATCHZY_CONFIG_TMPL}" "${MATCHZY_CONFIG}"
        sed -i \
            -e "s|{{DREAMLEAGUE_MATCH_DATABASE}}|${DREAMLEAGUE_MATCH_DATABASE}|g" \
            -e "s|{{DREAMLEAGUE_MATCH_DATABASE_NAME}}|${DREAMLEAGUE_MATCH_DATABASE_NAME}|g" \
            -e "s|{{DREAMLEAGUE_MATCH_DATABASE_COLLECTION}}|${DREAMLEAGUE_MATCH_DATABASE_COLLECTION}|g" \
            -e "s|{{DREAMLEAGUE_API_URL}}|${DREAMLEAGUE_API_URL}|g" \
            "${MATCHZY_CONFIG}"
    fi

    # 6. Patch gameinfo.gi to load Metamod — idempotent
    if [ -n "${METAMODURL}" ]; then
        if ! grep -q "Game csgo/addons/metamod" "${STEAMAPPDIR}/game/csgo/gameinfo.gi"; then
            echo "[DreamLeague] Patching gameinfo.gi for Metamod..."
            LOWVIOLENCE_LINE=$(grep -m 1 -n 'Game_LowViolence' "${STEAMAPPDIR}/game/csgo/gameinfo.gi" | cut -f1 -d:)
            LOWVIOLENCE_LINE=$((LOWVIOLENCE_LINE + 1))
            sed -i "${LOWVIOLENCE_LINE} i\\\\t\\t\\t\\tGame csgo/addons/metamod" "${STEAMAPPDIR}/game/csgo/gameinfo.gi"
        fi
    fi
fi

# ---------------------------------------------------------------------------
# 7. Apply server.cfg template
# ---------------------------------------------------------------------------
cp /etc/server.cfg "${STEAMAPPDIR}/game/csgo/cfg/server.cfg"
sed -i \
    -e "s/{{SERVER_HOSTNAME}}/${CS2_SERVERNAME}/g" \
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

# ---------------------------------------------------------------------------
# 8. Start CS2
# ---------------------------------------------------------------------------
# Use cs2.sh so it sets LD_LIBRARY_PATH (needed for libv8.so), ulimits, and
# working directory correctly — same as Valve's own launcher does.
if [[ -z "${CS2_IP}" ]]; then
    CS2_IP_ARGS=""
else
    CS2_IP_ARGS="-ip ${CS2_IP}"
fi

echo "[DreamLeague] Starting CS2 dedicated server..."

exec "${STEAMAPPDIR}/game/cs2.sh" \
    -dedicated \
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
