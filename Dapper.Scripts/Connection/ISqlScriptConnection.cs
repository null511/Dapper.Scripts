﻿using System.Data;

namespace Dapper.Scripts.Connection
{
    public interface ISqlScriptConnection : IDbConnection
    {
        IDbConnection ConnectionBase {get;}
        string GetScriptSql(string key, object param = null);
    }
}
