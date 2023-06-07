## Device Manager

This monolithic project has two main components: a .Net application that runs as a backend and manages messaging, and an included React SPA to test some of the functionality.

## Stack
* .NET 6
* ReactJS
* MS SQL
* [RabbitMQ](https://www.rabbitmq.com/): Widely used message broker to convert server requests into mqtp messages that the backend is also listening in.
* [SignalR](https://dotnet.microsoft.com/en-us/apps/aspnet/signalr): Handles server-to-client data flow, replacing the need for clients to keep polling the server.

## Prerequisites for Running the App Locally

* .NET 6 [Install](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* Install Docker and Docker Desktop [Install for Windows](https://docs.docker.com/desktop/install/windows-install/)
* Docker images for RabbitMQ and SQL Server
* NuGet Packages:
    * `Microsoft.EntityFrameworkCore`
    * `Microsoft.EntityFrameworkCore.Tools`
    * `Microsoft.EntityFrameworkCore.Design`
    * `Microsoft.EntityFrameworkCore.SqlServer`
    * `Microsoft.AspNetCore.SpaProxy`
    * `RabbitMQ.Client`
    * `Microsoft.AspNetCore.SignalR.Common`
    * `Swashbuckle.AspNetCore`

## Starting the App

* Download the Docker images for [RabbitMQ](https://hub.docker.com/_/rabbitmq) (or `docker pull rabbitmq`) and [MSSQL Server](https://hub.docker.com/_/microsoft-mssql-server) (or `docker pull mcr.microsoft.com/mssql/server`)
* Clone the repository
* Navigate to `DeviceManager/ClientApp`
* Run `npm install`
* Create DB: In the CLI or Package Manager Console, run `npm run db:create` to create an SQL Server container.
* Create RabbitMQ Server: In the CLI or Package Manager Console, run `npm run rabbit:create` to create a RabbitMQ messaging server.
* Run `Update-Database` in Package Manager Console to apply database migrations.
* Due to the smaller scope of the application, environment variables are stored in the `appsettings.json` file. 
* Run the application, SPA component should start in the browser automatically.