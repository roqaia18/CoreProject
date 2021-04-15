using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static Project.Controllers.CustomersController;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.NumOfOrders = getNumberOfOrders();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        HttpClient client = new HttpClient();


        public int getNumberOfOrders()
        {
            string OrderList;
            HttpResponseMessage response = client.GetAsync("https://localhost:44393/api/Orders").Result;
            OrderList = response.Content.ReadAsStringAsync().Result;
            List<Order> allOrder = JsonConvert.DeserializeObject<IEnumerable<Order>>(OrderList).ToList();
            int numOfOrdersThisMonth = 0;
            foreach (var item in allOrder)
            {
                string date = item.OrderDate.ToShortDateString();
                string month = date.Substring(3, 2);
                string year = date.Substring(6, 4);
                if (month[0] == '0')
                    month = month[1].ToString();
                if (month == DateTime.Now.Month.ToString() && year == DateTime.Now.Year.ToString())
                    numOfOrdersThisMonth++;
            }
            return numOfOrdersThisMonth;
        }

        //public int getNumberOfCustomers()
        //{
        //    string CustomerList;
        //    HttpResponseMessage response = client.GetAsync("https://localhost:44393/api/Customers").Result;
        //    CustomerList = response.Content.ReadAsStringAsync().Result;
        //    List<Customer> allCustomer = JsonConvert.DeserializeObject<IEnumerable<Customer>>(CustomerList).ToList();
        //    int numOfCustomersThisMonth = 0;
        //    foreach (var item in allCustomer)
        //    {
        //        item.
        //    }
        //    return numOfCustomersThisMonth;
        //}

    }
}
