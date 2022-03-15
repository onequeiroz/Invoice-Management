using System.ComponentModel.DataAnnotations;

namespace Vibbraneo.API.Models
{
    public class CategoryModel
    {
        public int IdCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string DateInclusion { get; set; }
        public string TimeInclusion { get; set; }
        public string DateEnd { get; set; }
        public string TimeEnd { get; set; }

    }

    public class InsertCategoryModel
    {
        [Required(ErrorMessage = "The field Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field Description is required.")]
        public string Description { get; set; }
    }

    public class UpdateCategoryModel : InsertCategoryModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "The Category's register Id is required to update.")]
        public int IdCategory { get; set; }

        [Required(ErrorMessage = "The field IsActive is required.")]
        public bool IsActive { get; set; }
    }
}
