FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/IdentityServer/IdentityServer.csproj", "src/Services/IdentityServer/"]
COPY ["src/BuildingBlock/cShop.Infrastructure/cShop.Infrastructure.csproj", "src/BuildingBlock/cShop.Infrastructure/"]
COPY ["src/BuildingBlock/cShop.Contracts/cShop.Contracts.csproj", "src/BuildingBlock/cShop.Contracts/"]
COPY ["src/BuildingBlock/cShop.Core/cShop.Core.csproj", "src/BuildingBlock/cShop.Core/"]
RUN dotnet restore "src/Services/IdentityServer/IdentityServer.csproj"
COPY . .
WORKDIR "/src/src/Services/IdentityServer"
RUN dotnet build "IdentityServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "IdentityServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityServer.dll"]
