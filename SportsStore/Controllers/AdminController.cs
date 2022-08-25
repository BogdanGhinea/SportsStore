using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers {
    public class AdminController : Controller {
        private IStoreRepository repository;
        public AdminController(IStoreRepository repo) {
            repository = repo;
        }
        public IActionResult Index() {
            return View(repository.Products);
        }
        [HttpGet]
        public IActionResult Edit(int productId) {
            Product p = repository.Products
                            .FirstOrDefault(p => p.ProductID == productId);
            return View(p);
        }
        [HttpPost]
        public IActionResult Edit(Product product) {
            if (ModelState.IsValid) {
                repository.SaveProduct(product);
                return RedirectToAction("Index");
            }
            else {
                return View(product);
            }
        }
        [HttpPost]
        public IActionResult Delete(int productId)
        {
            repository.DeleteProduct(productId);
            return RedirectToAction("Index");
        }
    }
}
