using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Persistence.Providers.SqlServer
{
    public class Client : Persistence.DataBase.AbstractDataBasePersistence
	{
		#region Variables

                private System.Data.SqlClient.SqlConnection   _connection  = null;
                private System.Data.SqlClient.SqlTransaction  _transaction = null;

		#endregion

        #region Conneciton

				public override void openConnection()
				{
                    try
                    {
                        _connection.ConnectionString = _connectionString;
                        _connection.Open();
                    }
                    catch( System.Exception e )
                    {
                        throw new Persistence.Exceptions.ConnectionException( e.Message );
                    }
                }

				public override void closeConnection()
				{
                    try
                    {
                        _connection.Close() ;
                    }
                    catch( System.Exception e )
                    {
                        throw new Persistence.Exceptions.ConnectionException(e.Message);
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
                                SqlCommand  command = _connection.CreateCommand();
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
                                SqlCommand  command = _connection.CreateCommand();
                                            command.CommandText = sqlString;
                                            command.CommandType = System.Data.CommandType.Text;
                                            command.Transaction = (null != _transaction) ? _transaction : null;

                                resultDataSet = new DataSet();

                                SqlDataAdapter  dataAdapter = new SqlDataAdapter( command );
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
                    _connection = new System.Data.SqlClient.SqlConnection();
                }
        #endregion
	}
}
