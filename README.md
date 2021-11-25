## Post Service

Service to enable the submission of Articles to GeekIam


### Generating database migrations

In the root of the project directory there is a docker compose file which enables setting a simple PostgreSql database on the local machine.
This database is just used to enable creating migrations and testing them, 



#### Install EF Core tool

```shell
dotnet tool install --global dotnet-ef
```

#### Update Tool

```shell
dotnet tool update --global dotnet-ef
```

### Verify Installation

```shell
dotnet ef
```
