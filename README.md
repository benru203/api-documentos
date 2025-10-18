# 📚 API de Gestión de Documentos

API REST desarrollada en .NET Core 8 para la gestión de documentos, implementando **Arquitectura Hexagonal**, **Principios SOLID**, **TDD** y **Patrón Repository** con Entity Framework Core.

![.NET Core 8](https://img.shields.io/badge/.NET%20Core-8.0-purple?logo=dotnet)
![C#](https://img.shields.io/badge/C%23-12.0-blue?logo=csharp)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-8.0-green)
![License](https://img.shields.io/badge/license-MIT-blue.svg)

---

## 📋 Tabla de Contenidos

- [Características](#-características)
- [Arquitectura](#-arquitectura)
- [Prerequisitos](#-prerequisitos)
- [Instalación](#-instalación)
- [Configuración](#-configuración)
- [Ejecución](#-ejecución)
- [Pruebas](#-pruebas)
- [Endpoints](#-endpoints-api)
- [Estructura del Proyecto](#-estructura-del-proyecto)
- [Tecnologías](#-tecnologías)
- [Principios Aplicados](#-principios-aplicados)

---

## ✨ Características

### Funcionalidades
- ✅ Crear, leer, actualizar y eliminar documentos (CRUD completo)
- ✅ Listado paginado de documentos
- ✅ Búsqueda avanzada por autor, tipo y estado
- ✅ Validaciones de negocio en la capa de dominio
- ✅ Manejo de estados del documento (Registrado, Pendiente, Validado, Archivado)
- ✅ Documentación automática con Swagger/OpenAPI

### Arquitectura y Patrones
- 🏗️ **Arquitectura Hexagonal** (Puertos y Adaptadores)
- 🎯 **Principios SOLID**
- 🧪 **Test-Driven Development (TDD)**
- 📦 **Patrón Repository**
- 🔄 **Dependency Injection**
- 📝 **Domain-Driven Design (DDD)**

---

## 🏛️ Arquitectura

```
┌─────────────────────────────────────────────────────────────┐
│                    CAPA DE PRESENTACIÓN                      │
│                  (DocumentosAPI.API)                         │
│                   Controllers + Swagger                       │
└───────────────────────────┬─────────────────────────────────┘
                            │
┌───────────────────────────▼─────────────────────────────────┐
│                   CAPA DE APLICACIÓN                         │
│              (DocumentosAPI.Application)                     │
│            Services + DTOs + Casos de Uso                    │
└───────────────────────────┬─────────────────────────────────┘
                            │
┌───────────────────────────▼─────────────────────────────────┐
│                    CAPA DE DOMINIO                           │
│                (DocumentosAPI.Domain)                        │
│        Entidades + Interfaces + Reglas de Negocio           │
└───────────────────────────┬─────────────────────────────────┘
                            │
┌───────────────────────────▼─────────────────────────────────┐
│                 CAPA DE INFRAESTRUCTURA                      │
│             (DocumentosAPI.Infrastructure)                   │
│       Repositorios + EF Core + DbContext                     │
└───────────────────────────┬─────────────────────────────────┘
                            │
                    ┌───────▼───────┐
                    │  SQL Server   │
                    └───────────────┘
```

---

## 📦 Prerequisitos

### Software Obligatorio

#### 1. .NET 8 SDK
**Versión mínima:** 8.0.0

**Instalación:**

**Windows:**
```powershell
# Descargar desde:
https://dotnet.microsoft.com/download/dotnet/8.0

# Verificar instalación
dotnet --version
# Output esperado: 8.0.x
```

**macOS:**
```bash
# Con Homebrew
brew install --cask dotnet-sdk

# Verificar instalación
dotnet --version
```

**Linux (Ubuntu/Debian):**
```bash
# Añadir repositorio de Microsoft
wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

# Instalar SDK
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0

# Verificar instalación
dotnet --version
```

#### 2. SQL Server

**Opción A: SQL Server LocalDB (Recomendado para desarrollo)**

**Windows:**
```powershell
# Incluido con Visual Studio o descargar:
https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb

# Verificar instalación
sqllocaldb info
```

**Opción B: SQL Server Express**
```powershell
# Descargar desde:
https://www.microsoft.com/sql-server/sql-server-downloads
```

**Opción C: Docker (Windows/macOS/Linux)**
```bash
# Ejecutar SQL Server en contenedor
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong@Passw0rd" \
   -p 1433:1433 --name sqlserver \
   -d mcr.microsoft.com/mssql/server:2022-latest

# Verificar que está corriendo
docker ps
```
#### 3. IDE/Editor (Recomendado)

**Opción A: Visual Studio 2022**
- Community/Professional/Enterprise
- Workload: ASP.NET and web development
- [Descargar aquí](https://visualstudio.microsoft.com/downloads/)

**Opción B: Visual Studio Code**
```bash
# Descargar desde: https://code.visualstudio.com/

# Extensiones recomendadas:
- C# Dev Kit (ms-dotnettools.csdevkit)
- C# (ms-dotnettools.csharp)
- NuGet Package Manager
- REST Client o Thunder Client
```
#### 4. Git
```bash
# Windows
winget install Git.Git

# macOS
brew install git

# Linux
sudo apt-get install git

# Verificar
git --version
```

### Software Opcional (Recomendado)

#### Postman o Insomnia
Para probar los endpoints de la API
- [Postman](https://www.postman.com/downloads/)
- [Insomnia](https://insomnia.rest/download)

#### SQL Server Management Studio (SSMS)
Para administrar la base de datos
- [Descargar SSMS](https://learn.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms)
## 🚀 Instalación

### 1. Clonar el Repositorio

```bash
# HTTPS
git clone https://github.com/benru203/api-documentos.git

# SSH
git clone git@github.com:benru203/api-documentos.git

# Entrar al directorio
cd documentos-api
```

### 2. Restaurar Dependencias

```bash
# Restaurar todos los paquetes NuGet
dotnet restore

# O restaurar por proyecto específico
dotnet restore src/Documento/Dominio/Documento.Dominio/Documento.Dominio.csproj
dotnet restore src/Documento/Aplicacion/Documento.Aplicacion/Documento.Aplicacion.csproj
dotnet restore src/Documento/Presentacion/Documento.Api/Documento.Api.csproj
dotnet restore src/Documento/Infraestructura/Documento.Infraestructura/Documento.Infraestructura.csproj
```

### 3. Verificar la Instalación

```bash
# Compilar la solución
dotnet build

# Debería mostrar:
# Build succeeded.
#     0 Warning(s)
#     0 Error(s)
```

---

## ⚙️ Configuración

### 1. Configurar la Cadena de Conexión

Edita el archivo `src/DocumentosAPI.API/appsettings.json`:

#### Para SQL Server LocalDB (Windows):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GestionDocumentos;Trusted_Connection=true;TrustServerCertificate=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

#### Para SQL Server Express (Windows):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=GestionDocumentos;Trusted_Connection=true;TrustServerCertificate=true"
  }
}
```

#### Para SQL Server con Usuario/Contraseña:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=GestionDocumentos;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true"
  }
}
```

#### Para SQL Server en Docker:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=GestionDocumentos;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true"
  }
}
```
## 🎮 Ejecución

### Método 1: Desde la Terminal

```bash
# Navegar al proyecto API
cd src/Documento/Presentacion/Documento.Api


#Ejecutar en modo desarrollo con certificado opcional
dotnet dev-certs https --trust

# Ejecutar en modo desarrollo
dotnet run



# O con watch (recarga automática)
dotnet watch run

# Debería mostrar:
# info: Microsoft.Hosting.Lifetime[14]
#       Now listening on: https://localhost:5001
#       Now listening on: http://localhost:5000
```

### Método 2: Desde Visual Studio

1. Abrir `api-documentos.sln`
2. Establecer `Documento.Api` como proyecto de inicio
3. Presionar `F5` o clic en ▶️ Run

### Método 3: Desde VS Code

1. Abrir la carpeta del proyecto
2. Presionar `F5`
3. Seleccionar ".NET Core Launch (web)"

### Método 4: Docker (Opcional)

```bash
# Construir imagen
docker build -t api-documentos:latest .

# Ejecutar contenedor
docker run -d -p 5000:8080 \
    -e ConnectionStrings__DefaultConnection="Server=host.docker.internal,1433;..." \
    --name api-documentos \
    api-documentos:latest

# Ver logs
docker logs -f api-documentos
```

### Verificar que está funcionando

Abre tu navegador en: **https://localhost:7199/swagger**

Deberías ver la interfaz de Swagger UI con todos los endpoints documentados.

---

## 🧪 Pruebas

### Ejecutar Tests Unitarios

```bash
# Ejecutar todos los tests
dotnet test

# Ejecutar con cobertura
dotnet test src/Documento/Dominio/Documento.Dominio.Test/Documento.Dominio.Test.csproj --collect:"XPlat Code Coverage" -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=html

# Ejecutar tests de un proyecto específico
dotnet test  dotnet test src/Documento/Dominio/Documento.Dominio.Test/Documento.Dominio.Test.csproj

# Ejecutar tests con logs detallados
dotnet test src/Documento/Dominio/Documento.Dominio.Test/Documento.Dominio.Test.csproj --logger "console;verbosity=detailed"

```

### Cobertura Esperada
- ✅ Domain.Tests: >90%
- ✅ Application.Tests: >85%
- ✅ Infrastructure.Tests: >75%

```bash
```
## 📡 Endpoints API

### Base URL
- **Desarrollo:** `https://localhost:7199/api`

### Documentación Swagger
- **Desarrollo:** https://localhost:7199/swagger
- **OpenAPI JSON:** https://localhost:7199/swagger/v1/swagger.json

### Endpoints Disponibles

#### 1. Crear Documento
```http
POST /api/documentos
Content-Type: application/json

{
  "titulo": "Manual de Usuario",
  "autor": "Juan Pérez",
  "tipo": "INFORME",
  "estado": "PENDIENTE"
}
```

**Response 201 Created:**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

#### 2. Listar Documentos (Paginado)
```http
GET /api/documentos?pagina=1&tamano_pagina=10
```

**Response 200 OK:**
```json
{
  "datos": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "titulo": "Manual de Usuario",
      "autor": "Juan Pérez",
      "tipo": "INFORME",
      "estado": "REGISTRADO",
      "fechaRegistro": "2024-10-16T10:30:00Z"
    }
  ],
  "pagina": 1,
  "tamano_pagina": 10,
  "total": 25,
}
```

#### 3. Obtener Documento por ID
```http
GET /api/documentos/{id}
```

**Response 200 OK:** (igual que crear)
**Response 404 Not Found:**
```json
{
  "error": "No se encontró un documento con el Id ..."
}
```

#### 4. Buscar Documentos
```http
GET /api/documentos/buscar?autor=Juan&tipo=INFORME&estado=REGISTRADO
```

**Parámetros opcionales:**
- `autor` - Búsqueda parcial por autor
- `tipo` - Valores: INFORME, CONTRATO, ACTA,
- `estado` - Valores: REGISTRADO, PENDIENTE, VALIDADO, ARCHIVADO

#### 5. Actualizar Documento
```http
PUT /api/documentos/{id}
Content-Type: application/json

{
  "titulo": "Manual de Usuario v2.0",
  "autor": "Juan Pérez",
  "tipo": "Informe",
  "estado": "Validado"
}
```

**Response 204 NoContent:** Documento actualizado
**Response 400 Bad Request:** Error de validación
**Response 404 Not Found:** Documento no existe

#### 6. Eliminar Documento
```http
DELETE /api/documentos/{id}
```

**Response 204 No Content:** Eliminado exitosamente
**Response 404 Not Found:** Documento no existe

### Valores Válidos

#### Tipo de Documento
- `INFORME`
- `CONTRATO`
- `ACTA`

#### Estado de Documento
- `REGISTRADO`
- `PENDIENTE`
- `VALIDADO`
- `ARCHIVADO`

### Códigos de Estado HTTP

| Código | Significado |
|--------|-------------|
| 200 | OK - Solicitud exitosa |
| 201 | Created - Recurso creado |
| 204 | No Content - Eliminado exitosamente |
| 400 | Bad Request - Error de validación |
| 404 | Not Found - Recurso no encontrado |
| 500 | Internal Server Error - Error del servidor |

---

## 📁 Estructura del Proyecto

```
api-documentos/
│
src/
└── Documento/
├── Aplicacion/
│ ├── Documento.Aplicacion/
│ │ ├── Dependencias/
│ │ ├── DTOs/
│ │ ├── Interfaces/
│ │ ├── Servicios/
│ │ └── DependencyContainer.cs
│ └── Documento.Aplicacion.Test/
│ ├── Dependencias/
│ └── AplicacionDocumentoTest.cs
│
├── Dominio/
│ ├── Documento.Dominio/
│ │ ├── Dependencias/
│ │ ├── Entidades/
│ │ ├── Interfaces/
│ │ └── ValueObjects/
│ └── Documento.Dominio.Test/
│ ├── Dependencias/
│ └── DocumentoTest.cs
│
├── Infraestructura/
│ ├── Documento.Infraestructura/
│ │ ├── Dependencias/
│ │ ├── Context/
│ │ ├── Repositorios/
│ │ └── DependencyContainer.cs
│ └── Documento.Infraestructura.Test/
│ ├── Dependencias/
│ └── InfraestructuraTest.cs
│
├── Presentacion/
│ ├── Documento.Api/
│ │ ├── Connected Services/
│ │ ├── Dependencias/
│ │ ├── Properties/
│ │ ├── Controllers/
│ │ ├── .gitignore
│ │ ├── appsettings.json
│ │ ├── Documento.Api.http
│ │ └── Program.cs
│ └── Documento.Api.Test/
│ ├── Dependencias/
│ └── ApiTest.cs
│
└── Documento.IoC/
│  └── DependencyContainer.cs
├── .gitignore
├── api-documentos.sln 
└── README.md

```

---
## 🛠️ Tecnologías

### Framework y Lenguaje
- **.NET Core 8.0** - Framework principal
- **C# 12** - Lenguaje de programación

### ORM y Base de Datos
- **Entity Framework Core 8.0** - ORM
- **SQL Server 2019+**

### Testing
- **xUnit 2.5** - Framework de testing
- **Moq 4.20** - Mocking framework

### Documentación
- **Swashbuckle.AspNetCore 6.6** - Swagger/OpenAPI

---
## 🎯 Principios Aplicados

### Arquitectura Hexagonal (Puertos y Adaptadores)

```
┌─────────────┐
│   Puertos   │ ──► Interfaces (IDocumentoRepository)
└─────────────┘
       │
       ▼
┌─────────────┐
│ Adaptadores │ ──► Implementaciones (DocumentoRepository)
└─────────────┘
```

- **Puertos de entrada:** IDocumentoService
- **Puertos de salida:** IDocumentoRepository
- **Adaptadores:** Controllers, Repositories

### Principios SOLID

#### 1. **S**ingle Responsibility Principle
- `DocumentoService`: Solo gestiona lógica de aplicación
- `DocumentoRepository`: Solo gestiona persistencia
- `Documento`: Solo contiene lógica de dominio

#### 2. **O**pen/Closed Principle
- Extensible a través de interfaces
- Cerrado para modificación directa

#### 3. **L**iskov Substitution Principle
- Las implementaciones son intercambiables
- `DocumentoRepository` puede ser reemplazado por otra implementación

#### 4. **I**nterface Segregation Principle
- Interfaces específicas y cohesivas
- No hay métodos innecesarios

#### 5. **D**ependency Inversion Principle
- Dependencias hacia abstracciones (interfaces)
- Inyección de dependencias con DI Container

### Domain-Driven Design (DDD)

- **Entidades:** Documento con identidad (GUID)
- **Value Objects:** (Titulo,Autor, Tipo, EstadoDocumento)
- **Repositorios:** Acceso a colecciones de entidades

### Test-Driven Development (TDD)

1. ✅ Escribir test (Red)
2. ✅ Implementar código mínimo (Green)
3. ✅ Refactorizar (Refactor)
---
