FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Catalog/Command/Application/Application.csproj", "src/Services/Catalog/Command/Application/"]
COPY ["src/BuildingBlock/cShop.Infrastructure/cShop.Infrastructure.csproj", "src/BuildingBlock/cShop.Infrastructure/"]
COPY ["src/BuildingBlock/cShop.Core/cShop.Core.csproj", "src/BuildingBlock/cShop.Core/"]
COPY ["src/Services/Catalog/Command/MessageBus/MessageBus.csproj", "src/Services/Catalog/Command/MessageBus/"]
COPY ["src/BuildingBlock/cShop.Contracts/cShop.Contracts.csproj", "src/BuildingBlock/cShop.Contracts/"]
COPY ["src/Services/Catalog/Command/EventStore/EventStore.csproj", "src/Services/Catalog/Command/EventStore/"]
COPY ["src/BuildingBlock/cShop.Infrastructure.EfCore/cShop.Infrastructure.EfCore.csproj", "src/BuildingBlock/cShop.Infrastructure.EfCore/"]
COPY ["src/Services/Catalog/Command/Domain/Domain.csproj", "src/Services/Catalog/Command/Domain/"]
COPY ["src/BuildingBlock/cShop.Infrastructure.Ole/cShop.Infrastructure.Ole.csproj", "src/BuildingBlock/cShop.Infrastructure.Ole/"]
RUN dotnet restore "src/Services/Catalog/Command/Application/Application.csproj"
COPY . .
WORKDIR "/src/src/Services/Catalog/Command/Application"
RUN dotnet build "Application.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Application.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Application.dll"]
