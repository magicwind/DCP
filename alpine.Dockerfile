﻿FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /app

COPY . .
RUN dotnet publish "./DCP/DCP.csproj" -c Release -o /app/out
COPY docker-entrypoint.sh /app/out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# install libgdiplus for System.Drawing
RUN apk add libgdiplus --update-cache --repository http://dl-cdn.alpinelinux.org/alpine/edge/testing/ --allow-untrusted && \
    apk add terminus-font

ENV ASPNETCORE_URLS http://+:80
ENV ASPNETCORE_ENVIRONMENT Production
ENV DB_CONN_STR_DEFAULT "Server=localhost; Database=dcp; User=root; Password=root;"

ENTRYPOINT ["sh", "docker-entrypoint.sh"]