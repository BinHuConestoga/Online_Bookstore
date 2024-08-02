< Window x: Class = "BookstoreApp.BookDetails"
        xmlns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns: x = "http://schemas.microsoft.com/winfx/2006/xaml"
        Title = "Book Details" Height = "450" Width = "800" >
    < Grid >
        < TextBlock x: Name = "TitleTextBlock" Width = "760" Height = "30" Margin = "10,10,10,0" />
        < TextBlock x: Name = "AuthorTextBlock" Width = "760" Height = "30" Margin = "10,50,10,0" />
        < TextBlock x: Name = "PriceTextBlock" Width = "760" Height = "30" Margin = "10,90,10,0" />
        < TextBlock x: Name = "CategoryTextBlock" Width = "760" Height = "30" Margin = "10,130,10,0" />
        < TextBlock x: Name = "DescriptionTextBlock" Width = "760" Height = "100" Margin = "10,170,10,0" TextWrapping = "Wrap" />
        < Image x: Name = "BookPictureImage" Width = "200" Height = "200" Margin = "10,280,0,0" />
    </ Grid >
</ Window >
