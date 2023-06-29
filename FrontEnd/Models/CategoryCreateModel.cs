using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class CategoryCreateModel
    {
        [Required(ErrorMessage ="Kategori Adı Zorunludur")]
        public string?  Definition { get; set; }
    }
}
