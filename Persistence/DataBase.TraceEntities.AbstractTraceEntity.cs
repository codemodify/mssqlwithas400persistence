using System;
using System.Xml.Serialization;
using System.Collections;

namespace Persistence.DataBase.TraceEntities
{
    [XmlRoot("IndexTrace")]
    [Serializable]
    public abstract class AbstractTraceEntity : ITraceEntity
    {
        #region Variables
                protected static    XmlSerializer   _xmlSerializer  = null;

                protected           string          _table          = String.Empty;
                protected           string          _field          = String.Empty;
                protected           int             _dbType         = -1;
                protected           string          _maxLength      = String.Empty;
        #endregion

        #region Xml Atrributes
                [XmlAttribute("NomTable")]
                public string Table
                {
                    get {return _table;}
                    set { _table = value;}
                }

                [XmlAttribute("NomChamp")]
                public string Field
                {
                    get {return _field;}
                    set { _field = value;}
                }

                [XmlAttribute("DataType")]
                public int DataType
                {
                    get { return _dbType;  }
                    set { _dbType = value; }
                }

                [XmlAttribute("TailleMax")]
                public string MaxLength
                {
                    get { return _maxLength; }
                    set { _maxLength = value;}
                }
        #endregion

        #region Properties
                public int MaxLengthNumeric
                {
                    get 
                    { 
                        if( _maxLength == null || _maxLength == string.Empty )
                            return -1;
                        try
                        {
                            return int.Parse( _maxLength );
                        }
                        catch
                        {
                            return -1;
                        }
                    }
                }
        #endregion

        #region Methods
                public AbstractTraceEntity()
                {}

                public AbstractTraceEntity( string table, string field, int dataType, int maxLength )
                {
                    this._table     = table;
                    this._field     = field;
                    this._dbType    = dataType;
                    this._maxLength = maxLength.ToString();
                }

                public AbstractTraceEntity( string table, string field, int dataType )
                {
                    this._table     = table;
                    this._field     = field;
                    this._dbType    = dataType;
                    this._maxLength = String.Empty;
                }
        #endregion
    }
}
