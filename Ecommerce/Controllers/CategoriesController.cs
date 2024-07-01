using Ecommerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly EcommerceDbContext _context;
        public CategoriesController(EcommerceDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult>  Index()
        {
            var Response =await  _context.Categories.ToListAsync();
            return View(Response);
        }
    }
}
