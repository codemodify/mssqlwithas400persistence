using System;
using System.Collections.Generic;
using IBM.Data.DB2.iSeries;

namespace Persistence.Tester
{
    partial class Program
    {
        static void Test()
        {
            try
            {
                String result = null;

                List<String>    additioalParameters = new List<String>();
                                additioalParameters.Add( "QTEMP,E_PGM,E_FIC" );

                Persistence.DataBase.IDataBasePersistence client = null;

                client = new Persistence.Providers.As400.Client();
                client.setParameters( "ETUDES", "SYSCAAP", "SYSCAAP", additioalParameters );
                client.openConnection();
                {
                    List<Object>    arguments = new List<Object>();
                                    arguments.Add( "1" );
                                    arguments.Add( "1" );

                    String commandToExecute = client.getDymanicSqlBuilder().buildExecProcQuery( "E_PGM.TEST", ref arguments );

                    client.executeDynamicSql( commandToExecute );

                    //client.executeDynamicSqlWithReturnValue( commandToExecute, "E_FIC.POCARES", "RESULTE", "RESCDCF", "2747", ref result );
                }
                client.closeConnection();

                Console.WriteLine( String.Format("Result: ->{0}<-",result) );

                ////////////iDB2Command cmd = new iDB2Command( "exec DISVEROBJ.RGSAF09 @val1" );

                ////////////Build the parameter
                ////////////iDB2Parameter   parm1 = cmd.CreateParameter();
                ////////////parm1.ParameterName = "@val1";
                ////////////parm1.iDB2DbType = iDB2DbType.iDB2Integer;
                ////////////parm1.Direction = System.Data.ParameterDirection.InputOutput;

                ////////////Attach the parameter to the command
                ////////////cmd.Parameters.Add( parm1 );          
            }
            catch( Exception e )
            {
                System.Console.WriteLine( e.ToString() );
            }
        }
    }
}
