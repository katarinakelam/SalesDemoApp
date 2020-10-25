# SalesDemoApp
Demo API, dockerized

This demo application shows possibilities of a dockerized API. The API is connected to the SQL Server Express database, that is also in a container. 

Initializing the database container:
  1. Download the server image using:  `docker pull microsoft/mssql-server-windows-express`
  2. Start a mssql server instance: `docker run -d -p 1433:1433 -e sa_password=Panda!123 -e ACCEPT_EULA=Y microsoft/mssql-server-windows-express`
  3. Find the container id using: `docker ps -a` -> Find the mssqlserver instance, and copy the <container-id>
  4. Setup the instance and open the command line prompt: `docker exec -it <container-id> "cmd"`
  5. Connect to the server: `sqlcmd -S localhost -U SA -P  Panda!123`
  5. Create the database:
  * `create database DemoSalesAppDB; go;`
  6. Find the container external IP address: `docker inspect <container-name>`
  7. Use the container's external IP address in the connection string inside the appsettings.json file to connect to the database.
  8. If you wish to start the API locally and connect to the local database, use the "SalesLocalContext" inside the appsettings.json file.
