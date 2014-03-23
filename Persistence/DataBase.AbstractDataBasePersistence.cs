using System;
using System.Collections.Generic;
using System.Data;

namespace Persistence.DataBase
{
    public abstract class AbstractDataBasePersistence : IDataBasePersistence
    {
        #region Variables

                protected static IDynamicSqlBuilder _dynamicSqlBuilder  = null;
                protected        String             _connectionString   = String.Empty;

        #endregion

        #region Connection

                public virtual void setParameters( String dbName, String dbUser, String dbPassword )
                {}

                public virtual void setParameters( String dbName, String dbUser, String dbPassword, List<String> additionalParameters )
                {}

                public virtual void setParameters( String connectionString )
                {
                    _connectionString = connectionString;
                }

                public abstract void openConnection();
                public abstract void closeConnection();

        #endregion

        #region Transaction

                public abstract void beginTransaction();
                public abstract void endTransaction();
                public abstract void rollBack();

        #endregion

        #region Dynamic SQL Builder for speciffic format - TrasnsactSQL / PL-SQL / etc...

                public IDynamicSqlBuilder getDymanicSqlBuilder()
                {
                    if( null == _dynamicSqlBuilder )
                        createDynamicSqlBuilder();

                    return _dynamicSqlBuilder;
                }

                public abstract void createDynamicSqlBuilder();

        #endregion

        #region Executes stored procedures / functions / dynamic SQl

                public abstract void executeDynamicSql( String sqlString );

                public abstract void executeDynamicSqlWithReturnValue( String sqlString, ref DataSet resultDataSet );

                #region executeDynamicSqlWithReturnValue( sqlString, resultList )

                        public void executeDynamicSqlWithReturnValue( String sqlString, ref List<String> resultList )
                        {
                            try
                            {
                                DataSet resultDataSet = null;
                                executeDynamicSqlWithReturnValue( sqlString, ref resultDataSet );

                                resultList = new List<String>();
                                foreach( DataRow dataRow in resultDataSet.Tables[ 0 ].Rows )
                                {
                                    resultList.Add( dataRow[ 0 ].ToString().Trim() );
                                }
                            }
                            catch( System.Exception e )
                            {
                                throw new Persistence.Exceptions.DynamicSqlException( e.Message );
                            }
                        }

                #endregion

                #region executeDynamicSqlWithReturnValue( sqlString, result )

                        public void executeDynamicSqlWithReturnValue( String sqlString, ref String result )
                        {
                            try
                            {
                                List<String> resultList = null;
                                executeDynamicSqlWithReturnValue( sqlString, ref resultList );

                                result = ( resultList.Count > 0 ) ? resultList[ 0 ] : String.Empty;
                            }
                            catch( System.Exception e )
                            {
                                throw new Persistence.Exceptions.DynamicSqlException( e.Message );
                            }
                        }
                        
                #endregion

                #region executeDynamicSqlWithReturnValue( sqlString, resultTableName, resultColumn, resultList )

                        public void executeDynamicSqlWithReturnValue( String sqlString, String resultTableName, String resultColumn, ref List<String> resultList )
                        {
                            try
                            {
                                // execute the SQL that will fill a result table
                                executeDynamicSql( sqlString );

                                // extract data from the result table
                                String selectCommand = String.Format( "select {0} from {1}", resultColumn, resultTableName );
                                DataSet resultDataSet = null;

                                executeDynamicSqlWithReturnValue( selectCommand, ref resultDataSet );

                                // copy the result
                                resultList = new List<String>();

                                foreach( DataRow dataRow in resultDataSet.Tables[ 0 ].Rows )
                                {
                                    resultList.Add( dataRow[ resultColumn ].ToString().Trim() );
                                }
                            }
                            catch( System.Exception e )
                            {
                                throw new Persistence.Exceptions.DynamicSqlException( e.Message );
                            }
                        }

                #endregion

                #region executeDynamicSqlWithReturnValue( sqlString, resultTableName, resultColumn, result )

                        public void executeDynamicSqlWithReturnValue( String sqlString, String resultTableName, String resultColumn, ref String result )
                        {
                            try
                            {
                                List<String> resultList = null;
                                executeDynamicSqlWithReturnValue( sqlString, resultTableName, resultColumn, ref resultList );

                                result = (resultList.Count > 0) ? resultList[ 0 ] : String.Empty;
                            }
                            catch( System.Exception e )
                            {
                                throw new Persistence.Exceptions.DynamicSqlException( e.Message );
                            }
                        }

                #endregion

                #region executeDynamicSqlWithReturnValue( sqlString, resultTableName, resultColumn, whereColumn, whereValue, resultList )

                        public void executeDynamicSqlWithReturnValue( String sqlString, String resultTableName, String resultColumn, String whereColumn, String whereValue, ref List<String> resultList )
                        {
                            try
                            {
                                // execute the SQL that will fill a result table
                                executeDynamicSql( sqlString );

                                // extract data from the result table
                                String selectCommand = String.Format( "select {0} from {1} where {2} = '{3}'", resultColumn, resultTableName, whereColumn, whereValue );
                                DataSet resultDataSet = null;

                                executeDynamicSqlWithReturnValue( selectCommand, ref resultDataSet );

                                // copy the result
                                resultList = new List<String>();

                                foreach( DataRow dataRow in resultDataSet.Tables[ 0 ].Rows )
                                {
                                    resultList.Add( dataRow[ resultColumn ].ToString().Trim() );
                                }
                            }
                            catch( System.Exception e )
                            {
                                throw new Persistence.Exceptions.DynamicSqlException( e.Message );
                            }
                        }

                #endregion

                #region executeDynamicSqlWithReturnValue( sqlString, resultTableName, resultColumn, whereColumn, whereValue, result )

                        public void executeDynamicSqlWithReturnValue( String sqlString, String resultTableName, String resultColumn, String whereColumn, String whereValue, ref String result )
                        {
                            try
                            {
                                List<String> resultList = null;
                                executeDynamicSqlWithReturnValue( sqlString, resultTableName, resultColumn, whereColumn, whereValue, ref resultList );

                                result = (resultList.Count > 0) ? resultList[ 0 ] : String.Empty;
                            }
                            catch( System.Exception e )
                            {
                                throw new Persistence.Exceptions.DynamicSqlException( e.Message );
                            }
                        }

                #endregion

        #endregion
    }
}
