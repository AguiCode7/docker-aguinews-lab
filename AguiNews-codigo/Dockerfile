# syntax=docker/dockerfile:1
# Ápi de AguiNews en dos etapas: el "taller" compila y la imagen final solo ejecuta.

# Etapa 1: SDK de .NET con todas las herramientas para compilar.
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Primero solo los proyectos: esta capa (y el restore) se reutiliza
# mientras no cambien las dependencias, aunque cambie el código.
COPY AguiNews.sln ./
COPY src/AC.API/AC.API.csproj src/AC.API/
COPY src/AC.Core/AC.Core.csproj src/AC.Core/
COPY src/AC.Infrastructure/AC.Infrastructure.csproj src/AC.Infrastructure/
RUN dotnet restore src/AC.API/AC.API.csproj

# Después el código, que es lo que cambia todo el tiempo.
COPY src/ src/
RUN dotnet publish src/AC.API/AC.API.csproj -c Release -o /app --no-restore

# Etapa 2: solo el runtime de ASP.NET Core y el resultado publicado.
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app .
# Usuario sin privilegios que ya trae la imagen oficial (nunca root).
USER $APP_UID
# Las imágenes de .NET 8+ escuchan en el 8080 por defecto.
EXPOSE 8080
ENTRYPOINT ["dotnet", "AC.API.dll"]
