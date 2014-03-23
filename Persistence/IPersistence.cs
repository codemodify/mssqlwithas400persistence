using System;

namespace Persistence
{
    public interface IPersistence
    {
        #region Connection
                void openConnection();
                void closeConnection();
        #endregion

        #region Transaction
                void beginTransaction();
                void endTransaction();
                void rollBack();
        #endregion
    }
}
