using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository repository;
        public int PageSize = 4;

        public HomeController(IStoreRepository repo)
        {
            repository =repo;
        }
        public IActionResult Index(int productPage = 1)
        {
            ProductsListViewModel plvm = new ProductsListViewModel()
            {
                Products = repository.Products
                            .OrderBy(p => p.ProductId)
                            .Skip((productPage - 1) * PageSize)
                            .Take(PageSize),
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                }
            };


            return View(plvm);
        }

    }
}
