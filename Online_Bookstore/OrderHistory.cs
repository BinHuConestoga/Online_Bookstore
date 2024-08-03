using System;
using System.Collections.Generic;

namespace BookstoreApp
{
    [Serializable]
    public class OrderHistory
    {
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}