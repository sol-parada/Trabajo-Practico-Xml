# Trabajo Práctico XML

Este proyecto es una aplicación de consola desarrollada en C# para importar, validar y mostrar datos desde archivos XML, utilizando una base de datos y contenedores Docker para su despliegue.

## Características

- Importación y validación de archivos XML.
- Limpieza y procesamiento de datos.
- Persistencia en base de datos.
- Pruebas unitarias con xUnit.
- Preparado para ejecutarse en contenedores Docker.

## Requisitos

- [.NET 6.0 SDK o superior](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (opcional, para despliegue en contenedores)
- SQL Server o base de datos compatible

## Estructura del proyecto

- **Models/**: Modelos de datos.
- **Utils/**: Utilidades y helpers.
- **Services/**: Lógica de negocio y acceso a datos.
- **Helpers/**: Validaciones y funciones auxiliares.
- **Tests/**: Pruebas unitarias.

## Requisitos

- Tener instalado [.NET 6.0 SDK o superior](https://dotnet.microsoft.com/download)
- Tener PostgreSQL instalado (o acceso a una base de datos compatible)
- Los archivos XML requeridos deben estar en la carpeta `Archivos/`

## Pasos para ejecutar la aplicación

1. Clonar el repositorio y entrar al directorio del proyecto:
   ```sh
   git clone <https://github.com/sol-parada/Trabajo-Practico-Xml>
   cd TP_XML
   ```

2. Restaurar los paquetes:
   ```sh
   dotnet restore
   ```

3. Compilar y ejecutar la aplicación:
   ```sh
   dotnet run --project TP_XML
   ```

4. Seguir las instrucciones que aparecen en la consola.

## Pruebas

Para ejecutar las pruebas unitarias:
```sh
dotnet test
```

## Licencia

Este proyecto se distribuye bajo la licencia MIT.

---

> _Trabajo Práctico para la materia Desarrollo de Software - 3er año_
