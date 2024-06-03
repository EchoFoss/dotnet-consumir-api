FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ConsumirApi/ConsumirApi.csproj", "ConsumirApi/"]
COPY ["WebApi/WebApi.csproj", "WebApi/"]
RUN dotnet restore "ConsumirApi/ConsumirApi.csproj"
COPY . .
WORKDIR "/src/ConsumirApi"
RUN dotnet build "ConsumirApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ConsumirApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsumirApi.dll"]
