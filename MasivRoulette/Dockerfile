#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
MAINTAINER Jose Bobadilla Castro "jfbobadillac@gmail.com"

ARG BUILDCONFIG=RELEASE
ARG VERSION=1.0.0

WORKDIR /build
COPY *.csproj ./

RUN dotnet restore

COPY . ./

RUN dotnet publish -c $BUILDCONFIG -o out


FROM mcr.microsoft.com/dotnet/core/sdk:3.1
MAINTAINER Jose Bobadilla Castro "jfbobadillac@gmail.com"

ENV REDISHOST=172.17.0.3
ENV REDISPORT=6379

EXPOSE 5000

WORKDIR /app

COPY --from=build /build/out .
ENTRYPOINT ["dotnet", "MasivRoulette.dll"]