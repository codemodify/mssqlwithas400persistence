using System;
using System.Collections.Generic;

namespace Persistence.Tester
{
    partial class Program
    {
        static void POCLCAPHO()
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
                                    arguments.Add("2747");
                                    arguments.Add("817516");
                                    arguments.Add("N");
                                    arguments.Add("");
                                    arguments.Add("");
                                    arguments.Add("");
                                    arguments.Add("");
                                    arguments.Add("");

                    String commandToExecute = client.getDymanicSqlBuilder().buildExecProcQuery( "E_PGM.POCLCAPHO", ref arguments );

                    client.executeDynamicSqlWithReturnValue( commandToExecute, "E_FIC.POCARES", "RESULTE", "RESCDCF", "2747", ref result );
                }
                client.closeConnection();

                Console.WriteLine( String.Format("Result: ->{0}<-",result) );
            }
            catch( Exception e )
            {
                System.Console.WriteLine( e.ToString() );
            }
        }
    }
}
