using System;
using System.Collections;
using System.Collections.Generic;

namespace Persistence.DataBase
{
    public interface IDynamicSqlBuilder
    {
        string buildInsertQuery( String table, TraceEntities.TraceEntityCollection fields, String[] values );
        string buildUpdateQuery( String table, TraceEntities.TraceEntityCollection fields, String[] values );

        string buildExecProcQuery( String procedureName, ref List<Object> parameters );

        // Converti la chaîne récupérée du csv dans le type et le formatage attendu par oracle.
        string getConvertedValue( string val, int destinationTypeAsInt, int maxLength );
    }
}
