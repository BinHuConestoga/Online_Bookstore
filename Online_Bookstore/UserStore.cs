using System.Collections.Generic;
using System.Linq;

namespace BookstoreApp
{
    public static class UserStore
    {
        // In-memory list to store users
        private static readonly List<User> Users = new List<User>();

        // Method to authenticate users (for login)
        public static User AuthenticateUser(string username, string password)
        {
            return Users.FirstOrDefault(user => user.UserName == username && user.Password == password);
        }

        // Method to save a new user
        public static void SaveUser(User user)
        {
            Users.Add(user);
        }

        // Method to check if a user already exists
        public static bool UserExists(string username)
        {
            return Users.Any(user => user.UserName == username);
        }
    }
}