using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using keremRestoran.Models;
using keremRestoran.Service;
using Microsoft.AspNetCore.Hosting;
using keremRestoran.Service.Interface;

namespace keremRestoran.Controllers
{
    public class HomeController : Controller
    {

        private readonly IProductService _productService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IProductService productService, IHostingEnvironment hostingEnvironment)
        {
            _productService = productService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index(User model)
        {
            return View(model);
        }

        public IActionResult TümÜrünler(User userModel)
        {

            var products = _productService.GetAllProducts().Select(k => new RestoranÜrün { Id = k.Id, Name = k.Name, Price = k.Price }).ToList();

            RestoranUserÜrünContainer container = new RestoranUserÜrünContainer();
            container.user = userModel;
            container.RestoranUserÜrünList = products;

            return View(container);
        }
    }

       
    }

