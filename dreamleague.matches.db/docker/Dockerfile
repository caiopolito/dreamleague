FROM mongo:latest

HEALTHCHECK --interval=10s --timeout=3s --start-period=10s --retries=10 \
    CMD echo 'db.runCommand("ping").ok' | mongosh localhost:27017/test --quiet