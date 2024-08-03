public static class UserManager
{
    private static User _currentUser;

    public static User GetCurrentUser()
    {
        return _currentUser;
    }

    public static void SetCurrentUser(User user)
    {
        _currentUser = user;
    }

    // Implement other user management methods as needed
}