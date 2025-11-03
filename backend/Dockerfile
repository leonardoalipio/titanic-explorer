# Dockerfile para API .NET 8
# Estágio de build
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia os arquivos de projeto de cada camada e restaura
COPY ["./src/Api/TitanicExplorer.Api.csproj", "Api/"]
COPY ["./src/Business/TitanicExplorer.Business.csproj", "Business/"]
COPY ["./src/Data/TitanicExplorer.Data.csproj", "Data/"]
RUN dotnet restore "./Api/./TitanicExplorer.Api.csproj"
COPY . .
WORKDIR "/src/Api"


RUN dotnet build "TitanicExplorer.Api.csproj" -c Release -o /app/build

RUN dotnet publish "TitanicExplorer.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "SuaApi.dll"]