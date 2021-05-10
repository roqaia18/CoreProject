using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using static Project.Controllers.ProductsController;

namespace Final_Project.Controllers
{
    public class HomeUserController : Controller
    {
        List<Item> Items = new List<Item>();
        HttpClient client = new HttpClient();
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.user = userId;
            HttpResponseMessage res = client.GetAsync("https://localhost:44329/api/product").Result;
            List<Product> allProducts = JsonConvert.DeserializeObject<IList<Product>>(res.Content.ReadAsStringAsync().Result).ToList();
            return View(allProducts);
        }
        public IActionResult Details(int id)
        {
            HttpResponseMessage res = client.GetAsync("https://localhost:44329/api/product/" + id).Result;
            Product Product = JsonConvert.DeserializeObject<Product>(res.Content.ReadAsStringAsync().Result);
            return View(Product);
        }
        public IActionResult Cart(int id)
        {
           
            HttpResponseMessage res = client.GetAsync("https://localhost:44329/api/product/" + id).Result;
            Product Product = JsonConvert.DeserializeObject<Product>(res.Content.ReadAsStringAsync().Result);
            Items.Add(new Item() { Product = Product, Quantity = 1 });
            
            return RedirectToAction("Index");
        }

        public IActionResult ShoppingCart()
        {
            return View(Items);
        }

    }
}
