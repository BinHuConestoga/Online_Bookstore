< Window x: Class = "BookstoreApp.AdminWindow"
        xmlns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns: x = "http://schemas.microsoft.com/winfx/2006/xaml"
        Title = "Admin Window" Height = "450" Width = "800" >
    < Grid >
        < TextBox x: Name = "BookTitleTextBox" Width = "200" Height = "30" Margin = "10,10,0,0" PlaceholderText = "Title" />
        < TextBox x: Name = "BookAuthorTextBox" Width = "200" Height = "30" Margin = "220,10,0,0" PlaceholderText = "Author" />
        < TextBox x: Name = "BookDescriptionTextBox" Width = "400" Height = "30" Margin = "10,50,0,0" PlaceholderText = "Description" />
        < TextBox x: Name = "BookPriceTextBox" Width = "200" Height = "30" Margin = "10,90,0,0" PlaceholderText = "Price" />
        < TextBox x: Name = "BookCategoryTextBox" Width = "200" Height = "30" Margin = "220,90,0,0" PlaceholderText = "Category" />
        < Button Content = "Select Picture" Width = "200" Height = "30" Margin = "430,90,0,0" Click = "SelectPictureButton_Click" />
        < TextBox x: Name = "BookAvailabilityTextBox" Width = "200" Height = "30" Margin = "10,130,0,0" PlaceholderText = "Availability" />
        < Button Content = "Save" Width = "100" Height = "30" Margin = "10,170,0,0" Click = "SaveButton_Click" />
        < Button Content = "Cancel" Width = "100" Height = "30" Margin = "120,170,0,0" Click = "CancelButton_Click" />
        < Image x: Name = "BookPictureImage" Width = "100" Height = "100" Margin = "10,210,0,0" />
    </ Grid >
</ Window >
