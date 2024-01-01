using System.Net.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;

namespace Library_wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private void Button_Click(object sender, RoutedEventArgs e) 
    {
        string text = textBox1.Text;
        List<Book> books = new List<Book>();
        Book.GetBooksAsync(books);
        string result = books[0].Name; //!
        MessageBox.Show(result);
        
    }
}