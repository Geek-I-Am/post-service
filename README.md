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

### Integration Tests
The integration tests can be run either in Rider using the Http End point tests 
or using [HttpYac](https://httpyac.github.io/guide/installation_cli.html#intallation) and the terminal using the command

This command will provide a detail JSON output
```shell
httpyac ./tests/Integration.Tests/Tests/*  --all  -e dev --json
```

This command will provide a nice short summary useful for quick tests
```shell
httpyac ./tests/Integration.Tests/Tests/*  --all  -e dev -o short
```

N.B. **Ensure the project is started and you are running the Docker-compose which contains the database**