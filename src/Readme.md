# Microservice Architecture.

## EF Core

### EF command
	- Add-Migration -o ./Persistence/Migrations/ <Name>
	- Add-Migration -o ./Persistence/Migrations/ --provider Sqlite
		- create migration
	
```
Options:
  -o|--output-dir <PATH>           The directory to put files in. Paths are relative to the project directory. Defaults to "Migrations".
  --json                           Show JSON output. Use with --prefix-output to parse programatically.
  -n|--namespace <NAMESPACE>       The namespace to use. Matches the directory by default.
  -c|--context <DBCONTEXT>         The DbContext to use.
  -a|--assembly <PATH>             The assembly to use. Required.
  --project <PATH>                 The path to the startup project file.
  -s|--startup-assembly <PATH>     The startup assembly to use. Defaults to the target assembly.
  --startup-project <PATH>         The path to the project file.
  --data-dir <PATH>                The data directory.
  --project-dir <PATH>             The project directory. Defaults to the current working directory.
  --root-namespace <NAMESPACE>     The root namespace. Defaults to the target assembly name.
  --language <LANGUAGE>            The language. Defaults to 'C#'.
  --nullable                       Enable nullable reference types.
  --working-dir <PATH>             The working directory of the tool invoking this command.
  --framework <FRAMEWORK>          The target framework.
  --configuration <CONFIGURATION>  The configuration to use.
  -h|--help                        Show help information
  -v|--verbose                     Show verbose output.
  --no-color                       Don't colorize output.
  --prefix-output                  Prefix output with level.

```





	- Update-Database
		- apply migration
	- Script-Migration
		- if we want to make a SQL script of all our migrations, 


## Problem Deatils

### Pkgs
	- Install-Package Hellang.Middleware.ProblemDetails

# API Should Implement.

	- Handle injections (javascript, sql, )