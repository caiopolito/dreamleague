FROM mcr.microsoft.com/mssql/server:2022-latest

# Exposing mssql port
EXPOSE 1433:1433

# Set user as root
USER root

# Create a config directory
RUN mkdir -p /usr/config
WORKDIR /usr/config

# Bundle config source
COPY . /usr/config

RUN chmod +x ./entrypoint.sh
RUN chmod +x ./configure-db.sh

HEALTHCHECK --interval=10s --timeout=3s --start-period=10s --retries=10 \
    CMD /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P ${MSSQL_SA_PASSWORD} -Q "SELECT 1" || exit 1

ENTRYPOINT ["./entrypoint.sh"]