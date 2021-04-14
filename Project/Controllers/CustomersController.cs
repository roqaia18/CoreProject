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
    public class CustomersController : Controller
    {
        public class Customer
        {
            public int ID { get; set; }

            public string Name { get; set; }
            [Required]
            public string Email { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string Phone { get; set; }

            //navigation properties

            public List<Order> Orders { get; set; }
        }

        HttpClient client = new HttpClient();
        public IActionResult Index()
        {
            //IEnumerable<Order> OrderList;
            string CustomerList;
            HttpResponseMessage response = client.GetAsync("https://localhost:44393/api/Customers").Result;
            CustomerList = response.Content.ReadAsStringAsync().Result;
            List<Customer> allCustomer = JsonConvert.DeserializeObject<IEnumerable<Customer>>(CustomerList).ToList();
            return View(allCustomer);
        }

        public IActionResult Create()
        {
            return View(new Customer());
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            HttpResponseMessage response = client.PostAsJsonAsync("https://localhost:44393/api/Customers", customer).Result;
            TempData["SuccessMessage"] = customer.Name + " Add Successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            HttpResponseMessage Response = client.GetAsync("https://localhost:44393/api/Customers/" + id.ToString()).Result;
            string CustomerEdit = Response.Content.ReadAsStringAsync().Result;
            object CustomerEditOBJ = JsonConvert.DeserializeObject<Customer>(CustomerEdit);
            return View(CustomerEditOBJ);

        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            HttpResponseMessage response = client.PutAsJsonAsync("https://localhost:44393/api/Customers/" + customer.ID, customer).Result;
            TempData["SuccessMessage"] = customer.Name + " Updated Successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            HttpResponseMessage response = client.DeleteAsync("https://localhost:44393/api/Customers/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Customer Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
