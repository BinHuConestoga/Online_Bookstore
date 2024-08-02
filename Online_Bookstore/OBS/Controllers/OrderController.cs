using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBookstore.Controllers
{
    public class OrderController : Controller
    {
        // Mock list to store orders
        private static List<Order> orders = new List<Order>();

        // Display list of orders (for admin or user order history)
        public IActionResult Index()
        {
            // Here, you can filter orders by the current user if needed
            return View(orders);
        }

        // Display the current cart for order review before placing the order
        public IActionResult ReviewOrder()
        {
            var cartItems = CartRepository.GetCartItems();
            return View(cartItems);
        }

        // Place the order based on the current cart items
        public IActionResult PlaceOrder()
        {
            var cartItems = CartRepository.GetCartItems();
            if (cartItems.Count == 0)
            {
                return RedirectToAction("Index", "Books");
            }

            // Create a new order with the cart items
            var newOrder = new Order
            {
                Id = orders.Count + 1,
                Items = cartItems,
                Status = "Processing",
                OrderDate = System.DateTime.Now
            };

            // Add the order to the mock order list
            orders.Add(newOrder);

            // Clear the cart after placing the order
            CartRepository.ClearCart();

            // Redirect to the order confirmation page
            return RedirectToAction("OrderConfirmation", new { orderId = newOrder.Id });
        }

        // Display order confirmation details
        public IActionResult OrderConfirmation(int orderId)
        {
            var order = orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // Optional: Admin function to update order status
        public IActionResult UpdateOrderStatus(int orderId, string status)
        {
            var order = orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                order.Status = status;
            }
            return RedirectToAction("Index");
        }
    }
}
