FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Basket/WebApi/WebApi.csproj", "src/Services/Basket/WebApi/"]
COPY ["src/BuildingBlock/cShop.Infrastructure/cShop.Infrastructure.csproj", "src/BuildingBlock/cShop.Infrastructure/"]
COPY ["src/BuildingBlock/cShop.Core/cShop.Core.csproj", "src/BuildingBlock/cShop.Core/"]
COPY ["src/BuildingBlock/cShop.Contracts/cShop.Contracts.csproj", "src/BuildingBlock/cShop.Contracts/"]
COPY ["src/Services/Basket/EventStore/EventStore.csproj", "src/Services/Basket/EventStore/"]
COPY ["src/Services/Basket/Application/Application.csproj", "src/Services/Basket/Application/"]
COPY ["src/Services/Basket/Domain/Domain.csproj", "src/Services/Basket/Domain/"]
COPY ["src/Services/Basket/Bus/Bus.csproj", "src/Services/Basket/Bus/"]
COPY ["src/Services/Basket/Projection/Projection.csproj", "src/Services/Basket/Projection/"]
COPY ["src/Services/Basket/Infrastructure/Infrastructure.csproj", "src/Services/Basket/Infrastructure/"]
RUN dotnet restore "src/Services/Basket/WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/src/Services/Basket/WebApi"
RUN dotnet build "WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
