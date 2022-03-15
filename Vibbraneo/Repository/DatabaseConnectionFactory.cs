using System.Data;
using System.Data.SqlClient;
using Vibbraneo.API.Repository.Interfaces;

namespace Vibbraneo.API.Repository
{
    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
	{
		protected string ConnectionString { get; set; }

		public DatabaseConnectionFactory(string connectionString)
		{
			ConnectionString = connectionString;
		}

		public virtual IDbConnection ConnectionQuery()
		{
			return new SqlConnection(ConnectionString);
		}

		public virtual IDbConnection ConnectionExecute()
		{
			return new SqlConnection(ConnectionString);
		}

		public void Rollback(IDbConnection connection, IDbTransaction transaction)
        {
			transaction.Rollback();
			transaction.Dispose();
			connection.Close();
			connection.Dispose();
		}

		public string GetErrorMessageInsert()
        {
			return "The insert process was finished due to an error. Please, contact the administrator.";
		}

		public string GetErrorMessageUpdate()
		{
			return "The update process was finished due to an error. Please, contact the administrator.";
		}
	}
}
