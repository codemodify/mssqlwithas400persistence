using System;

namespace Persistence.Exceptions
{
    public class NoProviderFoundInAssembly : System.Exception
    {
        public NoProviderFoundInAssembly() :
            base()
        {}


        public NoProviderFoundInAssembly( string message ) :
            base( message )
        {}
    }
}
