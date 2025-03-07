# Entity Framework with C#

- [Guide ADO](#guide-to-using-ado)
- [Guide Entity Framework](#guide-to-using-entity-framework)
- [Guide Dapper](#guide-to-using-dapper)
- [Organization of Folders](#organization-of-folders)

## Guide to Using ADO

### Step 1: Connect to the Database
Using Npgsql to connect to PostgreSQL database

### Step 2: Create a Command
Create a command to execute SQL queries
```using (NpgsqlConnection con = new NpgsqlConnection(conexao))```

### Step 3: Execute the Command in a reader
Execute the command and read the results with a reader
```
try
    {
        con.Open();
        Console.WriteLine($"Conexão aberta com Postgres!");

        // Imprimindo o que está dentro do banco
        string query = "SELECT * FROM \"Users\"";

        // NpgsqlCommand representa o comando SQL que será executado no banco
        using(NpgsqlCommand cmd = new NpgsqlCommand(query, con))
        {
            // Lendo e imprimindo os dados
            using(NpgsqlDataReader reader = cmd.ExecuteReader())
            {   // Representa o leitor de dados que irá ler os dados do banco
                while (reader.Read()) // Enquanto há dados para serem lidos
                {
                    Console.WriteLine($"Tabelas do banco de dados: ");
                    // Imprime o nome da tabela, ou valor ca doluna 0
                    // Console.WriteLine(reader.GetString(0));
                    Console.WriteLine($"ID: {reader["Id"]}, Nome: {reader["Nome"]}, Password: {reader["Password"]}");                  
                }
            }
        }
    }
catch (NpgsqlException ex)
    {
        Console.WriteLine($"ERROR: {ex.Message}");
    }
```


## Guide to Using Entity Framework

### Step 1: Install Entity Framework
To use Entity Framework with C#, you need to install the Entity Framework NuGet package.

### Step 2: Create a Database Context
Create a new class that inherits from DbContext. This class will be used to interact with the database.

### Step 3: Define the Database Model
Define the database model using Entity Framework's fluent API. This will map the C# classes to the database tables.

### Step 4: Make the migrations
Run the following command in the Package Manager Console to make the migrations:

```sh
ef migrations add InitialCreate
```

### Step 5: Apply the migrations
Run the following command in the Package Manager Console to apply the migrations:

```sh
ef database update
```

## Guide to use Dapper

### Step 1: Install Dapper

## Using appsetting.json

    Microsoft.EntityFrameworkCore
    Microsoft.EntityFrameworkCore.Design
    Microsoft.Extensions.Configuration
    Microsoft.Extensions.Configuration.Binder
    Microsoft.Extensions.Configuration.FileExtensions
    Microsoft.Extensions.Configuration.Json
    Npgsql.EntityFrameworkCore.PostgreSQL

## Organization of Folders

- ADO: Example of how to use ADO.NET to connect to a database
- EntityFramework: Example of how to use Entity Framework to connect to a database
- Dapper: Example of how to use Dapper to connect to a database
- WindowForms: Example of how to use Windows Forms

