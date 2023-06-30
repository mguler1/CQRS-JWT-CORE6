using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class ProductCreateModel
    {

        [Required(ErrorMessage = "Ürün Adı Zorunludur")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Stok Zorunludur")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Fiyat Zorunludur")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Kategori Adı Zorunludur")]
        public int CategoryId { get; set; }
        public SelectList? Categories { get; set; }
    }
}
