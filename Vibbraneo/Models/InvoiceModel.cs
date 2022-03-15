using System;
using System.ComponentModel.DataAnnotations;

namespace Vibbraneo.API.Models
{
    public class InvoiceModel
    {
        public int IdInvoice { get; set; }
        public int IdCompany { get; set; }
        public string NameCompany { get; set; }
        public int InvoiceNumber { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public int IdMonthRef { get; set; }
        public string MonthRef { get; set; }
        public int YearRef { get; set; }
        public string DatePayment { get; set; }
        public string DateInclusion { get; set; }
        public string TimeInclusion { get; set; }
    }

    public class InsertInvoiceModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "The field IdCompany is required.")]
        public int IdCompany { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field InvoiceNumber is required.")]
        public int InvoiceNumber { get; set; }

        [Range(0.01, int.MaxValue, ErrorMessage = "The field Value is required.")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "The field Description is required.")]
        public string Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field Month is required.")]
        public int CodMonthRef { get; set; }

        [Range(1900, 2999, ErrorMessage = "The field Year is required between 1900 and 2999.")]
        public int YearRef { get; set; }

        [Required(ErrorMessage = "The field Payment Date is required.")]
        public DateTime DatePayment { get; set; }

    }

    public class UpdateInvoiceModel : InsertInvoiceModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "The Invoice's register Id is required to update.")]
        public int IdInvoice { get; set; }
    }
}
