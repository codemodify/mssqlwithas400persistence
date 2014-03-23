using System;
using System.Collections.Generic;

namespace Persistence.Tester
{
    partial class Program
    {
        delegate void TestFunctionPrototype();

        static void Main(string[] args)
        {
            Dictionary<String,TestFunctionPrototype>    _testFunctions = new Dictionary<String,TestFunctionPrototype>();
                                                        _testFunctions.Add( "A", POCLCAAUTO );
                                                        _testFunctions.Add( "B", POCLCATAB  );
                                                        _testFunctions.Add( "C", POCLCACPOS );
                                                        _testFunctions.Add( "D", POCLCAPS   );
                                                        _testFunctions.Add( "E", POCLCAHR   );
                                                        _testFunctions.Add( "F", POCLCAMN   );
                                                        _testFunctions.Add( "G", POCLCACOMP );
                                                        _testFunctions.Add( "H", POCLCAPHO  );
                                                        _testFunctions.Add( "I", POCLCAPROS );
                                                        _testFunctions.Add( "J", POCLCAREVD );
                                                        _testFunctions.Add( "K", POCLCAOBS  );
                                                        _testFunctions.Add( "L", POCLCAVOBS );
                                                        _testFunctions.Add( "M", POCLCAHPHO );
                                                        _testFunctions.Add( "N", POCLCABIL );
                                                        _testFunctions.Add( "O", Test );

            foreach( KeyValuePair<String,TestFunctionPrototype> keyValuePair in _testFunctions )
            {
                Console.WriteLine( String.Format("{0} -> {1}", keyValuePair.Key, keyValuePair.Value.Method.Name) );
            }

            Console.WriteLine("");
            {
                ConsoleKeyInfo cnsoleKeyInfo = cnsoleKeyInfo=Console.ReadKey();
                Console.WriteLine("");

                if( cnsoleKeyInfo.Key != ConsoleKey.X )
                {
                    TestFunctionPrototype   testFunctionPrototype = _testFunctions[ cnsoleKeyInfo.Key.ToString() ];
                                            testFunctionPrototype();
                }
            }
            Console.ReadLine();
        }
    }
}
