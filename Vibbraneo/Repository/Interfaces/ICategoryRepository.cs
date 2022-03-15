using System.Collections.Generic;
using Vibbraneo.API.Models;

namespace Vibbraneo.API.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// Get Category registers based on Name and/or Description
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="isActive"></param>
        /// <returns>List CategoryModel</returns>
        List<CategoryModel> Get(int? id, string name, string description, bool isActive = true);

        /// <summary>
        /// Insert of a Category register
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Id Category inserted</returns>
        int Insert(InsertCategoryModel model);

        /// <summary>
        /// Update a Category register
        /// </summary>
        /// <param name="model"></param>
        /// <returns>True or Error</returns>
        bool Update(UpdateCategoryModel model);
    }
}
