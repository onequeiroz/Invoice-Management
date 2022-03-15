using System;
using System.Collections.Generic;
using Vibbraneo.API.Business.Interfaces;
using Vibbraneo.API.Models;
using Vibbraneo.API.Repository.Interfaces;

namespace Vibbraneo.API.Business
{
    public class CategoryBusiness : ICategoryBusiness
    {
        private readonly ICategoryRepository repository;

        public CategoryBusiness(ICategoryRepository _repository)
        {
            repository = _repository;
        }

        public List<CategoryModel> Get(int? id, string name, string description, bool isActive = true)
        {
            return repository.Get(id, name, description, isActive);
        }

        public int Insert(InsertCategoryModel model)
        {
            return repository.Insert(model);
        }

        public bool Update(UpdateCategoryModel model)
        {
            bool success = repository.Update(model);

            if (!success)
                throw new Exception("The update process was finished due to an error. Please, contact the administrator.");

            return true;
        }
    }
}
