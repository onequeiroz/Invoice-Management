using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using Vibbraneo.API.Repository.Interfaces;
using Vibbraneo.API.Models;
using System.Linq;

namespace Vibbraneo.API.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;

        public ExpenseRepository(IDatabaseConnectionFactory _databaseConnectionFactory)
        {
            databaseConnectionFactory = _databaseConnectionFactory;
        }

        public List<ExpenseModel> Get(int? id)
        {
            List<ExpenseModel> result;
            var filters = new Dictionary<string, object>()
            {
                { "ID", id },
            };

            string query = GetConsultQuery();

            using (IDbConnection connection = databaseConnectionFactory.ConnectionQuery())
            {
                connection.Open();

                result = connection.Query<ExpenseModel>(sql: query, param: filters).ToList();
            }

            return result;
        }

        public int Insert(InsertExpenseModel model)
        {
            int idInserted = 0;

            var filters = new Dictionary<string, object>()
            {
                { "ID_CATEGORY", model.IdCategory },
                { "VALUE", model.Value },
                { "NAME", model.Name },
                { "DT_PAYMENT", model.DatePayment },
                { "DT_REF", model.DateRef },
                { "ID_COMPANY", model.IdCompany == 0 ? null : model.IdCompany },
            };

            string query = GetInsertQuery();

            using (IDbConnection connection = databaseConnectionFactory.ConnectionExecute())
            {
                connection.Open();

                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        idInserted = connection.ExecuteScalar<int>(sql: query, param: filters, transaction: transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        databaseConnectionFactory.Rollback(connection, transaction);
                        throw new Exception(databaseConnectionFactory.GetErrorMessageInsert(), ex);
                    }
                }
            }

            return idInserted;
        }

        public bool Update(UpdateExpenseModel model)
        {
            int rowsUpdated = 0;

            var filters = new Dictionary<string, object>()
            {
                { "ID_EXPENSE", model.IdExpense },
                { "ID_CATEGORY", model.IdCategory },
                { "VALUE", model.Value },
                { "NAME", model.Name },
                { "DT_PAYMENT", model.DatePayment },
                { "DT_REF", model.DateRef },
                { "ID_COMPANY", model.IdCompany == 0 ? null : model.IdCompany },
            };

            string query = GetUpdateQuery();

            using (IDbConnection connection = databaseConnectionFactory.ConnectionExecute())
            {
                connection.Open();

                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        rowsUpdated = connection.ExecuteScalar<int>(sql: query, param: filters, transaction: transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        databaseConnectionFactory.Rollback(connection, transaction);
                        throw new Exception(databaseConnectionFactory.GetErrorMessageUpdate(), ex);
                    }
                }
            }

            return rowsUpdated == 1;
        }

        public bool Delete(int id)
        {
            string query = $"DELETE FROM [dbo].[EXPENSE] WHERE ID_EXPENSE = {id}";

            using (IDbConnection connection = databaseConnectionFactory.ConnectionExecute())
            {
                connection.Open();

                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        connection.Execute(sql: query, transaction: transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        databaseConnectionFactory.Rollback(connection, transaction);
                        throw new Exception(databaseConnectionFactory.GetErrorMessageUpdate(), ex);
                    }
                }
            }

            return true;
        }

        private string GetConsultQuery()
        {
            return @"
                        SELECT 
	                         [ID_EXPENSE]		AS [IdExpense]
	                        ,E.[ID_CATEGORY]	AS [IdCategory]
	                        ,C.[NAME]			AS [NameCategory]
	                        ,[VALUE]			AS [Value]
	                        ,E.[NAME]			AS [Name]
	                        ,[DT_PAYMENT]		AS [DatePayment]
	                        ,[DT_REF]			AS [DateRef]
	                        ,E.[ID_COMPANY]		AS [IdCompany]
	                        ,COMPANY.[NAME]		AS [NameCompany]
                        FROM 
	                        [dbo].[EXPENSE] AS E WITH (NOLOCK)
	                        LEFT JOIN CATEGORY AS C
		                        ON C.ID_CATEGORY = E.ID_CATEGORY
	                        LEFT JOIN COMPANY AS COMPANY
		                        ON COMPANY.ID_COMPANY = E.ID_COMPANY
                        WHERE
                            ([ID_EXPENSE] = @ID OR @ID IS NULL)
                        ORDER BY 
                            [DT_PAYMENT] ASC, [DT_REF]
                    ";
        }

        private string GetInsertQuery()
        {
            return @"
                        INSERT INTO [dbo].[EXPENSE]
                        (
	                         [ID_CATEGORY]
	                        ,[VALUE]
	                        ,[NAME]
	                        ,[DT_PAYMENT]
	                        ,[DT_REF]
	                        ,[ID_COMPANY]
	                        ,[DT_ICLO]
	                        ,[TM_ICLO]
                        )
                        VALUES
                        (
	                         @ID_CATEGORY
	                        ,@VALUE
	                        ,@NAME
	                        ,@DT_PAYMENT
	                        ,@DT_REF
	                        ,@ID_COMPANY
	                        ,GETDATE()
	                        ,GETDATE()
                        )

                        SELECT @@IDENTITY;
                    ";
        }

        private string GetUpdateQuery()
        {
            return @"
                        UPDATE 
	                         [dbo].[EXPENSE]
                        SET	  
	                         [ID_CATEGORY]  = @ID_CATEGORY
	                        ,[VALUE]        = @VALUE
	                        ,[NAME]         = @NAME
	                        ,[DT_PAYMENT]   = @DT_PAYMENT
	                        ,[DT_REF]       = @DT_REF
	                        ,[ID_COMPANY]   = @ID_COMPANY
                        WHERE 
	                         [ID_EXPENSE]   = @ID_EXPENSE

                        SELECT @@ROWCOUNT;
                    ";
        }
    }
}
