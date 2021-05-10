using Final_Project.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            //IEnumerable<Order> OrderList;
            string CustomerList;
            HttpResponseMessage response = client.GetAsync("https://localhost:44329/api/user").Result;
            CustomerList = response.Content.ReadAsStringAsync().Result;
            List<CustomIdentityUser> allCustomer = JsonConvert.DeserializeObject<IEnumerable<CustomIdentityUser>>(CustomerList).ToList();
            return View(allCustomer);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new CustomIdentityUser());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CustomIdentityUser customer)
        {
            HttpResponseMessage response = client.PostAsJsonAsync("https://localhost:44329/api/user", customer).Result;
            TempData["SuccessMessage"] = customer.UserName + " Add Successfully";
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id)
        {
            HttpResponseMessage Response = client.GetAsync("https://localhost:44329/api/user/" + id.ToString()).Result;
            string CustomerEdit = Response.Content.ReadAsStringAsync().Result;
            object CustomerEditOBJ = JsonConvert.DeserializeObject<CustomIdentityUser>(CustomerEdit);
            return View(CustomerEditOBJ);

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(CustomIdentityUser customer)
        {
            HttpResponseMessage response = client.PutAsJsonAsync("https://localhost:44329/api/user/" + customer.Id, customer).Result;
            TempData["SuccessMessage"] = customer.UserName + " Updated Successfully";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            HttpResponseMessage response = client.DeleteAsync("https://localhost:44329/api/user/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Customer Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
