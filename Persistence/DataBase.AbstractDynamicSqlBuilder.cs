using System;
using System.Collections;
using System.Collections.Generic;

namespace Persistence.DataBase
{
    public abstract class AbstractDynamicSqlBuilder : IDynamicSqlBuilder
    {
        public abstract string buildInsertQuery( String table, TraceEntities.TraceEntityCollection fields, String[] values );
        public abstract string buildUpdateQuery( String table, TraceEntities.TraceEntityCollection fields, String[] values );

        public abstract string buildExecProcQuery( String procedureName, ref List<Object> parameters );

        // Converti la chaîne récupérée du csv dans le type et le formatage attendu par oracle.
        public abstract string getConvertedValue( string val, int destinationTypeAsInt, int maxLength );
    }
}
