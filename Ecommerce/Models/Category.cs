using Ecommerce.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
     
{
    public class Category:IBaseEntity
    {
        public Category() {
            Products = new HashSet<Product>();
        }
        [Key]
        public int Id { get; set; } 
        [Required(ErrorMessage ="Name Is Required")]
        [Display(Name="Category Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description Is Required")]
        public string Description { get; set; }


        // Navigational Propery
        public ICollection<Product> Products { get; set; }

    }


}
