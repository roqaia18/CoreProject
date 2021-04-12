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
    public class CustomersController : Controller
    {
        public class Customer
        {
            public int id;
            public string name;
        }

            HttpClient client = new HttpClient();
            public IActionResult Index()
            {
                //IEnumerable<Order> OrderList;
                string CustomerList;
                HttpResponseMessage response = client.GetAsync("Customer").Result;
                CustomerList = response.Content.ReadAsStringAsync().Result;
                JsonConvert.DeserializeObject<IEnumerable<Customer>>(CustomerList);
                return View(CustomerList);
            }

            public IActionResult Create()
            {
                return View(new Customer());
            }

            [HttpPost]
            public IActionResult Create(Customer customer)
            {
                HttpResponseMessage response = client.PostAsJsonAsync("Customer", customer).Result;
                return RedirectToAction("Index");
            }
            public IActionResult Edit(int id)
            {
                HttpResponseMessage Response = client.GetAsync("Customer/" + id.ToString()).Result;
                string CustomerEdit = Response.Content.ReadAsStringAsync().Result;
                JsonConvert.DeserializeObject<Customer>(CustomerEdit);
                return View(CustomerEdit);

            }

            [HttpPost]
            public IActionResult Edit(Customer customer)
            {
                HttpResponseMessage response = client.PutAsJsonAsync("Customer/" + customer.id, customer).Result;
                return RedirectToAction("Index");
            }

            public IActionResult Delete(int id)
            {
                HttpResponseMessage response = client.DeleteAsync("Customer/" + id.ToString()).Result;
                return RedirectToAction("Index");
            }
        }
    }
