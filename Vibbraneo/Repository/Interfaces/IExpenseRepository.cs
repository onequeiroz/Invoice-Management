using System.Collections.Generic;
using Vibbraneo.API.Models;

namespace Vibbraneo.API.Repository.Interfaces
{
    public interface IExpenseRepository
    {
        /// <summary>
        /// Get Expense registers
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List ExpenseModel</returns>
        List<ExpenseModel> Get(int? id);

        /// <summary>
        /// Insert of an Expense register
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Id Expense inserted</returns>
        int Insert(InsertExpenseModel model);

        /// <summary>
        /// Update an Expense register
        /// </summary>
        /// <param name="model"></param>
        /// <returns>True or Error</returns>
        bool Update(UpdateExpenseModel model);

        /// <summary>
        /// Delete an Expense register
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True or Error</returns>
        bool Delete(int id);
    }
}
