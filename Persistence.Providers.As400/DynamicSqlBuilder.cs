using System;
using System.Text;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Persistence.Providers.As400
{
    public class DynamicSqlBuilder : Persistence.DataBase.AbstractDynamicSqlBuilder
	{
        #region buildInsertQuery()
        public override string buildInsertQuery( String table, DataBase.TraceEntities.TraceEntityCollection fields, String[] values )
        {
            StringBuilder   stringBuilderFields = new StringBuilder();
            StringBuilder   stringBuilderValues = new StringBuilder();
            String          queryString         = "INSERT INTO {0} ({1}) values ({2})";

            // fields preparation
            if( fields != null )
            {
                foreach( TraceEntity field in fields )
                    stringBuilderFields.AppendFormat( "{0}, ", field.Field );

                stringBuilderFields.Remove( stringBuilderFields.Length - 2, 2 ); // suppression des derniers ', '
            }

            // values preparation
            if( values != null )
            {
                for( int i = 0; i < values.Length; i++ )
                {
                    if( fields.Count >= values.Length )
                    {
                        String convertedValue = getConvertedValue( values[i], fields[i].DataType, fields[i].MaxLengthNumeric );
                        stringBuilderValues.AppendFormat( "{0}, ", convertedValue );
                    }
                    else
                        stringBuilderValues.AppendFormat( "{0}, ", values[i] );
                }

                stringBuilderValues.Remove( stringBuilderValues.Length - 2, 2 ); // suppression des derniers ', '
            }

            // build the query
            return string.Format( queryString, table, stringBuilderFields.ToString(), stringBuilderValues.ToString() );
        }
        #endregion

        #region buildUpdateQuery()
        public override string buildUpdateQuery(String table, DataBase.TraceEntities.TraceEntityCollection fields, String[] values)
        {
            StringBuilder   fieldValueCorrespondance = new StringBuilder();
            String          queryString              = "UPDATE {0} SET {1} where {2} = {3}";

            // make pairs between field <-> value
            for( int index=0; index < fields.Count-1; index++ )
            {
                String field = fields[ index ].Field;
                String value = getConvertedValue( values[index], fields[index].DataType, fields[index].MaxLengthNumeric );

                fieldValueCorrespondance.AppendFormat( "{0} = {1}, ", field, value );
            }
            fieldValueCorrespondance.Remove( fieldValueCorrespondance.Length - 2, 2 ); // suppression des derniers ', '

            // set the where clause
            String whereField = fields[ fields.Count-1 ].Field;
            String whereValue = getConvertedValue( values[fields.Count-1], fields[fields.Count-1].DataType, fields[fields.Count-1].MaxLengthNumeric );

            // build the query
            return string.Format( queryString, table, fieldValueCorrespondance, whereField, whereValue );
        }
        #endregion

        #region buildExecProcQuery()

        protected String fillWithSpaces( String stringtoFill, int size )
        {
            for( int i=0; i < size; i++ )
                stringtoFill += " ";

            return stringtoFill;
        }

        public override string buildExecProcQuery( String procedureName, ref List<Object> parameters )
        {
            #region Fill With Spaces
                    const int c_spacesCount = 760;

                    List<Object> newParameters = new List<Object>();

                    foreach( Object parameter in parameters )
                    {
                        if( parameter.GetType() == typeof( string ) )
                        {
                            String newValue = fillWithSpaces( parameter as String, c_spacesCount );

                            newParameters.Add( newValue );
                        }
                        else
                            newParameters.Add( parameter );
                    }

                    parameters.Clear();
                    parameters = newParameters;
            #endregion

            // build the query
            const String c_execInstruction = "call";

            StringBuilder query = new StringBuilder();

            query.AppendFormat( "{0} {1} (", c_execInstruction, procedureName );

            foreach( Object parameterObject in parameters )
            {
                if( parameterObject.GetType() == typeof(string) )
                {
                    String parameterObjectAsString = parameterObject as String;
                    if( -1 != parameterObjectAsString.IndexOf( "'" ) )
                        parameterObjectAsString = parameterObjectAsString.Replace( "'", "%" );

                    query.AppendFormat( "'{0}',", parameterObjectAsString );

                }
                else
                    query.AppendFormat( "{0},", parameterObject );
            }
            query.Remove( query.Length-1, 1 ); // remove the last ","
            query.Append( ")" );

            return query.ToString();
        }
        #endregion 

        #region getConvertedValue()
        public override string getConvertedValue( string val, int destinationTypeAsInt, int maxLength )
        {
            SqlDbType destinationType = (SqlDbType)destinationTypeAsInt;
            switch ( destinationType )
            {
                case SqlDbType.BigInt:
                    if( val == null )
                        return string.Empty;
                    
                    return val;
/*
                case SqlDbType.DateTime:
                    if( val == null || val == string.Empty )
                        return "NULL";

                    return String.Format("to_date(\'{0}\', \'ddmmyyyy hh24:mi:ss\')", val  );
*/
                case SqlDbType.DateTime:
                    if( val == null || val == string.Empty )
                        return "NULL";

                    DateTime dt2 = DateTime.Parse( val, new CultureInfo("fr-FR") );
                    return string.Format( "'{0}-{1}-{2} {3}'", dt2.Year, dt2.Month, dt2.Day, dt2.TimeOfDay.ToString() );

                default:
                    if( val == null || val == string.Empty )
                        return "''";

                    if( maxLength != -1 && val.Length > maxLength )
                        val = val.Substring( 0, maxLength );

                    return "'" + val + "'";
            }
        }
        #endregion
    }
}
