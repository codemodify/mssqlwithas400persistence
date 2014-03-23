using System;
using System.Collections.Generic;
using System.Data;

namespace Persistence.DataBase
{
    public interface IDataBasePersistence : Persistence.IPersistence
    {
        #region Connection

                void setParameters( String dbName, String dbUser, String dbPassword );
                void setParameters( String dbName, String dbUser, String dbPassword, List<String> additionalParameters );
                void setParameters( String connectionString );

        #endregion

        #region Dynamic SQL Builder for speciffic format - TrasnsactSQL / PL-SQL / etc...

                IDynamicSqlBuilder  getDymanicSqlBuilder();
                void                createDynamicSqlBuilder();

        #endregion

        #region Executes stored procedures / functions / dynamic SQl

                void executeDynamicSql( String sqlString );

                void executeDynamicSqlWithReturnValue( String sqlString, ref DataSet resultDataSet   );
        
                void executeDynamicSqlWithReturnValue( String sqlString, ref List<String> resultList );
                void executeDynamicSqlWithReturnValue( String sqlString, ref      String  result     );

                void executeDynamicSqlWithReturnValue( String sqlString, String resultTableName, String resultColumn, ref List<String> resultList );
                void executeDynamicSqlWithReturnValue( String sqlString, String resultTableName, String resultColumn, ref      String  result     );

                void executeDynamicSqlWithReturnValue( String sqlString, String resultTableName, String resultColumn, String whereColumn, String whereValue, ref List<String> resultList );
                void executeDynamicSqlWithReturnValue( String sqlString, String resultTableName, String resultColumn, String whereColumn, String whereValue, ref      String  result     );

        #endregion
    }
}
