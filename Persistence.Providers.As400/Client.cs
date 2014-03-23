using System;
using System.Collections.Generic;
using System.Data;
using IBM.Data.DB2.iSeries;

namespace Persistence.Providers.As400
{
    public class Client : Persistence.DataBase.AbstractDataBasePersistence
	{
		#region Variables

                private IBM.Data.DB2.iSeries.iDB2Connection     _connection     = null;
                private IBM.Data.DB2.iSeries.iDB2Transaction    _transaction    = null;

        #endregion

        #region Conneciton

                public override void setParameters( String dbName, String dbUser, String dbPassword, List<String> additionalParameters )
                {
                    _connectionString = String.Format
                    (
                        "DataSource={0};" +
                        "UserID={1};" +
                        "Password={2};" +
                        "DataCompression=True;" +
                        "LibraryList={3};",

                        dbName,
                        dbUser,
                        dbPassword,
                        additionalParameters[0]
                    );
                }

				public override void openConnection()
				{
                    try
                    {
                        _connection.ConnectionString = _connectionString;
                        _connection.Open();
                    }
                    catch( IBM.Data.DB2.iSeries.iDB2ConnectionFailedException ex )
                    {
                        throw new Persistence.Exceptions.ConnectionException( ex.ToString() );
                    }
                    catch( IBM.Data.DB2.iSeries.iDB2ConnectionTimeoutException ex )
                    {
                        throw new Persistence.Exceptions.ConnectionException( ex.ToString() );
                    }
                }

				public override void closeConnection()
				{
                    try
                    {
                        _connection.Close();
                    }
                    catch( System.Exception ex )
                    {
                        throw new Persistence.Exceptions.ConnectionException( ex.ToString() );
                    }
                }

		#endregion

		#region Transaction
				public override void beginTransaction()
				{
			        try 
			        {
				        _transaction = _connection.BeginTransaction();
			        } 
			        catch( System.Exception e ) 
			        {
                        throw new Persistence.Exceptions.TransactionException(e.Message);
			        }
                }

				public override void endTransaction()
				{
			        try 
			        {
                        if( null != _transaction )
				            _transaction.Commit();

                        _transaction = null;
			        } 
			        catch( System.Exception e ) 
			        {
                        throw new Persistence.Exceptions.TransactionException(e.Message);
			        }
                }

				public override void rollBack()
				{
			        try 
			        {
                        if( null != _transaction )
                            _transaction.Rollback();
			        }
                    catch( System.Exception e ) 
                    {
                        throw new Persistence.Exceptions.TransactionException(e.Message);
                    }
                }
		#endregion

        #region Dynamic SQL Builder for speciffic format - TrasnsactSQL / PL-SQL / etc...
                public override void createDynamicSqlBuilder()
                {
                    _dynamicSqlBuilder = (DataBase.IDynamicSqlBuilder) new DynamicSqlBuilder();
                }
        #endregion

        #region Executes stored procedures / functions / dynamic SQl

                #region executeDynamicSql( sqlString )

                        public override void executeDynamicSql( String sqlString )
                        {
                            try
                            {
                                iDB2Command command = _connection.CreateCommand();
                                            command.CommandText = sqlString;
                                            command.CommandType = System.Data.CommandType.Text;
                                            command.Transaction = (null != _transaction) ? _transaction : null;

                                            command.ExecuteNonQuery();
                            }
                            catch( System.Exception e )
                            {
                                throw new Persistence.Exceptions.DynamicSqlException( e.Message );
                            }
                        }

                #endregion

                #region executeDynamicSql( sqlString, resultDataSet )

                        public override void executeDynamicSqlWithReturnValue( String sqlString, ref DataSet resultDataSet )
                        {
                            try
                            {
                                iDB2Command command = _connection.CreateCommand();
                                            command.CommandText = sqlString;
                                            command.CommandType = System.Data.CommandType.Text;
                                            command.Transaction = (null != _transaction) ? _transaction : null;

                                resultDataSet = new DataSet();

                                iDB2DataAdapter dataAdapter = new iDB2DataAdapter( command );
                                                dataAdapter.Fill( resultDataSet );
                            }
                            catch( System.Exception e )
                            {
                                throw new Persistence.Exceptions.DynamicSqlException( e.Message );
                            }
                        }

                #endregion

        #endregion

        #region Additional Helper Methods Speciffic To The Class
        public Client()
        {
            _connection = new IBM.Data.DB2.iSeries.iDB2Connection();
        }
        #endregion
	}
}
