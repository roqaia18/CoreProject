using Final_Project.Helpers;
using Final_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Project.Controllers.CustomersController;
using static Project.Controllers.ProductsController;

namespace Project.Controllers
{

    public class Order
    {

        public int ID { get; set; }
        [Required]
        public int Ammount { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public string City { get; set; }
        [Required]
        public string Phone { get; set; }
        public bool Confirm { get; set; } = false;
        public long Cost { get; set; }
        [ForeignKey("User")]
        public string User_Id { get; set; }

        //navigation properties
        public virtual CustomIdentityUser User { get; set; }
        public virtual List<Item> Items { get; set; }
    }
    public class Item
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
    }

    [Authorize]
    public class OrdersController : Controller
    {

        HttpClient client = new HttpClient();

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            //IEnumerable<Order> OrderList;
            string OrderList;
            HttpResponseMessage response = client.GetAsync("https://localhost:44329/api/Order").Result;
            OrderList = response.Content.ReadAsStringAsync().Result;
            List<Order> allOrder = JsonConvert.DeserializeObject<IEnumerable<Order>>(OrderList).ToList();
            return View(allOrder);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult EachProduct(int id)
        {
            string Order;
            HttpResponseMessage response = client.GetAsync("https://localhost:44329/api/Order/" + id).Result;
            Order = response.Content.ReadAsStringAsync().Result;
            ViewBag.CustomerName = JsonConvert.DeserializeObject<Order>(Order).User.UserName;
            ViewBag.allProducts = JsonConvert.DeserializeObject<Order>(Order).Items;
            return View();
        }
        [Authorize(Roles = "Customer")]
        public IActionResult Create()
        {
            return View(new Order());
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public IActionResult Create(Order order)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            // HttpResponseMessage response = client.GetAsync("https://localhost:44329/api/user/"+userId).Result;
            // CustomIdentityUser Customer = JsonConvert.DeserializeObject<CustomIdentityUser>(response.Content.ReadAsStringAsync().Result);
            order.User_Id = userId;
            order.Items = GetItems();
            HttpResponseMessage res = client.PostAsJsonAsync("https://localhost:44329/api/Order", order).Result;
            TempData["SuccessMessage"] = "Order Add Successfully";
            return RedirectToAction("Index", "HomeUser", null);
        }
        public List<Item> GetItems() {
            return SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            HttpResponseMessage Response = client.GetAsync("https://localhost:44329/api/Order/" + id.ToString()).Result;
            string OrderEdit = Response.Content.ReadAsStringAsync().Result;
            object OrderEditOBJ = JsonConvert.DeserializeObject<Order>(OrderEdit);
            ViewBag.Order = OrderEditOBJ;
            return View(OrderEditOBJ);

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Order order)
        {
            HttpResponseMessage Response = client.GetAsync("https://localhost:44329/api/Order/" + order.ID.ToString()).Result;
            string OrderEdit = Response.Content.ReadAsStringAsync().Result;
            Order OrderEditOBJ = JsonConvert.DeserializeObject<Order>(OrderEdit);
            order.Ammount = OrderEditOBJ.Ammount;
            order.Email = OrderEditOBJ.Email;
            order.Address = OrderEditOBJ.Address;
            order.City = OrderEditOBJ.City;
            order.Cost = OrderEditOBJ.Cost;
            order.Phone = OrderEditOBJ.Phone;
            order.User_Id = OrderEditOBJ.User_Id;
            order.User = OrderEditOBJ.User;
            string ProductList;
            HttpResponseMessage response2 = client.GetAsync("https://localhost:44329/api/product").Result;
            ProductList = response2.Content.ReadAsStringAsync().Result;
            List<Product> allProducts = JsonConvert.DeserializeObject<IList<Product>>(ProductList).ToList();
            if (order.Confirm == true)
            {
                foreach (var item in OrderEditOBJ.Items)
                {
                    foreach (var Product in allProducts)
                    {
                        if (item.Product.ID == Product.ID)
                        {
                            Product.Stock -= item.Quantity;
                            string SerializedObject = JsonConvert.SerializeObject(Product);
                            StringContent requestBody = new StringContent(SerializedObject, Encoding.UTF8, "application/json");
                            HttpResponseMessage responseMessage = client.PutAsync("https://localhost:44329/api/product/" + Product.ID, requestBody).Result;
                        }
                    }
                }
                #region Send e-mail confirm mail
                DateTime date = DateTime.Now;
                DateTime date2= date.AddDays(7);
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("shipped2uteam@gmail.com");
                msg.To.Add(order.Email);
                msg.Subject = "Shipped2U";
                msg.Body = $"Dear {order.User.FirstName} {order.User.LastName}," +
                $"\nThis Mail confirms your order at {order.OrderDate.ToShortDateString().ToString()} and the order will be delivered in {date2.ToShortDateString().ToString()}" +
                    $"\nShipped2U App Team,\nRegards.";

                //msg.Attachments.Add(new Attachment("path"));

                SmtpClient smt = new SmtpClient();

                smt.Host = "smtp.gmail.com";
                System.Net.NetworkCredential ntcd = new NetworkCredential();
                smt.UseDefaultCredentials = false;
                ntcd.UserName = "shipped2uteam@gmail.com";
                ntcd.Password = "Team.2020";
                smt.Credentials = ntcd;
                smt.EnableSsl = true;
                smt.Port = 587;
                smt.Send(msg);


                #endregion

            }
            HttpResponseMessage response = client.PutAsJsonAsync("https://localhost:44329/api/Order/" + order.ID, order).Result;
            TempData["SuccessMessage"] = "Order Cofirm Successfully";
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            HttpResponseMessage Response = client.GetAsync("https://localhost:44329/api/Order/" + id.ToString()).Result;
            string OrderDelete = Response.Content.ReadAsStringAsync().Result;
            Order OrderDeleteOBJ = JsonConvert.DeserializeObject<Order>(OrderDelete);
            HttpResponseMessage response = client.DeleteAsync("https://localhost:44329/api/Order/" + id.ToString()).Result;
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("shipped2uteam@gmail.com");
            msg.To.Add(OrderDeleteOBJ.Email);
            msg.Subject = "Shipped2U";
            msg.Body = $"Dear {OrderDeleteOBJ.User.FirstName} {OrderDeleteOBJ.User.LastName}," +
            $"\nYour order at {OrderDeleteOBJ.OrderDate.ToShortDateString().ToString()} has been canceled for some reasons,/n Please try another order" +
                $"\nShipped2U App Team,\nRegards.";

            //msg.Attachments.Add(new Attachment("path"));

            SmtpClient smt = new SmtpClient();

            smt.Host = "smtp.gmail.com";
            System.Net.NetworkCredential ntcd = new NetworkCredential();
            smt.UseDefaultCredentials = false;
            ntcd.UserName = "shipped2uteam@gmail.com";
            ntcd.Password = "Team.2020";
            smt.Credentials = ntcd;
            smt.EnableSsl = true;
            smt.Port = 587;
            smt.Send(msg);


            TempData["SuccessMessage"] = "Order Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
