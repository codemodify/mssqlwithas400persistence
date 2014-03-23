using System;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

namespace Persistence.DataBase.TraceEntities
{
    public class TraceEntitySerializer
    {
        #region Variables
                private TraceEntityCollection   _collection     = new TraceEntityCollection();
                private String			        _configFileName = String.Empty;
                private System.Type             _entityType     = null;
        #endregion

        #region Methods
                public TraceEntitySerializer( System.Type entityType )
                {
                    _entityType = entityType;
                    _configFileName = getAppFilePath();
                }

                public TraceEntitySerializer( System.Type entityType, string configFileName )
                {
                    _entityType = entityType;
                    _configFileName = configFileName == null ? getAppFilePath() : configFileName;
                }

                private String getAppFilePath()
                {
                    return String.Format
                    (
                        "{0}\\{1}.xml",
                        AppDomain.CurrentDomain.BaseDirectory,
                        AppDomain.CurrentDomain.FriendlyName
                    );
                }
        #endregion

        #region Load / Save
                public void load()
                {
                    this._collection.Clear();

                    XmlSerializer serializer = new XmlSerializer( typeof(TraceEntityCollection), new Type[]{_entityType} );

                    using( StreamReader streamReader = new StreamReader(_configFileName) )
                    {
                        this._collection = (TraceEntityCollection) serializer.Deserialize( streamReader );
                    }
                }

                public void save()
                {
                    XmlSerializer serializer = new XmlSerializer( typeof(TraceEntityCollection), new Type[]{_entityType} );
                    using( StreamWriter streamWriter = new StreamWriter(_configFileName) )
                    {
                        serializer.Serialize( streamWriter, this._collection );
                    }
                }
        #endregion

        #region Methods
                public void add( AbstractTraceEntity traceEntity )
                {
                    this._collection.Add( traceEntity );
                }

                // Obtient un sous-ensemble en fonction du nom de table.
                public TraceEntityCollection getSubSet( string tableName )
                {
                    TraceEntityCollection result = new TraceEntityCollection();

                    foreach( AbstractTraceEntity traceEntity in  this._collection )
                    {
                        if( traceEntity.Table == tableName )
                            result.Add( traceEntity );
                    }

                    return result;
                }
        #endregion
    }
}
