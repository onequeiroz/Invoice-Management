using System.Data;

namespace Vibbraneo.API.Repository.Interfaces
{
    public interface IDatabaseConnectionFactory
    {
        /// <summary>
        /// Returns a connection with the database to the method "Query"
        /// </summary>
        /// <returns>Connection</returns>
        IDbConnection ConnectionQuery();

        /// <summary>
        /// Returns a connection with the database to the method "Execute"
        /// </summary>
        /// <returns>Connection</returns>
        IDbConnection ConnectionExecute();

        /// <summary>
        /// Rollbacks the current transaction and closes the current connection
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        void Rollback(IDbConnection connection, IDbTransaction transaction);

        /// <summary>
        /// Returns a message for an Insert process error
        /// </summary>
        /// <returns>String</returns>
        string GetErrorMessageInsert();

        /// <summary>
        /// Returns a message for an Update process error
        /// </summary>
        /// <returns>String</returns>
        string GetErrorMessageUpdate();
    }
}
