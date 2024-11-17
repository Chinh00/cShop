FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Catalog/WebApi/WebApi.csproj", "src/Services/Catalog/WebApi/"]
COPY ["src/BuildingBlock/cShop.Infrastructure/cShop.Infrastructure.csproj", "src/BuildingBlock/cShop.Infrastructure/"]
COPY ["src/BuildingBlock/cShop.Contracts/cShop.Contracts.csproj", "src/BuildingBlock/cShop.Contracts/"]
COPY ["src/BuildingBlock/cShop.Core/cShop.Core.csproj", "src/BuildingBlock/cShop.Core/"]
COPY ["src/Services/Catalog/Application/Application.csproj", "src/Services/Catalog/Application/"]
COPY ["src/Services/Catalog/Domain/Domain.csproj", "src/Services/Catalog/Domain/"]
COPY ["src/Services/Catalog/Infrastructure/Infrastructure.csproj", "src/Services/Catalog/Infrastructure/"]
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
