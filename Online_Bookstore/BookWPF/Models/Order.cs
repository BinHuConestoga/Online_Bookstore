using System;
using System.Collections.Generic;

namespace OnlineBookstore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public User Customer { get; set; }
        public List<Book> Books { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } // "Pending", "Shipped", "Delivered"
    }
}
