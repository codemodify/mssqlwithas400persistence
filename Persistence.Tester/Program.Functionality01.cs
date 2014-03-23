using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.Tester
{
    partial class Program
    {
        static void Functionality01()
        {
            try
            {
                List<String>    additioalParameters = new List<String>();
                                additioalParameters.Add( "QTEMP,E_PGM,E_FIC" );

                Persistence.DataBase.IDataBasePersistence client = null;

                client = new Persistence.Providers.As400.Client();
                client.setParameters( "ETUDES", "SYSCAAP", "SYSCAAP", additioalParameters );
                client.openConnection();
                {
                    List<Object>    arguments = new List<Object>();
                                    arguments.Add( "2747"          );
                                    arguments.Add( "797279"        );
                                    arguments.Add( "abcdefghi 123" );

                    String commadToExecute = client.getDymanicSqlBuilder().buildExecProcQuery
                    (
                        "E_PGM.POCLCAOBS",
                        ref arguments
                    );

                    String result = null;
                    client.executeDynamicSqlWithReturnValue
                    (
                        commadToExecute,
                        "E_FIC.POCARES",
                        "RESULTE",
                        ref result
                    );
                }
                client.closeConnection();
            }
            catch( System.Exception e )
            {
                string s = e.ToString();
            }
        }
    }
}
