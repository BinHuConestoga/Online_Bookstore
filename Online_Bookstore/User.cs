using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;


[XmlRoot("Users")]
public class UserCollection
{
    [XmlElement("User")]
    public List<User> Users { get; set; } = new List<User>();
}


public class User
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string HomeAddress { get; set; }
    public Role UserRole { get; set; }
}