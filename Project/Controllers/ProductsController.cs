using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
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

            // 08/04/2021
            public string ChangeDateFormat(DateTime date)
            {
                string newDate = date.ToShortDateString();
                string day = newDate.Substring(0, 2);
                string month = newDate.Substring(3, 2);
                string year = newDate.Substring(6);
                return year+"-"+month+"-"+day;
            }
        }

        HttpClient client = new HttpClient();
        public IActionResult Index()
        {
            //IEnumerable<Order> OrderList;
            string ProductList;
            HttpResponseMessage response = client.GetAsync("https://localhost:44393/api/products").Result;
            ProductList = response.Content.ReadAsStringAsync().Result;
            List<Product> allProducts =  JsonConvert.DeserializeObject<IList<Product>>(ProductList).ToList();
            return View(allProducts);
        }

        public IActionResult EachProduct(int id)
        {
            string Product;
            HttpResponseMessage response = client.GetAsync("https://localhost:44393/api/products/"+id).Result;
            Product = response.Content.ReadAsStringAsync().Result;
            ViewBag.currentProduct = JsonConvert.DeserializeObject<Product>(Product);
            return View();
        }

        public IActionResult Create()
        {
            return View(new Product() );
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            string fileName = product.Name + product.Price + product.Weight;
            System.IO.File.Create("wwwroot/Images/" + fileName + ".png").Dispose();

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(product.Image), "wwwroot/Images/" + fileName + ".png");
            }

            //using (WebClient webClient = new WebClient())
            //{
            //    byte[] data = webClient.DownloadData(product.Image);

            //    using (MemoryStream mem = new MemoryStream(data))
            //    {
            //        using (var yourImage = Image.FromStream(mem))
            //        {
            //            yourImage.Save("wwwroot/Images/image" + product.ID + ".png" , ImageFormat.Png);
            //        }
            //    }

            //}

            //product.Create_Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                string ProudctInString = JsonConvert.SerializeObject(product);
                StringContent requestBody = new StringContent(ProudctInString, Encoding.UTF8, "application/json");
                HttpResponseMessage respMessage = client.PostAsync("https://localhost:44393/api/products", requestBody).Result;
                
                TempData["Added"] = "Added";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            HttpResponseMessage Response = client.GetAsync("https://localhost:44393/api/products/" + id.ToString()).Result;
            string ProductEdit = Response.Content.ReadAsStringAsync().Result;
            Product myProductToEdit = JsonConvert.DeserializeObject<Product>(ProductEdit);
            return View(myProductToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            string fileName = product.Name + product.Price + product.Weight;
            System.IO.File.Create("wwwroot/Images/" + fileName + ".png").Dispose();

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(product.Image), "wwwroot/Images/" + fileName + ".png");
            }
            string SerializedObject = JsonConvert.SerializeObject(product);
            StringContent requestBody = new StringContent(SerializedObject, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = client.PutAsync("https://localhost:44393/api/products/" + product.ID , requestBody).Result;
            //HttpResponseMessage response = client.PutAsJsonAsync("Product/" + product.ID, product).Result;
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            HttpResponseMessage response = client.DeleteAsync("https://localhost:44393/api/products/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }




    }
    
}
