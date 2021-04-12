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
    public class Order {
      public int id;
       public string name;
    }
    public class OrdersController : Controller
    {

        HttpClient client = new HttpClient();
        public IActionResult Index()
        {
            //IEnumerable<Order> OrderList;
            string OrderList;
            HttpResponseMessage response = client.GetAsync("Order").Result;
            OrderList = response.Content.ReadAsStringAsync().Result;
            JsonConvert.DeserializeObject<IEnumerable<Order>>(OrderList);
            return View(OrderList);
        }

        public IActionResult Create() {
            return View(new Order());
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            HttpResponseMessage response = client.PostAsJsonAsync("Order", order).Result;
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            HttpResponseMessage Response = client.GetAsync("Order/" + id.ToString()).Result;
            string OrderEdit = Response.Content.ReadAsStringAsync().Result;
            JsonConvert.DeserializeObject<Order>(OrderEdit);
                return View(OrderEdit);
            
        }

        [HttpPost]
        public IActionResult Edit(Order order)
        {
            HttpResponseMessage response = client.PutAsJsonAsync("Order/" + order.id, order).Result;
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            HttpResponseMessage response = client.DeleteAsync("Order/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }
    }
}
