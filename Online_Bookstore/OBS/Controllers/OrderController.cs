using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Models;
using System.Collections.Generic;

namespace OnlineBookstore.Controllers
{
    public class OrderController : Controller
    {
        private static List<Order> orders = new List<Order>();

        public IActionResult Index()
        {
            // Display order history for the logged-in user
            var userOrders = orders; // Normally, you'd filter by the logged-in user's ID or username
            return View(userOrders);
        }

        public IActionResult Manage()
        {
            // Admin view for managing all orders
            return View(orders);
        }

        public IActionResult UpdateStatus(int orderId, string status)
        {
            var order = orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                order.Status = status;
            }
            return RedirectToAction(nameof(Manage));
        }
    }
}
