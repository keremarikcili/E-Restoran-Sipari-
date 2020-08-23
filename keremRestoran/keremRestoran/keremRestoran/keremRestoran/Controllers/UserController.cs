using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using keremRestoran.Models;
using keremRestoran.Service.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using static keremRestoran.Models.User;

namespace keremRestoran.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        private readonly IProductService _productService;

        public UserController(IUserService userService, IProductService productService)
        {
            _userService = userService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            User model = new User();
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(User model)
        {
            var result = _userService.LoginUser(model);

            if (result.IsSuccess)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Id)
                    };
                ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Product", new { Id = model.Id });


            }
            else
            {
                model.Error = result.Error;
                return RedirectToAction("Index", "Home", model);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            User model = new User();
            return View(model);
        }

        [HttpPost]
        public IActionResult Register(User model)
        {

            var result = _userService.RegisterUser(model);
            if (result.IsSuccess)
            {

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }

        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult GoToRegister()
        {
            return RedirectToAction("Register", "User");
        }

        [HttpGet]
        public IActionResult AddProductToCart(string id,string userId)
        {
            var user = _userService.GetUserById(userId);

            if (user.Cart.Where(k => k.Id == id).FirstOrDefault() == null)
            {
                user.Cart.Add(new ShoppingCartItem { Id = id, Quantity = 1 });
            }
            else
            {
                user.Cart.Where(k => k.Id == id).FirstOrDefault().Quantity++;
            }

            _userService.Update(user);
            return RedirectToAction("CartGet","User",new { Id = user.Id });

        }

        [HttpGet]
        public IActionResult DecProductToCart(string id, string userId)
        {
            var user = _userService.GetUserById(userId);

            if (user.Cart.Where(k => k.Id == id).FirstOrDefault() != null)
            {
                user.Cart.Where(k => k.Id == id && k.Quantity != 0).FirstOrDefault().Quantity--;
                

               

            }
           

            _userService.Update(user);
            return RedirectToAction("CartGet", "User", new { Id = user.Id });

        }


        [HttpPost]
        public IActionResult Cart(string id)
        {

            var user = _userService.GetUserById(id);
            if (user.CartItemCount > 0)
            {
                var products = _productService.GetProductsById(user.Cart.Select(k => k.Id).ToList());
                List<ShoppingCartItem> itemList = new List<ShoppingCartItem>();
                foreach (var sci in user.Cart)
                {
                    ShoppingCartItem item = new ShoppingCartItem();
                    var product = products.Where(k => k.Id == sci.Id).FirstOrDefault();
                    if (product != null)
                    {
                        item.Id = product.Id;
                        item.Name = product.Name;
                        item.Quantity = sci.Quantity;
                        item.Price = product.Price;
                        itemList.Add(item);
                    }
                }
                user.Cart = itemList;
            }
            RestoranUserÜrünContainer userProductContainer = new RestoranUserÜrünContainer();
            userProductContainer.user = user;

            return View(userProductContainer);
        }

        [HttpGet]
        public IActionResult CartGet(string id)
        {

            var user = _userService.GetUserById(id);
            if (user.CartItemCount > 0)
            {
                var products = _productService.GetProductsById(user.Cart.Select(k => k.Id).ToList());
                List<ShoppingCartItem> itemList = new List<ShoppingCartItem>();
                foreach (var sci in user.Cart)
                {
                    ShoppingCartItem item = new ShoppingCartItem();
                    var product = products.Where(k => k.Id == sci.Id).FirstOrDefault();
                    if (product != null)
                    {
                        item.Id = product.Id;
                        item.Name = product.Name;
                        item.Quantity = sci.Quantity;
                        item.Price = product.Price;
                        itemList.Add(item);
                        
                    }
                }
                user.Cart = itemList;
            }
            RestoranUserÜrünContainer userProductContainer = new RestoranUserÜrünContainer();
            userProductContainer.user = user;

            return View(userProductContainer);
        }

        [HttpPost]
        public IActionResult Delete(User model)
        {

            var result = _userService.Delete(model);
            if (result.IsSuccess)
            {

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }

        }

        [HttpPost]
        public IActionResult GoToDelete()
        {
            return RedirectToAction("Delete", "User");
        }
        [HttpGet]
        public IActionResult Delete()
        {
            User model = new User();
            return View(model);
        }
    }
}