# ProductManager

ProductManager es una Web desarrollada en .NET para la gestión de Productos, Categorias, Ubicación y Kardex.

## Configuración de la base de datos

Este proyecto está configurado para conectarse a una base de datos SQL Server. Los datos de conexión por defecto son los siguientes:

- **Database:** `productManagerDb`
- **Usuario:** `userManager`
- **Contraseña:** `pr0ductM@n@ger`

Asegúrate de que la base de datos esté configurada y corriendo antes de ejecutar el proyecto.

## Configuración del proyecto

1. Clona este repositorio en tu máquina local:

   ```
   https://github.com/Evaristo360/ProductManager.git
   ```
2. Configura las variables de conexión a la base de datos en el archivo appsettings.json
  ```
  {
    "ConnectionStrings": {
      "DefaultConnection": "Server=SERVER;Database=productManagerDb;UID=userManager;Password=pr0ductM@n@ger;TrustServerCertificate=True;"
    }
  } 
  ```
3. Restaura las dependencias del proyecto:
```
dotnet restore
```
## Migraciones y Base de Datos

El proyecto utiliza Entity Framework para manejar las migraciones de la base de datos, sigue los siguientes pasos para aplicar las migraciones:

1. Genera las migraciones (si aún no lo has hecho):
  1.1. Ejecuta el siguiente comando en la Consola del Adminitrador de Paquetes
  ```
    .\migrations-batch.ps1
  ```

## Ejecución del proyecto
Para ejecutar el proyecto localmente:
Ejecuta el siguiente comando en la Consola del Adminitrador de Paquetes
```
dotnet run
```
