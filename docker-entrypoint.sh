/bin/sh -c 'sed -i "s/%CONN_STR_DEFAULT_VALUE%/${DB_CONN_STR_DEFAULT}/g" appsettings.json'
cat appsettings.json | grep Database=
dotnet DCP.dll