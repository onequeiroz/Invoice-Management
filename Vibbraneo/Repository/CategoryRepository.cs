using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Vibbraneo.API.Models;
using Vibbraneo.API.Repository.Interfaces;

namespace Vibbraneo.API.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;

        public CategoryRepository(IDatabaseConnectionFactory _databaseConnectionFactory)
        {
            databaseConnectionFactory = _databaseConnectionFactory;
        }

        public List<CategoryModel> Get(int? id, string name, string description, bool isActive = true)
        {
            List<CategoryModel> result;
            var filters = new Dictionary<string, object>()
            {
                { "ID", id },
                { "NAME", name },
                { "DESCRIPTION", description },
                { "ACTIVE", isActive },
            };

            string query = GetConsultQuery(name, description);

            using (IDbConnection connection = databaseConnectionFactory.ConnectionQuery())
            {
                connection.Open();

                result = connection.Query<CategoryModel>(sql: query, param: filters).ToList();
            }

            return result;
        }

        public int Insert(InsertCategoryModel model)
        {
            int idInserted = 0;
            var filters = new Dictionary<string, object>()
            {
                { "NAME", model.Name },
                { "DESCRIPTION", model.Description },
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
                    catch(Exception ex)
                    {
                        databaseConnectionFactory.Rollback(connection, transaction);
                        throw new Exception(databaseConnectionFactory.GetErrorMessageInsert(), ex);
                    }
                }
            }

            return idInserted;
        }

        public bool Update(UpdateCategoryModel model)
        {
            int rowsUpdated = 0;
            var filters = new Dictionary<string, object>()
            {
                { "ID_CATEGORY", model.IdCategory },
                { "NAME", model.Name },
                { "DESCRIPTION", model.Description },
                { "ACTIVE", model.IsActive },
                { "DT_END", !model.IsActive ? DateTime.Now.ToString() : null },
                { "TM_END", !model.IsActive ? DateTime.Now.ToString() : null },
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

        private string GetConsultQuery(string name, string description)
        {
            return $@"
                        SELECT 
	                         [ID_CATEGORY]	AS [IdCategory]
	                        ,[NAME]			AS [Name]
	                        ,[DESCRIPTION]  AS [Description]
	                        ,[ACTIVE]		AS [IsActive]
	                        ,CONVERT(VARCHAR, DT_ICLO, 23)		AS [DateInclusion]
	                        ,CONVERT(VARCHAR, TM_ICLO, 108)		AS [TimeInclusion]
	                        ,CONVERT(VARCHAR, DT_END, 23)		AS [DateEnd]
	                        ,CONVERT(VARCHAR, TM_END, 108)		AS [TimeEnd]
                        FROM 
	                        [dbo].[CATEGORY] WITH (NOLOCK)
                        WHERE
		                        ([NAME]			LIKE '%{name}%'        OR @NAME        IS NULL)
	                        AND ([DESCRIPTION]	LIKE '%{description}%' OR @DESCRIPTION IS NULL)
                            AND ([ID_CATEGORY] = @ID OR @ID IS NULL)
	                        AND ([ACTIVE] = @ACTIVE)
                        ORDER BY 
	                        [NAME] ASC
                     ";
        }

        private string GetInsertQuery()
        {
            return @"
                        INSERT INTO [dbo].[CATEGORY]
                        (
	                         [NAME]
	                        ,[DESCRIPTION]
	                        ,[ACTIVE]
	                        ,[DT_ICLO]
	                        ,[TM_ICLO]
                        )
                        VALUES
                        (
	                         @NAME
	                        ,@DESCRIPTION
	                        ,1
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
                             [dbo].[CATEGORY]
                        SET	 
                             [NAME]         = @NAME
	                        ,[DESCRIPTION]  = @DESCRIPTION
	                        ,[ACTIVE]       = @ACTIVE
	                        ,[DT_END]       = @DT_END
	                        ,[TM_END]       = @TM_END
                        WHERE 
                             [ID_CATEGORY]  = @ID_CATEGORY

                        SELECT @@ROWCOUNT;
                    ";
        }
    }
}
