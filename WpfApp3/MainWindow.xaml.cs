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

namespace LeNguyenAnNinhWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            // Ví dụ: kiểm tra đơn giản với Admin
            if (username == "customer" && password == "123")
            {
                var customerWindown = new CustomerWindown(); // hoặc CustomerView, tùy role
                customerWindown.Show();
                this.Close(); // đóng login
            }
            else
            {
                MessageBox.Show("Invalid credentials");
            }
        }

    }
}