
# .NET Suscriptor (RabbitMQ a SQL Server)

Este proyecto es un servicio backend creado en .NET que se suscribe a un broker RabbitMQ para recibir mensajes y luego almacenarlos en una base de datos SQL Server.

## Requisitos

- .NET SDK 6.0 o superior
- RabbitMQ en funcionamiento
- SQL Server configurado y accesible
- Un archivo `appsettings.json` configurado para RabbitMQ y SQL Server

## Estructura del Proyecto

- **Controllers**: Contiene los controladores que manejan la lógica de la suscripción y almacenamiento.
- **Mensajes**: Contiene las clases y lógica necesarias para el manejo de los mensajes recibidos de RabbitMQ.
- **Data**: Contiene la lógica para interactuar con la base de datos SQL Server.

## Configuración del Proyecto

1. Clona el repositorio:
    ```bash
    git clone <URL-del-repositorio>
    cd nombre-del-repositorio
    ```

2. Configura los detalles de conexión en el archivo `appsettings.json`:
    ```json
    {
      "RabbitMQ": {
        "Host": "localhost",
        "Port": 5672,
        "Username": "guest",
        "Password": "guest"
      },
      "ConnectionStrings": {
        "DefaultConnection": "Server=<servidor>;Database=<nombre-bdd>;User Id=<usuario>;Password=<contraseña>;"
      }
    }
    ```
    Asegúrate de actualizar los valores según la configuración de tu RabbitMQ y SQL Server.

## Comandos

### Restaurar dependencias

```bash
dotnet restore
```

Este comando restaurará todas las dependencias definidas en los archivos `.csproj`.

### Compilar la aplicación

```bash
dotnet build
```

Compila el proyecto y genera los archivos binarios necesarios.

### Ejecutar la aplicación en desarrollo

```bash
dotnet run --project <nombre-del-proyecto>
```

Este comando ejecuta la aplicación desde la carpeta correspondiente. Los mensajes recibidos de RabbitMQ se almacenarán en la base de datos SQL Server configurada.

### Ejecutar pruebas

Si tienes un proyecto de pruebas, puedes ejecutar los tests con el siguiente comando:

```bash
dotnet test
```

### Publicar para producción

```bash
dotnet publish --configuration Release --output ./publish
```

Este comando genera los archivos necesarios para desplegar la aplicación en un servidor de producción.

## Interacción con RabbitMQ y SQL Server

- La aplicación se suscribe a un broker RabbitMQ para recibir mensajes.
- Estos mensajes se almacenan en una base de datos SQL Server configurada a través de una cadena de conexión.

## Despliegue

Para desplegar la aplicación en un entorno de producción:
1. Ejecuta el comando `dotnet publish` para generar los archivos optimizados.
2. Despliega los archivos en un servidor web compatible o usa Docker si es necesario.
3. Configura RabbitMQ y SQL Server para que estén accesibles desde tu servidor.

## Personalización

Puedes ajustar los tópicos de RabbitMQ o la configuración de la base de datos SQL Server en el archivo `appsettings.json` para adaptarlo a tus necesidades específicas.
