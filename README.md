# FootballPlayers

This is a simple REST API that implement CRUD operations on an list of football players.
It uses .Net 6, Dapper, and FluentValidation and is developed using Visual Studio 2022. Also, SQLite is used for data storage.

The solution structure is mainly based on [Clean Architecture by Json Taylor ](https://jasontaylor.dev/clean-architecture-getting-started/) with Application and Domain combined together.

To run the application, you can use Visual Studio 2022. All you need to do is to set "FootballPlayers.Api" project as the startup project and hit F5.
A fresh database file (database.db) is included in the project but in case of needing to create a new one again, there is a script file (DatabaseInitializer.sql) inside the solution folder.
