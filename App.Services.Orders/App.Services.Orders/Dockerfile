FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["App.Services.Orders/App.Services.Orders/App.Services.Orders.csproj", "App.Services.Orders/"]
RUN dotnet restore "App.Services.Orders/App.Services.Orders.csproj"
COPY . .
WORKDIR "/src/App.Services.Orders"
RUN dotnet build "App.Services.Orders/App.Services.Orders.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App.Services.Orders/App.Services.Orders.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.Services.Orders.dll"]
