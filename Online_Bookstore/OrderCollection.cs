using System;
using System.Collections.ObjectModel;

namespace BookstoreApp
{
    [Serializable]
    public class Order
    {
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public DateTime OrderDate { get; set; }
        public ObservableCollection<OrderItem> OrderItems { get; set; } = new ObservableCollection<OrderItem>();
    }

    [Serializable]
    public class OrderItem
    {
        public string BookTitle { get; set; }
        public int Quantity { get; set; }
    }

    [Serializable]
    public class OrderCollection
    {
        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();
    }
}