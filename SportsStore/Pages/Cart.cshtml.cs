using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Infrastructure;
using SportsStore.Models;

namespace SportsStore.Pages
{
    public class CartModel : PageModel
    {
        private IStoreRepository repository;
        public CartModel(IStoreRepository repo, Cart cartService) {
            repository = repo;
            Cart = cartService;
        }
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }
        public IActionResult OnPost(long productId, string returnUrl) {
            Product product = repository.Products
                                .FirstOrDefault(p => p.ProductID == productId);
            Cart.AddItem(product, 1);
            return RedirectToPage(new { returnUrl = returnUrl });
        }
        public IActionResult OnPostRemove(long productId, string returnUrl) {
            Product p = Cart.Lines
                            .First(cl => cl.Product.ProductID == productId)
                            .Product;
            Cart.RemoveLine(p);
            return RedirectToPage(new { returnUrl = returnUrl });
        }
        public IActionResult OnPostIncrease(long productId, string returnUrl) {
            CartLine line = Cart.Lines
                                .First(cl => cl.Product.ProductID == productId);
            int quantity = line.Quantity + 1;
            Cart.SetQuantity(productId, quantity);
            return RedirectToPage(new { returnUrl = returnUrl });
        }
        public IActionResult OnPostDecrease(long productId, string returnUrl) {
            CartLine line = Cart.Lines
                                .First(cl => cl.Product.ProductID == productId);
            int quantity = Math.Max(1, line.Quantity - 1);
            Cart.SetQuantity(productId, quantity);
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
