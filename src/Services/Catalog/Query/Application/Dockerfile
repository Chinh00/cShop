FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Catalog/Query/Application/Application.csproj", "src/Services/Catalog/Query/Application/"]
COPY ["src/BuildingBlock/cShop.Infrastructure/cShop.Infrastructure.csproj", "src/BuildingBlock/cShop.Infrastructure/"]
COPY ["src/BuildingBlock/cShop.Core/cShop.Core.csproj", "src/BuildingBlock/cShop.Core/"]
COPY ["src/Services/Catalog/Query/Projections/Projections.csproj", "src/Services/Catalog/Query/Projections/"]
COPY ["src/Services/Catalog/Query/EventBus/EventBus.csproj", "src/Services/Catalog/Query/EventBus/"]
COPY ["src/BuildingBlock/cShop.Contracts/cShop.Contracts.csproj", "src/BuildingBlock/cShop.Contracts/"]
RUN dotnet restore "src/Services/Catalog/Query/Application/Application.csproj"
COPY . .
WORKDIR "/src/src/Services/Catalog/Query/Application"
RUN dotnet build "Application.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Application.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Application.dll"]
