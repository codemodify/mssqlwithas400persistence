using System;
using System.Collections;
using System.Xml.Serialization;

namespace Persistence.DataBase.TraceEntities
{
    [Serializable]
	public class TraceEntityCollection : CollectionBase
	{
		public TraceEntityCollection() :
            base()
		{}

        public AbstractTraceEntity this[ int index ]  
        {
            get  
            {
                return( (AbstractTraceEntity) List[index] );
            }
            set  
            {
                List[index] = value;
            }
        }

        public int Add( AbstractTraceEntity value )  
        {
            return( List.Add( value ) );
        }

        public int IndexOf( AbstractTraceEntity value )  
        {
            return( List.IndexOf( value ) );
        }

        public void Insert( int index, AbstractTraceEntity value )  
        {
            List.Insert( index, value );
        }

        public void Remove( AbstractTraceEntity value )  
        {
            List.Remove( value );
        }

        public bool Contains( AbstractTraceEntity value )  
        {
            return( List.Contains( value ) ); // If value is not of type Int16, this will return false.
        }
	}
}
