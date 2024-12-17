FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Simu.Api/Simu.Api/Simu.Api.csproj", "src/Simu.Api/Simu.Api/"]
RUN dotnet restore "src/Simu.Api/Simu.Api/Simu.Api.csproj"
COPY . .
WORKDIR "/src/src/Simu.Api/Simu.Api"
RUN dotnet build "Simu.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Simu.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Simu.Api.dll"]
