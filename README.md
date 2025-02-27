
# MemberService Microservice

MemberService es un microservicio desarrollado en .NET 8 que forma parte del sistema ERP para la gestión de miembros (afiliados). Este servicio expone una API RESTful para realizar operaciones CRUD (crear, leer, actualizar y eliminar miembros) utilizando Entity Framework Core, migraciones, seeders y buenas prácticas (DTOs, repositorios, servicios, AutoMapper, etc.). Además, el proyecto está contenerizado con Docker y se orquesta mediante Docker Compose.

## Características

- **API RESTful:** Endpoints para la administración de miembros.
- **Entity Framework Core:** Uso de Fluent API, migraciones y seeders para gestionar el esquema de la base de datos.
- **Buenas Prácticas de Desarrollo:** Separación en capas (Controladores, Servicios, Repositorios), uso de DTOs y AutoMapper, inyección de dependencias y aplicación de principios SOLID.
- **Contenerización con Docker:** Incluye un Dockerfile para construir la imagen del microservicio y un docker-compose.yml para levantar el microservicio junto con SQL Server.
- **Configuración Flexible:** Manejo de variables de entorno para separar la configuración de desarrollo y producción. Se incluye un archivo `.env.example` para guiar la configuración.

## Estructura del Proyecto

La estructura del repositorio es la siguiente:

```
ErpMicroservices/
├── MemberService/
│   ├── MemberService/                 # Carpeta principal del microservicio
│   │   ├── Controllers/               # Controladores de la API
│   │   ├── Data/                      # DbContext, configuraciones, migraciones y seeders
│   │   │   ├── Configurations/
│   │   │   ├── Migrations/
│   │   │   └── Seeders/
│   │   ├── DTOs/                      # Data Transfer Objects
│   │   ├── Models/                    # Entidades del dominio (e.g., Member)
│   │   ├── Repositories/              # Interfaces e implementaciones para acceso a datos
│   │   ├── Services/                  # Interfaces e implementaciones de lógica de negocio (e.g., MemberManagementService)
│   │   ├── Mappers/                   # Configuración de AutoMapper (MappingProfile)
│   │   ├── Program.cs                 # Configuración del host y DI
│   │   ├── MemberService.csproj       # Archivo de proyecto .NET
│   │   ├── Dockerfile                 # Dockerfile para construir la imagen del microservicio
│   │   ├── docker-compose.yml         # Orquestación local con SQL Server
│   │   ├── docker.env                 # Variables de entorno para producción (no sensibles)
│   │   ├── .env.local                 # Variables de entorno para desarrollo
│   │   ├── .env.production            # Variables de entorno para producción (no subir datos sensibles)
│   │   ├── .env.example               # Archivo de ejemplo para la configuración de variables de entorno
│   │   ├── appsettings.json           # Configuración general de la aplicación
│   │   └── appsettings.Development.json  # Configuración específica para desarrollo
└── .gitignore                         # Exclusiones de archivos (bin, obj, etc.)
```

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- SQL Server (se levanta mediante Docker Compose, versión 2022-latest en este ejemplo)
- (Opcional) Visual Studio o cualquier editor compatible con .NET

## Configuración de Variables de Entorno

El proyecto utiliza diferentes archivos de entorno para separar la configuración entre desarrollo y producción.

### Para Desarrollo (Local)

Crea un archivo llamado **.env.local** (ya se incluye un ejemplo en `.env.example`):

```env
DB_CONNECTION=Server=localhost;Database=DatabaseName;User Id=UserId;Password=DatabasePassword*;TrustServerCertificate=True;
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://+:8080
```

### Para Producción

Crea un archivo llamado **.env.production** basado en **.env.example**:

```env
DB_CONNECTION=Server=db,1433;Database=DatabaseName;User Id=UserId;Password=DatabasePassword*;TrustServerCertificate=True;
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://+:8080
```

> **Importante:**  
> No subas archivos con datos sensibles. Usa `.env.example` para guiar la configuración y excluye los archivos reales (.env.production, .env.local) en el archivo `.gitignore`.

## Cómo Ejecutar el Proyecto

### Ejecutar Localmente con .NET

1. **Configura tu entorno:**  
   Asegúrate de tener el archivo `.env.local` en la raíz del microservicio.

2. **Restaura las dependencias**
   ```bash
   dotnet restore
   ```
3. **Ejecuta la aplicación:**
   ```bash
   dotnet run
   ```
   La API estará disponible en `http://localhost:8080`.

