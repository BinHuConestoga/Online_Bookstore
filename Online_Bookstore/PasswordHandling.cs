public partial class AdminMainWindow : Window
{
    private int currentPage = 1;
    private const int PageSize = 9;

    private void LoadBooks(string sortField = "Title", bool ascending = true)
    {
        var booksQuery = AdminWindow.Books.AsQueryable();

        if (ascending)
        {
            booksQuery = booksQuery.OrderBy(b => EF.Property<object>(b, sortField));
        }
        else
        {
            booksQuery = booksQuery.OrderByDescending(b => EF.Property<object>(b, sortField));
        }

        var books = booksQuery.Skip((currentPage - 1) * PageSize).Take(PageSize).ToList();
        BooksListBox.ItemsSource = books;
    }

    private void NextPageButton_Click(object sender, RoutedEventArgs e)
    {
        currentPage++;
        LoadBooks();
    }

    private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
    {
        if (currentPage > 1)
        {
            currentPage--;
            LoadBooks();
        }
    }

    private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedSort = (sender as ComboBox).SelectedItem as string;
        LoadBooks(sortField: selectedSort);
    }
}
