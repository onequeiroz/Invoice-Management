using System;
using System.Collections.Generic;
using Vibbraneo.API.Business.Interfaces;
using Vibbraneo.API.Models;
using Vibbraneo.API.Repository.Interfaces;

namespace Vibbraneo.API.Business
{
    public class ExpenseBusiness : IExpenseBusiness
    {
        private readonly IExpenseRepository repository;

        public ExpenseBusiness(IExpenseRepository _repository)
        {
            repository = _repository;
        }

        public List<ExpenseModel> Get(int? id)
        {
            return repository.Get(id);
        }

        public int Insert(InsertExpenseModel model)
        {
            return repository.Insert(model);
        }

        public bool Update(UpdateExpenseModel model)
        {
            bool success = repository.Update(model);

            if (!success)
                throw new Exception("The update process was finished due to an error. Please, contact the administrator.");

            return true;
        }

        public bool Delete(int id)
        {
            if (id == 0)
                throw new ArgumentException("The Expense's register Id is required.");

            return repository.Delete(id);
        }
    }
}
