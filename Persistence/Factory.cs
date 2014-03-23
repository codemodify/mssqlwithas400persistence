using System;

namespace Persistence
{
	public class Factory
	{
        #region loadProviderFromAssembly()
                public static void loadProviderFromAssembly
                (
                    String fullAssemblyFile, 
                    String typeName,
                    String traceEntityTypeName,
                    ref Persistence.DataBase.IDataBasePersistence dataBasePersistence,
                    ref System.Type traceEntityType
                )
                {
                    try
                    {
                        System.Reflection.Assembly  assembly = System.Reflection.Assembly.LoadFile( fullAssemblyFile );

                        dataBasePersistence = assembly.CreateInstance(typeName) as Persistence.DataBase.IDataBasePersistence;
                        traceEntityType     = assembly.CreateInstance(traceEntityTypeName).GetType();

                        if( (null == dataBasePersistence) || (null == traceEntityType) )
                        {
                            throw new Exceptions.NoProviderFoundInAssembly
                            (
                                String.Format
                                (
                                    "assemblyFile: {0}, typeName: {1}, traceEntityTypeName: {2}",
                                    fullAssemblyFile,
                                    typeName,
                                    traceEntityTypeName
                                )
                            );
                        }
                    }
                    #region Disable some nasty crashes
                    catch( System.Exception e )
                    {
                        throw new Exceptions.NoProviderFoundInAssembly
                        (
                            String.Format
                            (
                                "assemblyFile: {0}, typeName: {1}, additionalError: {2}",
                                fullAssemblyFile,
                                typeName,
                                e.ToString()
                            )
                        );
                    }
                    #endregion
                }
        #endregion
	}
}
