using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Vibbraneo.API.Models;
using Vibbraneo.API.Repository.Interfaces;

namespace Vibbraneo.API.Repository
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;

        public ReportsRepository(IDatabaseConnectionFactory _databaseConnectionFactory)
        {
            databaseConnectionFactory = _databaseConnectionFactory;
        }

        public List<TotalValueByMonth> TotalInvoiceValueByMonth(int yearRef)
        {
            List<TotalValueByMonth> result;

            string query = $@"
                                SELECT 
	                                 M.CD_MONTH_REF	AS [CodMonth]
	                                ,M.DE_MONTH_REF	AS [DescMonth]
	                                ,SUM(I.VALUE)	AS [TotalValue]
                                FROM 
	                                [dbo].[INVOICE] AS I WITH (NOLOCK)
	                                LEFT JOIN [dbo].[MONTH_REF] AS M WITH (NOLOCK)
		                                ON M.ID_MONTH_REF = I.ID_MONTH_REF
                                WHERE 
	                                I.YEAR_REF = {yearRef}
                                GROUP BY 
	                                M.DE_MONTH_REF, M.CD_MONTH_REF
                                ORDER BY 
	                                M.CD_MONTH_REF ASC
                             ";

            using (IDbConnection connection = databaseConnectionFactory.ConnectionQuery())
            {
                connection.Open();

                result = connection.Query<TotalValueByMonth>(sql: query).ToList();
            }

            return result;
        }

        public List<TotalValueByMonth> TotalExpenseValueByMonth(int yearRef)
        {
            List<TotalValueByMonth> result;

            string query = $@"
                                SELECT 
	                                 M.CD_MONTH_REF	AS [CodMonth]
	                                ,M.DE_MONTH_REF	AS [DescMonth]
	                                ,SUM(E.VALUE)	AS [TotalValue]
                                FROM 
	                                [dbo].[EXPENSE] AS E WITH (NOLOCK)
	                                LEFT JOIN [dbo].[MONTH_REF] AS M WITH (NOLOCK)
		                                ON M.CD_MONTH_REF = MONTH(E.DT_REF)
                                WHERE 
	                                YEAR(E.DT_REF) = {yearRef}
                                GROUP BY 
	                                M.DE_MONTH_REF, M.CD_MONTH_REF
                                ORDER BY 
	                                M.CD_MONTH_REF ASC
                             ";

            using (IDbConnection connection = databaseConnectionFactory.ConnectionQuery())
            {
                connection.Open();

                result = connection.Query<TotalValueByMonth>(sql: query).ToList();
            }

            return result;
        }

        public List<TotalValueByCategory> TotalExpenseValueByCategory(int yearRef)
        {
            List<TotalValueByCategory> result;

            string query = $@"
                                SELECT 
	                                 C.NAME		   AS [Category]
	                                ,SUM(E.VALUE)  AS [TotalValue]
                                FROM 
	                                [dbo].[EXPENSE] AS E WITH (NOLOCK)
                                LEFT JOIN [dbo].[CATEGORY] AS C WITH (NOLOCK)
	                                ON C.ID_CATEGORY = E.ID_CATEGORY
                                WHERE 
	                                YEAR(E.DT_REF) = {yearRef}
                                GROUP BY C.NAME
                                ORDER BY C.NAME
                             ";

            using (IDbConnection connection = databaseConnectionFactory.ConnectionQuery())
            {
                connection.Open();

                result = connection.Query<TotalValueByCategory>(sql: query).ToList();
            }

            return result;
        }
    }
}
