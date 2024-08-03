< Window x: Class = "BookstoreApp.AdminDashboard"
        xmlns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns: x = "http://schemas.microsoft.com/winfx/2006/xaml"
        Title = "Admin Dashboard" Height = "450" Width = "800" >
    < Grid >
        < TabControl >
            < TabItem Header = "Inventory" >
                < ListBox x: Name = "InventoryListBox" Margin = "10" />
            </ TabItem >
            < TabItem Header = "Sales Reports" >
                < TextBlock x: Name = "SalesReportsTextBlock" Margin = "10" />
            </ TabItem >
            < TabItem Header = "Customer Trends" >
                < TextBlock x: Name = "CustomerTrendsTextBlock" Margin = "10" />
            </ TabItem >
        </ TabControl >
    </ Grid >
</ Window >
