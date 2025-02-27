# Dockerfile (actualizado)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar csproj y restaurar dependencias
COPY *.csproj ./
RUN dotnet restore --no-cache

# Copiar el resto del código
COPY . ./
RUN dotnet publish -c Release -o out 

# Imagen de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 8080

# El .env no se copia a la imagen de producción
# Las variables de entorno se pasan al contenedor al ejecutarlo
ENV ASPNETCORE_URLS=http://*:8080

ENTRYPOINT ["dotnet", "MemberService.dll"]