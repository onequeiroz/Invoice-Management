using System.Collections.Generic;
using Vibbraneo.API.Models;

namespace Vibbraneo.API.Business.Interfaces
{
    public interface ICompanyBusiness
    {
        /// <summary>
        /// Get Company registers based on Name, CNPJ and/or Corporate Name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="cnpj"></param>
        /// <param name="corporateName"></param>
        /// <returns>List CompanyModel</returns>
        List<CompanyModel> Get(int? id, string name, string cnpj, string corporateName);

        /// <summary>
        /// Insert of a Company register
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Id Company inserted</returns>
        int Insert(InsertCompanyModel model);

        /// <summary>
        /// Update a Company register
        /// </summary>
        /// <param name="model"></param>
        /// <returns>True or Error</returns>
        bool Update(UpdateCompanyModel model);

        int RemoveDuplicates(int[] nums);
    }
}
