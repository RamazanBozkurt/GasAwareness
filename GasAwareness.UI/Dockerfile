FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["GasAwareness.UI/GasAwareness.UI.csproj", "GasAwareness.UI/"]
COPY ["GasAwareness.API/GasAwareness.API.csproj", "GasAwareness.API/"]

RUN dotnet restore "GasAwareness.UI/GasAwareness.UI.csproj"

COPY . .

WORKDIR "/src/GasAwareness.UI"
RUN dotnet build "GasAwareness.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GasAwareness.UI.csproj" -c Release -o /app/publish /p:UseAppHost=false /p:ErrorOnDuplicatePublishOutputFiles=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GasAwareness.UI.dll"]