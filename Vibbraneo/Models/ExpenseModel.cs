using System;
using System.ComponentModel.DataAnnotations;

namespace Vibbraneo.API.Models
{
    public class ExpenseModel
    {
        public int IdExpense { get; set; }
        public int IdCategory { get; set; }
        public decimal Value { get; set; }
        public string Name { get; set; }
        public string NameCategory { get; set; }
        public string NameCompany { get; set; }
        public string DatePayment { get; set; }
        public string DateRef { get; set; }
        public int? IdCompany { get; set; }
        public string DateInclusion { get; set; }
        public string TimeInclusion { get; set; }
    }

    public class InsertExpenseModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "The field IdCategory is required.")]
        public int IdCategory { get; set; }

        [Range(0.01, int.MaxValue, ErrorMessage = "The field Value is required.")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "The field Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field Date Payment is required.")]
        public DateTime? DatePayment { get; set; }

        [Required(ErrorMessage = "The field Date Reference is required.")]
        public DateTime? DateRef { get; set; }

        public int? IdCompany { get; set; }

    }

    public class UpdateExpenseModel : InsertExpenseModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "The Expense's register Id is required to update.")]
        public int IdExpense { get; set; }
    }
}
