FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["App.Services.Tickets/App.Services.Tickets/App.Services.Tickets.csproj", "App.Services.Tickets/"]
RUN dotnet restore "App.Services.Tickets/App.Services.Tickets.csproj"
COPY . .
WORKDIR "/src/App.Services.Tickets"
RUN dotnet build "App.Services.Tickets/App.Services.Tickets.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App.Services.Tickets/App.Services.Tickets.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.Services.Tickets.dll"]
