public partial class AdminDashboard : Window
{
    public AdminDashboard()
    {
        InitializeComponent();
        LoadInventory();
        LoadSalesReports();
    }

    private void LoadInventory()
    {
        InventoryListBox.ItemsSource = AdminWindow.Books.ToList();
    }

    private void LoadSalesReports()
    {
        // Implement your logic to fetch and display sales reports
    }
}
