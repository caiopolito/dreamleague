#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN dotnet dev-certs https --trust
WORKDIR /src
COPY ["../api/src/dreamleague.api/dreamleague.api.csproj", "dreamleague.api/"]
RUN dotnet restore "dreamleague.api/dreamleague.api.csproj"
COPY ["../api", "."]
WORKDIR "/src/src/dreamleague.api"
RUN dotnet dev-certs https --trust
RUN dotnet build "dreamleague.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dreamleague.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dreamleague.api.dll"]