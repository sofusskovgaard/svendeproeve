FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["App.Services.Turnaments/App.Services.Turnaments/App.Services.Turnaments.csproj", "App.Services.Turnaments/"]
RUN dotnet restore "App.Services.Turnaments/App.Services.Turnaments.csproj"
COPY . .
WORKDIR "/src/App.Services.Turnaments"
RUN dotnet build "App.Services.Turnaments/App.Services.Turnaments.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App.Services.Turnaments/App.Services.Turnaments.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.Services.Turnaments.dll"]
