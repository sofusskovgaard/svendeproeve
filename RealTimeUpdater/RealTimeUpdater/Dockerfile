FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RealTimeUpdater/RealTimeUpdater/RealTimeUpdater.csproj", "RealTimeUpdater/"]
RUN dotnet restore "RealTimeUpdater/RealTimeUpdater.csproj"
COPY . .
WORKDIR "/src/RealTimeUpdater"
RUN dotnet build "RealTimeUpdater/RealTimeUpdater.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RealTimeUpdater/RealTimeUpdater.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RealTimeUpdater.dll"]
