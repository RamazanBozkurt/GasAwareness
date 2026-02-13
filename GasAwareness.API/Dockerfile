FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["GasAwareness.API/GasAwareness.API.csproj", "GasAwareness.API/"]
RUN dotnet restore "GasAwareness.API/GasAwareness.API.csproj"

COPY . .

WORKDIR "/src/GasAwareness.API"
RUN dotnet build "GasAwareness.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GasAwareness.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GasAwareness.API.dll"]