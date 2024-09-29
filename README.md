
# .NET Suscriptor (RabbitMQ a SQL Server)

Este proyecto es un servicio backend creado en .NET que se suscribe a un broker RabbitMQ para recibir mensajes y luego almacenarlos en una base de datos SQL Server.

## Requisitos

- .NET SDK 6.0 o superior
- RabbitMQ en funcionamiento
- SQL Server configurado y accesible
- Un archivo `appsettings.json` configurado para RabbitMQ y SQL Server

## Comandos

### Ejecutar la aplicación con Docker Compose

Para ejecutar la aplicación, utiliza el siguiente comando:

```bash
docker-compose up
```

Este comando levantará los servicios necesarios definidos en el archivo `docker-compose.yml` para ejecutar la aplicación.