5. **Probar la API:**
   - Accede a `http://localhost:8080/swagger` (si Swagger está habilitado en desarrollo).
   - O usa Postman para hacer peticiones a los endpoints, por ejemplo, `http://localhost:8080/api/members`.

### Ejecutar con Docker Compose

1. **Abre una terminal en la carpeta donde se encuentra el docker-compose.yml.**

2. **Construye y levanta los contenedores:**
   ```bash
   docker-compose up --build
   ```
   Esto levantará dos contenedores:
   - **db:** SQL Server (expuesto en el puerto 1433 y con volumen persistente).
   - **memberservice:** Tu microservicio (expuesto en el puerto 8080).

3. **Verifica que los contenedores estén en ejecución:**
   ```bash
   docker ps
   ```

4. **Probar la API:**
   - Accede a `http://localhost:8080/swagger` (si Swagger está habilitado en desarrollo) o prueba los endpoints con Postman.

## Construcción de la Imagen Docker

El **Dockerfile** de este proyecto realiza lo siguiente:

1. Usa la imagen de .NET SDK para construir la aplicación.
2. Restaura dependencias, copia el código y publica la aplicación en modo Release.
3. Usa la imagen de .NET ASP.NET para ejecutar la aplicación.
4. Expone el puerto 8080 y configura la variable de entorno `ASPNETCORE_URLS` para que la aplicación escuche en `http://*:8080`.

## Despliegue en Render con CI/CD

El proyecto está preparado para despliegues automáticos. Render se conectará a tu repositorio en GitHub y, cada vez que se haga push a la rama **main**, reconstruirá la imagen y actualizará el servicio.

### Pasos para Configurar Render

1. **Conecta tu repositorio a Render:**  
   Asegúrate de que el repositorio contiene la estructura completa y que Render tenga acceso a él.

2. **Crea un nuevo Web Service en Render:**
   - **Build Context:** Configura como `MemberService/MemberService` (donde se encuentra el Dockerfile).
   - **Dockerfile Path:** `Dockerfile`
   - **Puerto:** 8080
   - **Variables de Entorno en Render:** Configura las variables de producción:
     - `DB_CONNECTION=Server=db,1433;Database=DatabaseName;User Id=UserId;Password=DatabasePassword*;TrustServerCertificate=True;`
     - `ASPNETCORE_ENVIRONMENT=Production`
     - `ASPNETCORE_URLS=http://+:8080`
     
3. **Crear el Servicio SQL Server (opcional):**  
   Si deseas que SQL Server se despliegue en Render, crea otro servicio usando la imagen oficial o un Dockerfile personalizado para SQL Server. Nómbralo **db** para que tu connection string se resuelva correctamente.

4. **Habilita Auto Deploy:**  
   En el dashboard de Render, habilita la opción de Auto Deploy para la rama **main**.

## CI/CD (Opcional) con GitHub Actions

Si deseas complementar la integración nativa de Render, puedes configurar un workflow en GitHub Actions en la carpeta `.github/workflows`. Por ejemplo:

```yaml
name: Deploy to Render

on:
  push:
    branches:
      - main

jobs:
  deploy:
    runs-on: ubuntu-latest

    steps:
      - name: Checkout code
        uses: actions/checkout@v3

      - name: Build Docker image
        run: docker build -t memberservice:latest .

      # (Opcional) Trigger Render Deploy via API
      - name: Trigger Render Deploy
        run: |
          curl -X POST https://api.render.com/v1/deploy/srv-<your_service_id>/deploys \
               -H "Authorization: Bearer ${{ secrets.RENDER_API_KEY }}" \
               -H "Content-Type: application/json" \
               -d '{}'
```

Configura los secretos necesarios (como `RENDER_API_KEY`) en GitHub si decides usar esta opción.

## Notas de Seguridad

- **No subas datos sensibles:**  
  Asegúrate de que los archivos `.env.production` y otros con información sensible estén en el `.gitignore` y utiliza `.env.example` para guiar la configuración.
- **Variables de entorno:**  
  Configura las variables sensibles directamente en el dashboard de Render para producción.

## Conclusión

Este README.md proporciona todas las instrucciones necesarias para que otro desarrollador pueda:
- Configurar y ejecutar el microservicio MemberService en entornos locales y de producción.
- Entender la estructura del proyecto.
- Desplegar la aplicación utilizando Docker y Render con un flujo CI/CD.
- Configurar las variables de entorno de manera segura.

¡Con esto, el microservicio está listo para ser usado y desplegado sin complicaciones!
