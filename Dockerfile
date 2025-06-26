# Consulte https://aka.ms/customizecontainer para aprender a personalizar el contenedor de depuracion 

#Esta fase se usa cuando se ejecuta desde VS en modo rapido 
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base 
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Esta fase se usa para copilar el proyecto de servicio 
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["uttt.Micro.Libro.csproj","."]
RUN	dotnet restore "./uttt.Micro.Libro.csproj"
COPY . .
WORKDIR	"/src/."
RUN dotnet build "./uttt.Micro.Libro.csproj" -c $BUILD_CONFIGURATION -o /app/build

#Esta fase se usa para publicar el proyecto del servicio que se copiara en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./uttt.Micro.Libro.csproj" -c  $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

#Esta fase se usa en produccion o cuando se ejecuta desde VS en modo normal 
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "uttt.Micro.Libro.dll"]