using System.Collections.Generic;
using Vibbraneo.API.Models;

namespace Vibbraneo.API.Repository.Interfaces
{
    public interface IInvoiceRepository
    {
        /// <summary>
        /// Get Invoice registers
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<InvoiceModel> Get(int? id);

        /// <summary>
        /// Insert of an Invoice register
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Id Invoice inserted</returns>
        int Insert(InsertInvoiceModel model);

        /// <summary>
        /// Update an Invoice register
        /// </summary>
        /// <param name="model"></param>
        /// <returns>True or Error</returns>
        bool Update(UpdateInvoiceModel model);

        /// <summary>
        /// Delete an Invoice register
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True or Error</returns>
        bool Delete(int id);
    }
}
