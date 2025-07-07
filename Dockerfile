# Imagen base en tiempo de ejecución (producción)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000

# Imagen para compilación
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar y restaurar
COPY ["uttt.Micro.Libro.csproj", "."]
RUN dotnet restore "./uttt.Micro.Libro.csproj"

# Copiar el resto del código
COPY . .

# Compilar
RUN dotnet build "./uttt.Micro.Libro.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicar
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./uttt.Micro.Libro.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "uttt.Micro.Libro.dll"]
