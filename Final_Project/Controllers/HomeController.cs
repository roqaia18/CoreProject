using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Project.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Project.Controllers.ProductsController;

namespace Final_Project.Controllers
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
            //getMostProductSaled('m');
            return View( new HomeController(_logger) );
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
        public int getNumberOfOrdersChart(int _month)
        {
            string OrderList;
            HttpResponseMessage response = client.GetAsync("https://localhost:44329/api/Order").Result;
            OrderList = response.Content.ReadAsStringAsync().Result;
            List<Order> allOrder = JsonConvert.DeserializeObject<IEnumerable<Order>>(OrderList).ToList();
            int numOfOrdersThisPeriodChart = 0;

                foreach (var item in allOrder)
                {
                    if (item != null)
                    {
                        DateTime date = item.OrderDate;
                        int month = date.Month;
                        int year = date.Year;
                    int Month = DateTime.Now.Month - _month;
                    int Year = DateTime.Now.Year;
                    if (Month < 1)
                    {
                        Month += 12;
                        Year -= 1;
                    }
                    if (month == Month && year == Year)
                       numOfOrdersThisPeriodChart++;
                    }
                }
            return numOfOrdersThisPeriodChart;
            }
            public int getNumberOfOrders(char periodOfTime)
              {
            string OrderList;
            HttpResponseMessage response = client.GetAsync("https://localhost:44329/api/Order").Result;
            OrderList = response.Content.ReadAsStringAsync().Result;
            List<Order> allOrder = JsonConvert.DeserializeObject<IEnumerable<Order>>(OrderList).ToList();
            int numOfOrdersThisPeriod = 0;

            if (periodOfTime.ToString().ToLower() == "m")
            {
                foreach (var item in allOrder)
                {
                    if (item != null)
                    {
                        DateTime date = item.OrderDate;
                        string month = date.Month.ToString();
                        string year = date.Year.ToString();
                        if (month[0] == '0')
                            month = month[1].ToString();
                        if (month == DateTime.Now.Month.ToString() && year == DateTime.Now.Year.ToString())
                            numOfOrdersThisPeriod++;
                    }
                }
            }
            else if (periodOfTime.ToString().ToLower() == "y")
            {
                foreach (var item in allOrder)
                {
                    DateTime date = item.OrderDate;
                    string month = date.Month.ToString();
                    string year = date.Year.ToString();
                    if (month[0] == '0')
                        month = month[1].ToString();
                    if (year == DateTime.Now.Year.ToString())
                        numOfOrdersThisPeriod++;
                }
            }
            else if (periodOfTime.ToString().ToLower() == "w")
            {
                foreach (var item in allOrder)
                {
                    DateTime date = item.OrderDate;
                    string dayInString = date.Day.ToString();
                    if (dayInString[0] == '0')
                        dayInString = dayInString[1].ToString();

                    int day = int.Parse(dayInString);
                    string month = date.Month.ToString();
                    string year = date.Year.ToString();
                    if (month[0] == '0')
                        month = month[1].ToString();

                    int lengthOfWeekFromNow = DateTime.Now.Day - 7;
                    if (month == DateTime.Now.Month.ToString() && year == DateTime.Now.Year.ToString() && lengthOfWeekFromNow <= day && day <= DateTime.Now.Day)
                        numOfOrdersThisPeriod++;
                }
            }


            else if (periodOfTime.ToString().ToLower() == "a")
            {
                foreach (var item in allOrder)
                {
                    numOfOrdersThisPeriod++;
                }
            }
            return numOfOrdersThisPeriod;
        }
        public int getNumberOfUsersJoinedChart(int _month)
        {
            string Users;
            HttpResponseMessage response = client.GetAsync("https://localhost:44329/api/user").Result;
            Users = response.Content.ReadAsStringAsync().Result;
            List<CustomIdentityUser> allUsers = JsonConvert.DeserializeObject<IEnumerable<CustomIdentityUser>>(Users).ToList();
            int numOfOrdersThisPeriodChart = 0;
            foreach (var item in allUsers)
            {
                if (item != null)
                {
                    DateTime date = item.DateOfJoin;
                    int month = date.Month;
                    int year = date.Year;
                    int Month = DateTime.Now.Month - _month;
                    int Year = DateTime.Now.Year;
                    if (Month < 1)
                    {
                        Month += 12;
                        Year -= 1;
                    }
                    if (month == Month && year == Year)
                        numOfOrdersThisPeriodChart++;
                }
            }
            return numOfOrdersThisPeriodChart;
        }
        public int getNumberOfUsersJoined(char periodOfTime)
        {
            string Users;
            HttpResponseMessage response = client.GetAsync("https://localhost:44329/api/user").Result;
            Users = response.Content.ReadAsStringAsync().Result;
            List<CustomIdentityUser> allUsers = JsonConvert.DeserializeObject<IEnumerable<CustomIdentityUser>>(Users).ToList();
            int numOfOrdersThisPeriod = 0;

            if (periodOfTime.ToString().ToLower() == "m")
            {
                foreach (var item in allUsers)
                {
                    if (item != null)
                    {
                        DateTime date = item.DateOfJoin;
                        string month = date.Month.ToString();
                        string year = date.Year.ToString();
                        if (month[0] == '0')
                            month = month[1].ToString();
                        if (month == DateTime.Now.Month.ToString() && year == DateTime.Now.Year.ToString())
                            numOfOrdersThisPeriod++;
                    }
                }
            }
            else if (periodOfTime.ToString().ToLower() == "y")
            {
                foreach (var item in allUsers)
                {
                    DateTime date = item.DateOfJoin;
                    string month = date.Month.ToString();
                    string year = date.Year.ToString();
                    if (month[0] == '0')
                        month = month[1].ToString();
                    if (year == DateTime.Now.Year.ToString())
                        numOfOrdersThisPeriod++;
                }
            }
            else if (periodOfTime.ToString().ToLower() == "w")
            {
                foreach (var item in allUsers)
                {
                    DateTime date = item.DateOfJoin;

                    string dayInString = date.Day.ToString();
                    if (dayInString[0] == '0')
                        dayInString = dayInString[1].ToString();

                    int day = int.Parse(dayInString);
                    string month = date.Month.ToString();
                    string year = date.Year.ToString();
                    if (month[0] == '0')
                        month = month[1].ToString();

                    int lengthOfWeekFromNow = DateTime.Now.Day - 7;
                    if (month == DateTime.Now.Month.ToString() && year == DateTime.Now.Year.ToString() && lengthOfWeekFromNow <= day && day <= DateTime.Now.Day)
                        numOfOrdersThisPeriod++;
                }
            }
            else if (periodOfTime.ToString().ToLower() == "a")
            {
                {
                    foreach (var item in allUsers)
                    {
                        numOfOrdersThisPeriod++;
                    }
                }
            }
                return numOfOrdersThisPeriod;
        }

        public string getMostProductSaled(char periodOfTime)
        {


            //Order order = new Order();
            //order.ID = 5;
            //order.Ammount = 1;
            //order.Email = "mahmoud@gmail.com";
            //order.Address = "Egypt";
            //order.City = "Cairo";
            //order.Phone = "01125459218";
            //order.Cost = 30;
            //order.User.Id = "616579d8-c680-4deb-b894-51dab3730bbf";
            //order.OrderDate = DateTime.Now;
            //order.Products.Add
            //string ProudctInString = JsonConvert.SerializeObject(order);
            //StringContent requestBody = new StringContent(ProudctInString, Encoding.UTF8, "application/json");
            //HttpResponseMessage respMessage = client.PostAsync("https://localhost:44329/api/order", requestBody).Result;


            string OrderList;
            HttpResponseMessage response = client.GetAsync("https://localhost:44329/api/Order").Result;
            OrderList = response.Content.ReadAsStringAsync().Result;
            List<Order> allOrder = JsonConvert.DeserializeObject<IEnumerable<Order>>(OrderList).ToList();
            //int numOfOrdersThisPeriod = 0;
            Dictionary<string, int> MostProductSaledList = new Dictionary<string, int>();

            if (periodOfTime.ToString().ToLower() == "m")
            {
                foreach (var item in allOrder)
                {
                    if (item != null)
                    {
                        DateTime date = item.OrderDate;
                        string month = date.Month.ToString();
                        string year = date.Year.ToString();
                        if (month[0] == '0')
                            month = month[1].ToString();
                        if (month == DateTime.Now.Month.ToString() && year == DateTime.Now.Year.ToString())
                        {
                            foreach (var Product in item.Items)
                            {
                                if (Product != null)
                                {
                                    if (MostProductSaledList.ContainsKey(Product.Product.Name))
                                        MostProductSaledList[Product.Product.Name]++;

                                    else
                                        MostProductSaledList.Add(Product.Product.Name, 1);
                                }
                            }
                        }
                    }

                }
            }
            else if (periodOfTime.ToString().ToLower() == "y")
            {
                foreach (var item in allOrder)
                {
                    if (item != null)
                    {
                        DateTime date = item.OrderDate;
                        string month = date.Month.ToString();
                        string year = date.Year.ToString();
                        if (month[0] == '0')
                            month = month[1].ToString();
                        if (year == DateTime.Now.Year.ToString())
                        {
                            foreach (var Product in item.Items)
                            {
                                if (Product != null)
                                {
                                    if (MostProductSaledList.ContainsKey(Product.Product.Name))
                                        MostProductSaledList[Product.Product.Name]++;

                                    else
                                        MostProductSaledList.Add(Product.Product.Name, 1);
                                }
                            }
                        }
                    }
                }
            }
            else if (periodOfTime.ToString().ToLower() == "w")
            {
                foreach (var item in allOrder)
                {
                    if (item != null)
                    {
                        DateTime date = item.OrderDate;
                        string dayInString = date.Day.ToString();
                        if (dayInString[0] == '0')
                            dayInString = dayInString[1].ToString();

                        int day = int.Parse(dayInString);
                        string month = date.Month.ToString();
                        string year = date.Year.ToString();
                        if (month[0] == '0')
                            month = month[1].ToString();

                        int lengthOfWeekFromNow = DateTime.Now.Day - 7;
                        if (month == DateTime.Now.Month.ToString() && year == DateTime.Now.Year.ToString() && lengthOfWeekFromNow <= day && day <= DateTime.Now.Day)
                        {
                            foreach (var Product in item.Items)
                            {
                                if (Product != null)
                                {
                                    if (MostProductSaledList.ContainsKey(Product.Product.Name))
                                        MostProductSaledList[Product.Product.Name]++;

                                    else
                                        MostProductSaledList.Add(Product.Product.Name, 1);
                                }
                            }
                        }
                    }
                }
            }
            else if (periodOfTime.ToString().ToLower() == "a")
            {
                foreach (var item in allOrder)
                {
                    foreach (var Product in item.Items)
                    {
                        if (Product != null)
                        {
                            if (MostProductSaledList.ContainsKey(Product.Product.Name))
                                MostProductSaledList[Product.Product.Name]++;

                            else
                                MostProductSaledList.Add(Product.Product.Name, 1);
                        }
                    }
                }
            }

            string MostSaledProduct = "";
            int BiggestNumber = 0;
            foreach (var item in MostProductSaledList)
            {
                if (BiggestNumber < item.Value)
                {
                    BiggestNumber = item.Value;
                    MostSaledProduct = item.Key;
                }
            }

            return MostSaledProduct;
        }



    }
}
