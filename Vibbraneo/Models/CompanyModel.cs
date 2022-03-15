using System.ComponentModel.DataAnnotations;

namespace Vibbraneo.API.Models
{
    public class CompanyModel
    {
        public int IdCompany { get; set; }
        public string Cnpj { get; set; }
        public string Name { get; set; }
        public string CorporateName { get; set; }
        public string DateInclusion { get; set; }
        public string TimeInclusion { get; set; }

    }

    public class InsertCompanyModel
    {
        [Required(ErrorMessage = "The field CNPJ is required.")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "The field Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field Corporate Name is required.")]
        public string CorporateName { get; set; }
    }

    public class UpdateCompanyModel : InsertCompanyModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "The Company's register Id is required to update.")]
        public int IdCompany { get; set; }
    }
}
