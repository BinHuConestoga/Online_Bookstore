using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Xml.Serialization;

public static class UserStore
{
    private static string filePath = "users.xml";

    public static void SaveUser(User newUser, string password)
    {
        try
        {
            // Generate a new salt and hash the password
            newUser.Salt = PasswordHelper.GenerateSalt(16);
            newUser.Password = PasswordHelper.HashPassword(password, newUser.Salt);

            UserCollection userCollection = LoadUserCollection();
            userCollection.Users.Add(newUser);

            var serializer = new XmlSerializer(typeof(UserCollection));
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, userCollection);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving user data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public static UserCollection LoadUserCollection()
    {
        if (File.Exists(filePath))
        {
            var serializer = new XmlSerializer(typeof(UserCollection));
            using (var reader = new StreamReader(filePath))
            {
                return (UserCollection)serializer.Deserialize(reader);
            }
        }
        return new UserCollection(); // Return an empty collection if file does not exist
    }

    public static bool UserExists(string username)
    {
        UserCollection userCollection = LoadUserCollection();
        return userCollection.Users.Exists(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
    }

    public static User AuthenticateUser(string username, string password)
    {
        UserCollection userCollection = LoadUserCollection();
        var user = userCollection.Users
            .FirstOrDefault(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));

        if (user != null)
        {
            // Hash the provided password with the stored salt and compare
            var hashedPassword = PasswordHelper.HashPassword(password, user.Salt);
            if (user.Password == hashedPassword)
            {
                return user;
            }
        }
        return null;
    }

    public static class PasswordHelper
    {
        // Generate a random salt
        public static string GenerateSalt(int size)
        {
            var rng = new RNGCryptoServiceProvider();
            byte[] saltBytes = new byte[size];
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        // Hash the password with salt
        public static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = password + salt;
                byte[] passwordBytes = Encoding.UTF8.GetBytes(saltedPassword);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}