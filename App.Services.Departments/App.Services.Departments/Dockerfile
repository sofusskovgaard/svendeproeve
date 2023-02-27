FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["App.Services.Departments/App.Services.Departments/App.Services.Departments.csproj", "App.Services.Departments/"]
RUN dotnet restore "App.Services.Departments/App.Services.Departments.csproj"
COPY . .
WORKDIR "/src/App.Services.Departments"
RUN dotnet build "App.Services.Departments/App.Services.Departments.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App.Services.Departments/App.Services.Departments.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.Services.Departments.dll"]
