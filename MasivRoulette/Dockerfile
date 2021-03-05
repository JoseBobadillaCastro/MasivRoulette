#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["MasivRoulette/MasivRoulette.csproj", "MasivRoulette/"]
RUN dotnet restore "MasivRoulette/MasivRoulette.csproj"
COPY . .
WORKDIR "/src/MasivRoulette"
RUN dotnet build "MasivRoulette.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MasivRoulette.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MasivRoulette.dll"]