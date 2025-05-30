# Aplicacion_reseñas_de_libros

API.ReseñasLibros/
├── API.BackEnd/             # Proyecto .NET 8 Core
│   ├── Controllers/         # Endpoints (Auth, Libros, Resenas, Usuarios…)
|   ├── DTOS/                # DTOs para transferir los datos al frontend
│   ├── Services/            # Lógica de negocio
│   ├── Profiles/            # Mappeos AutoMapper
│   ├── Interfaces/          # Contratos de servicio
│   ├── Models/              # JwtSettings
│   ├── appsettings.json     # Configuración de DB y JWT
│   └── Program.cs           # Registro de middlewares y servicios
└── MODELOS.Shared/          # DbContext y entidades EF Core

#Requisitos previos
.NET 8 SDK
SQL Server
Cuenta de Azure y App Service creado

#Ejecutar localmente

Clonar el repositorio y situarse en la carpeta del API:

git clone <URL-del-repo-API>
cd API.ReseñasLibros/API.BackEnd


Ajustar la cadena de conexión en appsettings.json :

"ConnectionStrings": {
  "LocalConnection": CADENA DE CONECCION PUEDE SER ALGO COMO ESTO: "Server=Username;Database=ReseniasLibros;Trusted_Connection=True;TrustServerCertificate=True"
}

Configurar los valores JWT en appsettings.json:

"Jwt": {
  "Key": "TuClaveMuySeguraYSecreta",
  "Issuer": "ReseniasLibrosAPI",
  "Audience": "ReseniasLibrosClient",
  "ExpirationMinutes": 60
}


Restaurar y compilar:

dotnet restore
dotnet build
(Si usas Code-First) Aplicar migraciones:
dotnet ef database update

Ejecutar:

dotnet run
Abrir Swagger en https://localhost:5001/swagger para probar los endpoints.

#Despliegue en Azure App Service

En el Portal de Azure, crea un App Service para .NET 8 (Windows).

En Visual Studio Community:

Clic derecho sobre API.BackEnd → Publish…

Elegir Azure App Service (Windows) → seleccionar tu recurso.

Publicar.

En el App Service → Configuration:

Añade tu Connection String (LocalConnection).

Bajo Application settings, define las variables Jwt:Key, Jwt:Issuer, Jwt:Audience, Jwt:ExpirationMinutes.

Tras publicar, tu API estará en:

https://<tu-app-service>.azurewebsites.net/api
