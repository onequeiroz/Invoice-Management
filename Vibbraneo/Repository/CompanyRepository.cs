using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Vibbraneo.API.Repository.Interfaces;
using Vibbraneo.API.Models;

namespace Vibbraneo.API.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;

        public CompanyRepository(IDatabaseConnectionFactory _databaseConnectionFactory)
        {
            databaseConnectionFactory = _databaseConnectionFactory;
        }

        public List<CompanyModel> Get(int? id, string name, string cnpj, string corporateName)
        {
            List<CompanyModel> result;
            var filters = new Dictionary<string, object>()
            {
                { "ID", id },
                { "CNPJ", cnpj },
                { "NAME", name },
                { "CORPORATE_NAME", corporateName },
            };

            string query = GetConsultQuery(name, cnpj, corporateName);

            using (IDbConnection connection = databaseConnectionFactory.ConnectionQuery())
            {
                connection.Open();

                result = connection.Query<CompanyModel>(sql: query, param: filters).ToList();
            }

            return result;
        }

        public int Insert(InsertCompanyModel model)
        {
            int idInserted = 0;

            var filters = new Dictionary<string, object>()
            {
                { "CNPJ", model.Cnpj },
                { "NAME", model.Name },
                { "CORPORATE_NAME", model.CorporateName },
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

        public bool Update(UpdateCompanyModel model)
        {
            int rowsUpdated = 0;

            var filters = new Dictionary<string, object>()
            {
                { "ID_COMPANY", model.IdCompany },
                { "CNPJ", model.Cnpj },
                { "NAME", model.Name },
                { "CORPORATE_NAME", model.CorporateName },
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

        private string GetConsultQuery(string name, string cnpj, string corporateName)
        {
            return $@"
                        SELECT 
	                         [ID_COMPANY]	  AS [IdCompany]
	                        ,[CNPJ]			  AS [Cnpj]
	                        ,[NAME]			  AS [Name]
	                        ,[CORPORATE_NAME] AS [CorporateName]
	                        ,CONVERT(VARCHAR, DT_ICLO, 23)  AS [DateInclusion]
	                        ,CONVERT(VARCHAR, TM_ICLO, 108) AS [TimeInclusion]
                        FROM 
	                        [dbo].[COMPANY] WITH (NOLOCK)
                        WHERE
		                        ([CNPJ]			  LIKE '%{cnpj}%'           OR @CNPJ           IS NULL)
	                        AND ([NAME]			  LIKE '%{name}%'           OR @NAME           IS NULL)
	                        AND ([CORPORATE_NAME] LIKE '%{corporateName}%'  OR @CORPORATE_NAME IS NULL)
                            AND ([ID_COMPANY] = @ID OR @ID IS NULL)
                        ORDER BY 
                            [NAME] ASC
                     ";
        }

        private string GetInsertQuery()
        {
            return @"
                        INSERT INTO [dbo].[COMPANY]
                        (
	                         [CNPJ]
	                        ,[NAME]
	                        ,[CORPORATE_NAME]
	                        ,[DT_ICLO]
	                        ,[TM_ICLO]
                        )
                        VALUES
                        (
	                         @CNPJ
	                        ,@NAME
	                        ,@CORPORATE_NAME
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
                             [dbo].[COMPANY]
                        SET  
                             [CNPJ]           = @CNPJ
	                        ,[NAME]           = @NAME
	                        ,[CORPORATE_NAME] = @CORPORATE_NAME
                        WHERE 
                             [ID_COMPANY]     = @ID_COMPANY

                        SELECT @@ROWCOUNT;
                    ";
        }
    }
}
