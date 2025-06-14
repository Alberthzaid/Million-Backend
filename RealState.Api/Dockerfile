# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar archivos de la solución
COPY *.sln ./
COPY RealState.Domain/*.csproj ./RealState.Domain/
COPY RealState.Application/*.csproj ./RealState.Application/
COPY RealState.DTO/*.csproj ./RealState.DTO/
COPY RealState.Infrastructure/*.csproj ./RealState.Infrastructure/
COPY RealState.Api/*.csproj ./RealState.Api/

# Restaurar paquetes
RUN dotnet restore RealState.Api/RealState.Api.csproj

# Copiar el resto de los archivos
COPY . ./

# Publicar la app
RUN dotnet publish RealState.Api/RealState.Api.csproj -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Exponer el puerto por defecto
EXPOSE 80

ENTRYPOINT ["dotnet", "RealState.Api.dll"]
