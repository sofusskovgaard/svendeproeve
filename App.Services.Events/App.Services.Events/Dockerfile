FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["App.Services.Events/App.Services.Events/App.Services.Events.csproj", "App.Services.Events/"]
RUN dotnet restore "App.Services.Events/App.Services.Events.csproj"
COPY . .
WORKDIR "/src/App.Services.Events"
RUN dotnet build "App.Services.Events/App.Services.Events.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App.Services.Events/App.Services.Events.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.Services.Events.dll"]
