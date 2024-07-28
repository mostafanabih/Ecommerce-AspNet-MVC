using Ecommerce.Data.Base;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Data.Services
{
    public class CategoryServices : EntityBaseRepository<Category>,ICategoryServices
    {
        public CategoryServices(EcommerceDbContext context):base(context) 
        {
        }
        
    }
}
