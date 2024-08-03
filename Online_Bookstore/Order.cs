using System;
using System.Collections.Generic;

public class Order
{
    public int OrderId { get; set; }
    public string UserName { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}

public class OrderItem
{
    public string BookTitle { get; set; }
    public int Quantity { get; set; }
}