using System;

namespace Persistence.Exceptions
{
	public class ConnectionException : System.Exception
	{
		public ConnectionException() :
            base()
		{}


        public ConnectionException( string message ) :
            base( message )
        {}
	}
}
