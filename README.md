# SalesDemoApp
Demo API, dockerized

This demo application shows possibilities of a dockerized API. The API is connected to the SQL Server Express database, that is also in a container. 

Initializing the database container:
  1. Download the server image using:  `docker pull microsoft/mssql-server-windows-express`
  2. Start a mssql server instance: `docker run -d -p 1433:1433 -e sa_password=Panda!123 -e ACCEPT_EULA=Y microsoft/mssql-server-windows-express`
  3. Find the container id using: `docker ps -a` -> Find the mssqlserver instance, and copy the <container-id>
  4. Setup the instance and open the command line prompt: `docker exec -it <container-id> "cmd"`
  5. Connect to the server: `sqlcmd -S localhost -U SA -P  Panda!123`
  5. Create and seed the database:
  * `create database DemoSalesAppDB; go;`
  * `USE [DemoSalesAppDB]; SET ANSI_NULLS ON; SET QUOTED_IDENTIFIER ON; CREATE TABLE [dbo].[Articles](	[ArticleNumber] [nvarchar](32) NOT NULL,	[Name] [nvarchar](max) NULL, CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED (	[ArticleNumber] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] ; INSERT INTO Articles VALUES ('d1f2-b26e-407d-977c-cb5f', 'Banana');INSERT INTO Articles VALUES ('64fe-7a3b-43ef-95b1-4126', 'Apple');INSERT INTO Articles VALUES ('bdb3-bb72-460c-9151-cde6', 'Pear');INSERT INTO Articles VALUES ('4c9c-7d26-4d3f-88f2-0f64', 'Mango'); go;`
    * `USE [DemoSalesAppDB]; SET ANSI_NULLS ON;SET QUOTED_IDENTIFIER ON;create sequence [dbo].[SaleEvent_seq] start with 1 increment by 1; CREATE TABLE [dbo].[SaleEvents](	[Id] [int] NOT NULL,	[TimeStamp] [datetime2](7) NOT NULL,	[ArticleSoldNumber] [nvarchar](32) NULL,	[ArticleSoldPrice] [float] NOT NULL, CONSTRAINT [PK_SaleEvents] PRIMARY KEY CLUSTERED (	[Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]; ALTER TABLE [dbo].[SaleEvents] ADD  DEFAULT (NEXT VALUE FOR [dbo].[SaleEvent_seq]) FOR [Id]; ALTER TABLE [dbo].[SaleEvents] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [ArticleSoldPrice];ALTER TABLE [dbo].[SaleEvents]  WITH CHECK ADD  CONSTRAINT [FK_SaleEvents_Articles_ArticleSoldNumber] FOREIGN KEY([ArticleSoldNumber]) REFERENCES [dbo].[Articles] ([ArticleNumber]); ALTER TABLE [dbo].[SaleEvents] CHECK CONSTRAINT [FK_SaleEvents_Articles_ArticleSoldNumber]; GO;`
  6. Find the container external IP address: `docker inspect <container-name>`
  7. Use the container's external IP address in the connection string inside the appsettings.json file to connect to the database.
  8. If you wish to start the API locally, use the "SalesLocalContext" inside the appsettings.json file.
