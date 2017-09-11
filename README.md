# Dapper Scripts
Extension of Dapper which supports binding a collection of SQL scripts to a `SqlConnection`.

Avoid embedding SQL text strings into your code, and leverage syntax hightlighting, by storing your SQL operations in external *.sql files. Dapper scripts binds a collection of key-based sql strings to a connection factory, allowing you to keep your database code clean. All extensions match the original dapper commands, but using a _*Script_ suffix.

Setup
```c#
static class Sql
{
    public static SqlScriptConnectionFactory SampleDatabase {get;}
    
    static Sql()
    {
        var scripts = new SqlScriptCollection();
        scripts.Add.FromFile("test-query", "query.sql");

        DatabaseName = new SqlScriptConnectionFactory(scripts);
        DatabaseName.ConnectionString = "Server: ...;";
    }
}
```

Usage
```c#
using (var connection = Sql.SampleDatabase.Connect()) {
    return connection.QueryScript("test-query", new {arg = value});
}
```

For more examples, visit the [WiKi](https://github.com/null511/Dapper.Scripts/wiki).
