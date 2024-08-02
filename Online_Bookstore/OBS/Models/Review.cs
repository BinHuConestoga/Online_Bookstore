namespace OnlineBookstore.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int BookId { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
    }
}
