using Final_Project.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static Project.Controllers.ProductsController;

namespace Final_Project.Controllers
{
    public class CartController : Controller
    {
        HttpClient client = new HttpClient();
        // GET: CartController
        public ActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            if (cart == null)
                ViewBag.total = 0;
            else
            ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            return View();
        }
        
        private int IsExist(string id)
        {
            List<Item> items = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Product.ID.ToString().Equals(id))
                {
                    return i;
                }
                  
            }
            return -1;
        }

        // GET: CartController/Details/5
        public ActionResult Buy(string id)
        {
            
            HttpResponseMessage res = client.GetAsync("https://localhost:44329/api/product/" + id).Result;
            Product Product = JsonConvert.DeserializeObject<Product>(res.Content.ReadAsStringAsync().Result);

            if(SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item() {ID=Product.ID,Product=Product, Quantity = 1 });
                
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = IsExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item() {ID=Product.ID,Product=Product, Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            return RedirectToAction("Index");
        }

        // GET: CartController/Create
        public ActionResult Remove(string id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = IsExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

      
    }
}
