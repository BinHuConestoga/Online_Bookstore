< Window x: Class = "Online_Bookstore.AdminMainWindow"
        xmlns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns: x = "http://schemas.microsoft.com/winfx/2006/xaml"
        Title = "Admin Main Window" Height = "450" Width = "800" >
    < Grid >
        < TextBox x: Name = "SearchTextBox" Width = "200" Height = "30" Margin = "10" />
        < Button Content = "Search" Width = "100" Height = "30" Margin = "220,10,0,0" Click = "SearchButton_Click" />
        < ComboBox x: Name = "SortComboBox" Width = "150" Height = "30" Margin = "330,10,0,0" SelectionChanged = "SortComboBox_SelectionChanged" >
            < ComboBoxItem Content = "Title" />
            < ComboBoxItem Content = "Author" />
            < ComboBoxItem Content = "Price" />
        </ ComboBox >
        < ListBox x: Name = "BooksListBox" Width = "760" Height = "300" Margin = "10,50,10,0" SelectionChanged = "BooksListBox_SelectionChanged" />
        < TextBlock x: Name = "BookDetailsTextBlock" Width = "760" Height = "100" Margin = "10,360,10,0" />
        < Image x: Name = "BookPictureImage" Width = "100" Height = "100" Margin = "10,470,0,0" />
        < Button Content = "Next Page" Width = "100" Height = "30" Margin = "120,470,0,0" Click = "NextPageButton_Click" />
        < Button Content = "Previous Page" Width = "100" Height = "30" Margin = "230,470,0,0" Click = "PreviousPageButton_Click" />
        < Button Content = "Add Book" Width = "100" Height = "30" Margin = "340,470,0,0" Click = "AddButton_Click" />
        < Button Content = "Remove Book" Width = "100" Height = "30" Margin = "450,470,0,0" Click = "RemoveButton_Click" />
        < Button Content = "Logout" Width = "100" Height = "30" Margin = "560,470,0,0" Click = "LogoutButton_Click" />
    </ Grid >
</ Window >
