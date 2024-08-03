using System;
using System.IO;
using System.Xml.Serialization;

[XmlRoot("Books")]
public class BookCollection
{
    [XmlElement("Book")]
    public List<Book> Books { get; set; } = new List<Book>();
}
public class Book
{
    public int BookId { get; set; } // Unique Identifier
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public int Availability { get; set; }
    public byte[] Picture { get; set; }


}