# Trabajo Práctico XML

Este proyecto es una aplicación de consola desarrollada en C# para importar, validar y mostrar datos desde archivos XML, utilizando una base de datos y contenedores Docker para su despliegue.

## Características

- Importación y validación de archivos XML.
- Limpieza y procesamiento de datos.
- Persistencia en base de datos.
- Pruebas unitarias con xUnit.
- Preparado para ejecutarse en contenedores Docker.

## Estructura del proyecto

- **Models/**: Modelos de datos.
- **Utils/**: Utilidades y helpers.
- **Services/**: Lógica de negocio y acceso a datos.
- **Helpers/**: Validaciones y funciones auxiliares.
- **Tests/**: Pruebas unitarias.

## Requisitos previos

- Tener instalado [.NET 6.0 SDK o superior](https://dotnet.microsoft.com/download)
- Tener instalado [PostgreSQL](https://www.postgresql.org/download/)
- Crear una base de datos vacía llamada `DEV_SYSACAD` en PostgreSQL, con usuario `admin`, contraseña `admin` y puerto `5433`.

### Ejemplo de comandos para crear la base de datos en PostgreSQL

1. Iniciar sesión en PostgreSQL:
   ```sh
   psql -U postgres
   ```
2. Crear el usuario y la base de datos:
   ```sql
   CREATE USER admin WITH PASSWORD 'admin';
   CREATE DATABASE "DEV_SYSACAD" OWNER admin;
   GRANT ALL PRIVILEGES ON DATABASE "DEV_SYSACAD" TO admin;
   ```
3. (Opcional) Cambiar el puerto de PostgreSQL a 5433 en el archivo `postgresql.conf` si es necesario.

## Ejecución del programa

1. Clonar el repositorio y entrar al directorio:
   ```sh
   git clone <https://github.com/sol-parada/Trabajo-Practico-Xml>
   cd TP_XML
   ```

2. Restaurar dependencias:
   ```sh
   dotnet restore
   ```

3. Ejecutar el programa:
   ```sh
   dotnet run --project TP_XML
   ```

El programa creará automáticamente las tablas necesarias en la base de datos y realizará la importación de los datos desde los archivos XML.

## Notas

- Si el usuario, contraseña, base de datos o puerto son diferentes, edita la cadena de conexión en [`DatabaseSeeder.cs`](TP_XML/Services/DatabaseSeeder.cs).

## Pruebas

Para ejecutar las pruebas unitarias:
```sh
dotnet test
```



> _Trabajo Práctico para la materia Desarrollo de Software - 3er año_
