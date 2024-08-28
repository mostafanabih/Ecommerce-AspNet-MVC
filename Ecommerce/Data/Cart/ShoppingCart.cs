using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Data.Cart
{
    public class ShoppingCart
    {
        private readonly EcommerceDbContext _context;
        public string ShoppingCartId { get; set; }  
        public ShoppingCart(EcommerceDbContext context)
        {
            _context = context; 
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider service)
        {
            var session = service.GetRequiredService<IHttpContextAccessor>
                ().HttpContext.Session;
            var context = service.GetRequiredService<EcommerceDbContext>();
            string cartId = session.GetString("CartId")??Guid.NewGuid().ToString();
            session.SetString("CartId",cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        //Get All Items In Shopping Cart
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return _context.ShoppingCartItems.Where(x=>x.ShoppingCartId ==ShoppingCartId).Include(x=>x.Product).ToList();
        }

        //Calculate Total Amount In Shopping Cart Item
        public double GetShoppingCartTotal()
        
            => _context.ShoppingCartItems.Where(x=>x.ShoppingCartId == ShoppingCartId).Select(x=>x.Product.Price * x.Amount).Sum();

        public int GetShoppingCartTotalAmount()
            => _context.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId).Select(x => x.Amount).Sum();
        public async Task AddItemToShoppingCart(Product product)
        {
            var ShoppingCartItem = await  _context.ShoppingCartItems.FirstOrDefaultAsync(x => x.ShoppingCartId == ShoppingCartId && x.Product.Id == product.Id);
            if (ShoppingCartItem == null)
            {
                ShoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1
                };
                await _context.ShoppingCartItems.AddAsync(ShoppingCartItem);
            }
            else
            {
                ShoppingCartItem.Amount +=1;
            }
            await _context.SaveChangesAsync();
        }

        public async Task RemoveItemFromShoppingCart(Product product)
        {
            var ShoppingCartItem = await _context.ShoppingCartItems.FirstOrDefaultAsync(x => x.ShoppingCartId == ShoppingCartId && x.Product.Id == product.Id);
            if (ShoppingCartItem != null)
            {
                if (ShoppingCartItem.Amount > 1)
                {
                    ShoppingCartItem.Amount -= 1;
                }
                else { 
                    _context.ShoppingCartItems.Remove(ShoppingCartItem);    
                }
                await _context.SaveChangesAsync();
            }

        }

        public void ClearShoppingCart()
        {
            var items = _context.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId).ToList();
            _context.ShoppingCartItems.RemoveRange(items);
            _context.SaveChanges();
        }
    }

   
}
