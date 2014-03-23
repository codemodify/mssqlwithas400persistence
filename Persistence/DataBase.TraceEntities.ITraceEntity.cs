using System;
using System.Xml.Serialization;
using System.Collections;

namespace Persistence.DataBase.TraceEntities
{
    public interface ITraceEntity
    {
        #region Xml Atrributes
                string Table
                {
                    get;
                    set;
                }

                string Field
                {
                    get;
                    set;
                }

                int DataType
                {
                    get;
                    set;
                }

                string MaxLength
                {
                    get;
                    set;
                }
        #endregion

        #region Properties
                int MaxLengthNumeric
                {
                    get;
                }
        #endregion
    }
}
