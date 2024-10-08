﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Catalog/WebApi/WebApi.csproj", "src/Services/Catalog/WebApi/"]
COPY ["src/BuildingBlock/cShop.Infrastructure/cShop.Infrastructure.csproj", "src/BuildingBlock/cShop.Infrastructure/"]
COPY ["src/BuildingBlock/cShop.Core/cShop.Core.csproj", "src/BuildingBlock/cShop.Core/"]
COPY ["src/BuildingBlock/cShop.Contracts/cShop.Contracts.csproj", "src/BuildingBlock/cShop.Contracts/"]
COPY ["src/Services/Catalog/Projection/Projection.csproj", "src/Services/Catalog/Projection/"]
COPY ["src/Services/Catalog/GrpcService/GrpcService.csproj", "src/Services/Catalog/GrpcService/"]
COPY ["src/Services/Catalog/EventStore/EventStore.csproj", "src/Services/Catalog/EventStore/"]
COPY ["src/Services/Catalog/Bus/Bus.csproj", "src/Services/Catalog/Bus/"]
COPY ["src/Services/Catalog/Application/Application.csproj", "src/Services/Catalog/Application/"]
COPY ["src/Services/Catalog/Domain/Domain.csproj", "src/Services/Catalog/Domain/"]
RUN dotnet restore "src/Services/Catalog/WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/src/Services/Catalog/WebApi"
RUN dotnet build "WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
