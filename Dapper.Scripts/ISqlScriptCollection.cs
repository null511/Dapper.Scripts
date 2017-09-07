namespace Dapper.Scripts
{
    public interface ISqlScriptCollection
    {
        string GetScriptSql(string key, object param = null);
    }
}
