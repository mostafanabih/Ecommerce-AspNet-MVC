﻿using Ecommerce.Data;
using Ecommerce.Data.Services;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductServices _services;
        private readonly ICategoryServices _categoryServices;
        public ProductsController(IProductServices services,ICategoryServices categoryServices)
        {
            _services = services;
            _categoryServices = categoryServices;
        }
        public async Task<IActionResult> Index(string searchTerm)
        {
            var Response = await _services.GetAllAsync(x=>x.Category);
            if (!string.IsNullOrEmpty(searchTerm)) 
            {
                Response = Response.Where(x=>x.Name.Contains(searchTerm)).ToList(); 
            }
            return View(Response);
        }

        public async Task<IActionResult> Details(int id)
        {
            var Product = await _services.GetByIdAsync(id,x=>x.Category); 
            return View(Product);
        }

        [HttpGet]
        public async Task<IActionResult> Create() 
        {
            ViewBag.CategoryId = await _categoryServices.GetAllAsync();
            return View();
        }

        [HttpPost, ActionName(nameof(Create))]

        public async Task<IActionResult> CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await _services.CreateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View("NotFound");   

        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Category = await _categoryServices.GetAllAsync();
            var productId = await _services.GetByIdAsync(id,x=>x.Category);
            return View(productId);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid) {
             await _services.UpdateAsync(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _services.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
            
        }
    }
}
