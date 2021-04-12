using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            public int ID { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            public int Price { get; set; }
            public int Weight { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }
            public DateTime Create_Date { get; set; }
            [Required]
            public int Stock { get; set; }
            //navigation properties

            public List<Order> Orders { get; set; }
        }

            HttpClient client = new HttpClient();
            public IActionResult Index()
            {
                //IEnumerable<Order> OrderList;
                string ProductList;
                HttpResponseMessage response = client.GetAsync("https://localhost:44393/api/products/1").Result;
                ProductList = response.Content.ReadAsStringAsync().Result;
                List<Product> allProducts =  JsonConvert.DeserializeObject<IEnumerable<Product>>(ProductList).ToList();
                return View(allProducts);
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
                HttpResponseMessage response = client.PutAsJsonAsync("Product/" + product.ID, product).Result;
                return RedirectToAction("Index");
            }

            public IActionResult Delete(int id)
            {
                HttpResponseMessage response = client.DeleteAsync("Product/" + id.ToString()).Result;
                return RedirectToAction("Index");
            }
        }
    
}
