using BusinessObjects;
using System.Windows;

namespace LeNguyenAnNinhWpfApp
{
    public partial class CustomerDialog : Window
    {
        public Customer Customer { get; private set; }

        public CustomerDialog(Customer? customer = null)
        {
            InitializeComponent();
            if (customer != null)
            {
                Customer = new Customer
                {
                    CustomerId = customer.CustomerId,
                    CompanyName = customer.CompanyName,
                    ContactName = customer.ContactName,
                    ContactTitle = customer.ContactTitle,
                    Phone = customer.Phone,
                    Address = customer.Address
                };
                txtCompanyName.Text = customer.CompanyName;
                txtContactName.Text = customer.ContactName;
                txtContactTitle.Text = customer.ContactTitle;
                txtPhone.Text = customer.Phone;
                txtAddress.Text = customer.Address;
            }
            else
            {
                Customer = new Customer();
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            // Validate input if needed
            if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
            {
                MessageBox.Show("Company Name is required.");
                return;
            }
            Customer.CompanyName = txtCompanyName.Text.Trim();
            Customer.ContactName = txtContactName.Text.Trim();
            Customer.ContactTitle = txtContactTitle.Text.Trim();
            Customer.Phone = txtPhone.Text.Trim();
            Customer.Address = txtAddress.Text.Trim();

            DialogResult = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}