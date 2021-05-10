using Final_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using static Project.Controllers.CustomersController;

namespace Final_Project.Controllers
{
    public class ProfileController : Controller
    {
        HttpClient client = new HttpClient();
     
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            string OrderList;
            HttpResponseMessage response = client.GetAsync("https://localhost:44329/api/Order").Result;
            OrderList = response.Content.ReadAsStringAsync().Result;
            List<Order> allOrder = JsonConvert.DeserializeObject<IEnumerable<Order>>(OrderList).ToList();
            List<Order> UserOrderItem = new List<Order>();
            foreach (var item in allOrder)
            {
                if (item.User_Id == userId && item.Confirm==true)
                {
                    UserOrderItem.Add(item);
                }
            }
            HttpResponseMessage Response = client.GetAsync("https://localhost:44329/api/user/" + userId.ToString()).Result;
            CustomIdentityUser User = JsonConvert.DeserializeObject<CustomIdentityUser>(Response.Content.ReadAsStringAsync().Result);
            ViewBag.allOrder = UserOrderItem;
            return View(User);
        }

        public IActionResult Edit()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            HttpResponseMessage Response = client.GetAsync("https://localhost:44329/api/user/" + userId.ToString()).Result;
            string CustomerEdit = Response.Content.ReadAsStringAsync().Result;
            object CustomerEditOBJ = JsonConvert.DeserializeObject<CustomIdentityUser>(CustomerEdit);
            return View(CustomerEditOBJ);
        }

        // POST: Profile/Edit/5
        [HttpPost]
        public IActionResult Edit(CustomIdentityUser customer)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            HttpResponseMessage Response = client.GetAsync("https://localhost:44329/api/user/" + userId.ToString()).Result;
            CustomIdentityUser User = JsonConvert.DeserializeObject<CustomIdentityUser>(Response.Content.ReadAsStringAsync().Result);
            User.Address = customer.Address;
            User.City = customer.City;
            User.img = customer.img;
            User.LastName = customer.LastName;
            User.FirstName = customer.FirstName;
            User.Phone = customer.Phone;
            User.Email = customer.Email;
            User.UserName = customer.Email;
            HttpResponseMessage response = client.PutAsJsonAsync("https://localhost:44329/api/user/" + userId, User).Result;
            TempData["SuccessMessage"] = customer.UserName + " Updated Successfully";
            return RedirectToAction("Index");
        }
       
    }
}
