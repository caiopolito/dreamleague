#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 5000 5001

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["../api/src/dreamleague.api/dreamleague.api.csproj", "dreamleague.api/"]
COPY ["../api/src/dreamleague.common/dreamleague.common.csproj", "dreamleague.common/"]
RUN dotnet restore "dreamleague.api/dreamleague.api.csproj"
COPY ["../api", "."]
WORKDIR "/src/src/dreamleague.api"
RUN dotnet build "dreamleague.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dreamleague.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dreamleague.api.dll"]