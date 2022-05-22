# RoomWars

## Profiles

There is one single profile named WebApi, with ports 5001/5000

## DataBase

The application uses a PostgreSQL database. During development, version 12 was used on the standard port 5432

Links:
* https://www.postgresql.org/download/
* https://hub.docker.com/_/postgres

Command to run migrations:
```shell
dotnet-ef database update --startup-project WebApi --project Infrastructure --context RoomWarsContext -- --environment Development
```

## Project structure
A hexagonal architecture was chosen for the development. 

List of projects:
 * Domain
 * Application
 * Infrastructure
 * WebApi
 * Tests
   * UnitTests
 * ClientConsoleService

## Launch

To check the functionality of the application, you need to launch a Web Api profile, create users and the first game room. 

After that, using the Client Console Service project, connect to the desired room by 2 non-host users