using System;
using System.Collections.Generic;
using Vibbraneo.API.Business.Interfaces;
using Vibbraneo.API.Models;
using Vibbraneo.API.Repository.Interfaces;

namespace Vibbraneo.API.Business
{
    public class ReportsBusiness : IReportsBusiness
    {
        private readonly IReportsRepository repository;

        public ReportsBusiness(IReportsRepository _repository)
        {
            repository = _repository;
        }

        public List<TotalValueByMonth> TotalInvoiceValueByMonth(int yearRef)
        {
            if (yearRef < 1990 || yearRef > 2999)
                throw new ArgumentException("The year reference must be between 1990 and 2999");

            return repository.TotalInvoiceValueByMonth(yearRef);
        }

        public List<TotalValueByMonth> TotalExpenseValueByMonth(int yearRef)
        {
            if (yearRef < 1990 || yearRef > 2999)
                throw new ArgumentException("The year reference must be between 1990 and 2999");

            return repository.TotalExpenseValueByMonth(yearRef);
        }

        public List<TotalValueByCategory> TotalExpenseValueByCategory(int yearRef)
        {
            if (yearRef < 1990 || yearRef > 2999)
                throw new ArgumentException("The year reference must be between 1990 and 2999");

            return repository.TotalExpenseValueByCategory(yearRef);
        }
    }
}
