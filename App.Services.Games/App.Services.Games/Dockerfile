FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["App.Services.Games/App.Services.Games/App.Services.Games.csproj", "App.Services.Games/"]
RUN dotnet restore "App.Services.Games/App.Services.Games.csproj"
COPY . .
WORKDIR "/src/App.Services.Games"
RUN dotnet build "App.Services.Games/App.Services.Games.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App.Services.Games/App.Services.Games.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.Services.Games.dll"]
