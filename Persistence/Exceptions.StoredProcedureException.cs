using System;

namespace Persistence.Exceptions
{
	public class StoredProcedureException : System.Exception
	{
		public StoredProcedureException() :
            base()
		{}


        public StoredProcedureException( string message ) :
            base( message )
        {}
	}
}
