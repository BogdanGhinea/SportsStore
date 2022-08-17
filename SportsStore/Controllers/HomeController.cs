﻿using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
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
            return View(repository.Products
                            .OrderBy(p =>p.ProductId)
                            .Skip((productPage -1)*PageSize)
                            .Take(PageSize));
        }

    }
}
