# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar los archivos de proyecto que existen
COPY backend-pub-sub.sln .
COPY SubscriptoAplicaction/*.csproj ./SubscriptoAplicaction/
COPY CommunWork/*.csproj ./CommunWork/

# Restaurar las dependencias solo para los proyectos existentes
RUN dotnet restore "./SubscriptoAplicaction/SubscriptoAplicaction.csproj"

# Copiar todo el código fuente y compilar la aplicación
COPY . .
WORKDIR /app/SubscriptoAplicaction
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80

# Definir el comando de inicio de la aplicación
ENTRYPOINT ["dotnet", "SubscriptoAplicaction.dll"]
