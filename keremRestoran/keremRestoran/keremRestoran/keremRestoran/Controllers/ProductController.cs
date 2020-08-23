using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using keremRestoran.Models;
using keremRestoran.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace keremRestoran.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        public ProductController(IProductService productService, IUserService userService)
        {
            _productService = productService;
            _userService = userService;
        }


        public IActionResult Index(string id)
        {
            var userModel = _userService.GetUserById(id);
            var products = _productService.GetAllProducts().Select(k => new RestoranÜrün { Id = k.Id, Name = k.Name, Price = k.Price }).ToList();

            RestoranUserÜrünContainer container = new RestoranUserÜrünContainer();
            container.user = userModel;
            container.RestoranUserÜrünList = products;

            return View(container);
        }

        [HttpGet]
        public IActionResult Menüler(string id)
        {
            var userModel = _userService.GetUserById(id);
            var products = _productService.GetProductsByCategory("menüler").Select(k => new RestoranÜrün { Id = k.Id, Name = k.Name, Price = k.Price }).ToList();

            RestoranUserÜrünContainer container = new RestoranUserÜrünContainer();
            container.user = userModel;
            container.RestoranUserÜrünList = products;

            return View(container);
        }

        [HttpGet]
        public IActionResult İçecekler(string id)
        {
            var userModel = _userService.GetUserById(id);
            var products = _productService.GetProductsByCategory("içecekler").Select(k => new RestoranÜrün { Id = k.Id, Name = k.Name, Price = k.Price }).ToList();
            RestoranUserÜrünContainer container = new RestoranUserÜrünContainer();
            container.user = userModel;
            container.RestoranUserÜrünList = products;
            return View(container);
        }

        [HttpGet]
        public IActionResult Tatlılar(string id)
        {
            var userModel = _userService.GetUserById(id);
            var products = _productService.GetProductsByCategory("tatlılar").Select(k => new RestoranÜrün { Id = k.Id, Name = k.Name, Price = k.Price }).ToList();
            RestoranUserÜrünContainer container = new RestoranUserÜrünContainer();
            container.user = userModel;
            container.RestoranUserÜrünList = products;
            return View(container);
        }

        [HttpGet]
        public IActionResult Rezervasyon(string id)
        {
            var userModel = _userService.GetUserById(id);
            var products = _productService.GetProductsByCategory("rezervasyon").Select(k => new RestoranÜrün { Id = k.Id, Name = k.Name, Price = k.Price }).ToList();
            RestoranUserÜrünContainer container = new RestoranUserÜrünContainer();
            container.user = userModel;
            container.RestoranUserÜrünList = products;
            return View(container);
        }

    }

}