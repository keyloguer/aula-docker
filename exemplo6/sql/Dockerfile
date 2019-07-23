FROM microsoft/mssql-server-linux:2017-CU12
COPY ./script.sql .
ENV ACCEPT_EULA Y
ENV SA_PASSWORD Your_password123
RUN (/opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "Service Broker manager has started" && /opt/mssql-tools/bin/sqlcmd -S 0.0.0.0 -U SA -P $SA_PASSWORD -i script.sql 