using System;
using System.Collections.Generic;
using Vibbraneo.API.Business.Interfaces;
using Vibbraneo.API.Models;
using Vibbraneo.API.Repository.Interfaces;

namespace Vibbraneo.API.Business
{
    public class InvoiceBusiness : IInvoiceBusiness
    {
        private readonly IInvoiceRepository repository;

        public InvoiceBusiness(IInvoiceRepository _repository)
        {
            repository = _repository;
        }

        public List<InvoiceModel> Get(int? id)
        {
            return repository.Get(id);
        }

        public int Insert(InsertInvoiceModel model)
        {
            return repository.Insert(model);
        }

        public bool Update(UpdateInvoiceModel model)
        {
            bool success = repository.Update(model);

            if (!success)
                throw new Exception("The update process was finished due to an error. Please, contact the administrator.");

            return true;
        }

        public bool Delete(int id)
        {
            if (id == 0)
                throw new ArgumentException("The Invoice's register Id is required.");

            return repository.Delete(id);
        }
    }
}
