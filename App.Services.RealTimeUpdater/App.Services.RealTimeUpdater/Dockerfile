FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["App.Services.RealTimeUpdater/App.Services.RealTimeUpdater/App.Services.RealTimeUpdater.csproj", "App.Services.RealTimeUpdater/"]
RUN dotnet restore "App.Services.RealTimeUpdater/App.Services.RealTimeUpdater.csproj"
COPY . .
WORKDIR "/src/App.Services.RealTimeUpdater"
RUN dotnet build "App.Services.RealTimeUpdater/App.Services.RealTimeUpdater.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App.Services.RealTimeUpdater/App.Services.RealTimeUpdater.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.Services.RealTimeUpdater.dll"]
