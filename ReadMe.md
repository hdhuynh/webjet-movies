# Webjet Movies


### Setup

Follow these steps to get your development environment set up:

1. Clone the repository

3. Build the solution with Visual Studio, or use command line below:

```bash
dotnet build
```

4. Install SQL database with docker
```powershell
docker pull mcr.microsoft.com/mssql/server:2022-latest
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=password#23" -p 1433:1433 --name sql1 --hostname sql1 -d  mcr.microsoft.com/mssql/server:2022-latest
```

5. Setup the database by running:
```powershell
webjet-movies\Src> .\.scripts\db-dev.ps1
```

6. Start and initialize Azure Storage Emulator (to run WebJob project locally in development) (https://learn.microsoft.com/en-us/azure/storage/common/storage-use-emulator#:~:text=in%20this%20article.-,Start%20and%20initialize%20the%20Storage%20Emulator,-To%20start%20the)

7. In Visual Studio, set BackgroundJob as startup project. To run both WebUI and BackgroundJob, set multiple startup projects. BackgroundJob will be run as console window

