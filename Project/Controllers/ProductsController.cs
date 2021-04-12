using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Project.Controllers
{
    public class ProductsController : Controller
    {
        public class Product
        {
            public int id;
            public string name;
        }

            HttpClient client = new HttpClient();
            public IActionResult Index()
            {
                //IEnumerable<Order> OrderList;
                string ProductList;
                HttpResponseMessage response = client.GetAsync("Order").Result;
                ProductList = response.Content.ReadAsStringAsync().Result;
                JsonConvert.DeserializeObject<IEnumerable<Product>>(ProductList);
                return View(ProductList);
            }

            public IActionResult Create()
            {
                return View(new Product());
            }

            [HttpPost]
            public IActionResult Create(Product product)
            {
                HttpResponseMessage response = client.PostAsJsonAsync("Product", product).Result;
                return RedirectToAction("Index");
            }
            public IActionResult Edit(int id)
            {
                HttpResponseMessage Response = client.GetAsync("Product/" + id.ToString()).Result;
                string ProductEdit = Response.Content.ReadAsStringAsync().Result;
                JsonConvert.DeserializeObject<Product>(ProductEdit);
                return View(ProductEdit);

            }

            [HttpPost]
            public IActionResult Edit(Product product)
            {
                HttpResponseMessage response = client.PutAsJsonAsync("Product/" + product.id, product).Result;
                return RedirectToAction("Index");
            }

            public IActionResult Delete(int id)
            {
                HttpResponseMessage response = client.DeleteAsync("Product/" + id.ToString()).Result;
                return RedirectToAction("Index");
            }
        }
    
}
