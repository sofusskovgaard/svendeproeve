FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Template.Services.ServiceName/Template.Services.ServiceName.csproj", "Template.Services.ServiceName/"]
RUN dotnet restore "Template.Services.ServiceName/Template.Services.ServiceName.csproj"
COPY . .
WORKDIR "/src/Template.Services.ServiceName"
RUN dotnet build "Template.Services.ServiceName.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Template.Services.ServiceName.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Template.Services.ServiceName.dll"]
