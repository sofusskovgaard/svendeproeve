FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["App.Services.Billing/App.Services.Billing/App.Services.Billing.csproj", "App.Services.Billing/"]
RUN dotnet restore "App.Services.Billing/App.Services.Billing.csproj"
COPY . .
WORKDIR "/src/App.Services.Billing"
RUN dotnet build "App.Services.Billing/App.Services.Billing.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App.Services.Billing/App.Services.Billing.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.Services.Billing.dll"]
