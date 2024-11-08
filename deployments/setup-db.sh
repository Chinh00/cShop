#!/bin/bash

sleep 30s
echo "Start migrate database"

/opt/mssql-tools18/bin/sqlcmd -S localohost -U sa -P @P@ssw0rd02 -C -i cshop.sql
