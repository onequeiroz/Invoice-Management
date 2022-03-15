using System;
using System.Collections.Generic;
using Vibbraneo.API.Business.Interfaces;
using Vibbraneo.API.Models;
using Vibbraneo.API.Repository.Interfaces;

namespace Vibbraneo.API.Business
{
    public class CompanyBusiness : ICompanyBusiness
    {
        private readonly ICompanyRepository repository;

        public CompanyBusiness(ICompanyRepository _repository)
        {
            repository = _repository;
        }

        public List<CompanyModel> Get(int? id, string name, string cnpj, string corporateName)
        {
            return repository.Get(id, name, cnpj, corporateName);
        }

        public int Insert(InsertCompanyModel model)
        {
            return repository.Insert(model);
        }

        public bool Update(UpdateCompanyModel model)
        {
            bool success = repository.Update(model);

            if (!success)
                throw new Exception("The update process was finished due to an error. Please, contact the administrator.");

            return true;
        }

        public int RemoveDuplicates(int[] nums)
        {
            List<int> result = new List<int>();
            var r = new int[1, 1, 2];
            int unique = 0;
            bool firstNumber = true;

            for (int i = 0; i < nums.Length; i++)
            {
                var num = nums[i];
                if (firstNumber || num != unique)
                {
                    unique = num;
                    result.Add(num);
                }

                firstNumber = false;
            };

            return result.Count;
        }

    }
}
