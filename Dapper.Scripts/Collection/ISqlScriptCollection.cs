namespace Dapper.Scripts.Collection
{
    public interface ISqlScriptCollection
    {
        string GetScriptSql(string key, object param = null);
    }
}
