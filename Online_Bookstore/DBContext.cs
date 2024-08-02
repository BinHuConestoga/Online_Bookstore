public class Book
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Author { get; set; }

    [Required]
    public string Description { get; set; }

    [Range(0.01, 1000)]
    public decimal Price { get; set; }

    [Required]
    public string Category { get; set; }

    public byte[] Picture { get; set; }

    [Range(0, int.MaxValue)]
    public int Availability { get; set; }
}
