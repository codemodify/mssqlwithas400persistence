using System;
using System.Xml.Serialization;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Persistence.Providers.SqlServer
{
    [XmlRoot("IndexTrace")]
    [Serializable]
    public class TraceEntity : DataBase.TraceEntities.AbstractTraceEntity
    {
        #region Methods
                public TraceEntity() : 
                    base()
                {}

                public TraceEntity( string table, string field, SqlDbType oracleDataType, int maxLength ) :
                    base( table, field, (int)oracleDataType, maxLength )
                {}

                public TraceEntity( string table, string field, SqlDbType oracleDataType ) :
                    base( table, field, (int)oracleDataType )
                {}
        #endregion
    }
}
