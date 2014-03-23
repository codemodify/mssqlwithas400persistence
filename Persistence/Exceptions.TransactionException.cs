using System;

namespace Persistence.Exceptions
{
	public class TransactionException : System.Exception
	{
		public TransactionException() :
            base()
		{}


        public TransactionException( string message ) :
            base( message )
        {}
	}
}
