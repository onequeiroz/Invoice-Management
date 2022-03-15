using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using Vibbraneo.API.Repository.Interfaces;
using Vibbraneo.API.Models;
using System.Linq;

namespace Vibbraneo.API.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;

        public InvoiceRepository(IDatabaseConnectionFactory _databaseConnectionFactory)
        {
            databaseConnectionFactory = _databaseConnectionFactory;
        }

        public List<InvoiceModel> Get(int? id)
        {
            List<InvoiceModel> result;
            var filters = new Dictionary<string, object>()
            {
                { "ID", id },
            };

            string query = GetConsultQuery();

            using (IDbConnection connection = databaseConnectionFactory.ConnectionQuery())
            {
                connection.Open();

                result = connection.Query<InvoiceModel>(sql: query, param: filters).ToList();
            }

            return result;
        }

        public int Insert(InsertInvoiceModel model)
        {
            int idInserted = 0;

            var filters = new Dictionary<string, object>()
            {
                { "ID_COMPANY", model.IdCompany },
                { "INVOICE_NUMBER", model.InvoiceNumber },
                { "VALUE", model.Value },
                { "DESCRIPTION", model.Description },
                { "CD_MONTH_REF", model.CodMonthRef },
                { "YEAR_REF", model.YearRef },
                { "DT_PAYMENT", model.DatePayment },
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

        public bool Update(UpdateInvoiceModel model)
        {
            int rowsUpdated = 0;

            var filters = new Dictionary<string, object>()
            {
                { "ID_INVOICE", model.IdInvoice },
                { "ID_COMPANY", model.IdCompany },
                { "INVOICE_NUMBER", model.InvoiceNumber },
                { "VALUE", model.Value },
                { "DESCRIPTION", model.Description },
                { "CD_MONTH_REF", model.CodMonthRef },
                { "YEAR_REF", model.YearRef },
                { "DT_PAYMENT", model.DatePayment },
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
            string query = $"DELETE FROM [dbo].[INVOICE] WHERE ID_INVOICE = {id}";

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
	                         [ID_INVOICE]		AS [IdInvoice]
	                        ,I.[ID_COMPANY]		AS [IdCompany]
	                        ,C.[NAME]			AS [NameCompany]
	                        ,[INVOICE_NUMBER]	AS [InvoiceNumber]
	                        ,[VALUE]			AS [Value]
	                        ,[DESCRIPTION]		AS [Description]
	                        ,I.[ID_MONTH_REF]	AS [IdMonthRef]
	                        ,M.[DE_MONTH_REF]	AS [MonthRef]
	                        ,[YEAR_REF]			AS [YearRef]
	                        ,CONVERT(VARCHAR, [DT_PAYMENT], 103) AS [DatePayment]
                        FROM 
	                        [dbo].[INVOICE] AS I WITH (NOLOCK)
	                        LEFT JOIN MONTH_REF AS M WITH (NOLOCK)
		                        ON M.ID_MONTH_REF = I.ID_MONTH_REF
	                        LEFT JOIN COMPANY AS C WITH (NOLOCK)
		                        ON C.ID_COMPANY = I.ID_COMPANY
                        WHERE
                            ([ID_INVOICE] = @ID OR @ID IS NULL)
                        ORDER BY 
                            [DT_PAYMENT] ASC, [YEAR_REF] ASC
                    ";
        }

        private string GetInsertQuery()
        {
            return @"
                        INSERT INTO [dbo].[INVOICE]
                        (
	                         [ID_COMPANY]
	                        ,[INVOICE_NUMBER]
	                        ,[VALUE]
	                        ,[DESCRIPTION]
	                        ,[ID_MONTH_REF]
	                        ,[YEAR_REF]
	                        ,[DT_PAYMENT]
	                        ,[DT_ICLO]
	                        ,[TM_ICLO]
                        )
                        VALUES
                        (
	                         @ID_COMPANY
	                        ,@INVOICE_NUMBER
	                        ,@VALUE
	                        ,@DESCRIPTION
	                        ,(SELECT ID_MONTH_REF FROM MONTH_REF WITH (NOLOCK) WHERE CD_MONTH_REF = @CD_MONTH_REF)
	                        ,@YEAR_REF
	                        ,@DT_PAYMENT
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
	                         [dbo].[INVOICE]
                        SET	  
	                         [ID_COMPANY]     = @ID_COMPANY
	                        ,[INVOICE_NUMBER] = @INVOICE_NUMBER
	                        ,[VALUE]          = @VALUE
	                        ,[DESCRIPTION]    = @DESCRIPTION
	                        ,[ID_MONTH_REF]   = (SELECT ID_MONTH_REF FROM MONTH_REF WITH (NOLOCK) WHERE CD_MONTH_REF = @CD_MONTH_REF)
	                        ,[YEAR_REF]       = @YEAR_REF
	                        ,[DT_PAYMENT]     = @DT_PAYMENT
                        WHERE 
	                         [ID_INVOICE]     = @ID_INVOICE

                        SELECT @@ROWCOUNT;
                    ";
        }
    }
}
