using System;

namespace Persistence.Exceptions
{
    public class DynamicSqlException : System.Exception
    {
        public DynamicSqlException() :
            base()
        {}


        public DynamicSqlException( string message ) :
            base( message )
        {}
    }
}
