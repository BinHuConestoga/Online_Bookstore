namespace OnlineBookstore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public List<Book> Books { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
    }
}
