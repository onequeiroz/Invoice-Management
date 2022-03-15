using System.Collections.Generic;
using Vibbraneo.API.Models;

namespace Vibbraneo.API.Repository.Interfaces
{
    public interface IReportsRepository
    {
        /// <summary>
        /// Gets a report with the total invoice value by month, according to the year reference parameter
        /// </summary>
        /// <param name="yearRef"></param>
        /// <returns>List TotalInvoiceValueByMonth</returns>
        List<TotalValueByMonth> TotalInvoiceValueByMonth(int yearRef);

        /// <summary>
        /// Gets a report with the total expense value by month, according to the year reference parameter
        /// </summary>
        /// <param name="yearRef"></param>
        /// <returns>List TotalExpenseValueByMonth</returns>
        List<TotalValueByMonth> TotalExpenseValueByMonth(int yearRef);

        /// <summary>
        /// Gets a report with the total expense value by category, according to the year reference parameter
        /// </summary>
        /// <param name="yearRef"></param>
        /// <returns>List TotalValueByCategory</returns>
        List<TotalValueByCategory> TotalExpenseValueByCategory(int yearRef);
    }
}
