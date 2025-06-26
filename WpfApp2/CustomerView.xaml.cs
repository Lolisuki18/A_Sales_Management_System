using BusinessObjects;
using Repositories;
using Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WpfApp2.Helpers; // Nếu RelayCommand nằm trong Helpers folder

namespace WpfApp2
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        private readonly ICustomerService customerService = new CustomerService(new CustomerRepository());

        private ObservableCollection<Customer> customers;
        private string searchText = "";

        public ObservableCollection<Customer> Customers
        {
            get => customers;
            set { customers = value; OnPropertyChanged(); }
        }

        public string SearchText
        {
            get => searchText;
            set { searchText = value; OnPropertyChanged(); Search(); }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public CustomerViewModel()
        {
            AddCommand = new RelayCommand(_ => AddCustomer());
            EditCommand = new RelayCommand(c => EditCustomer(c as Customer));
            DeleteCommand = new RelayCommand(c => DeleteCustomer(c as Customer));
            MessageBox.Show("CustomerViewModel Loaded");
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            var list = customerService.GetCustomers();
            MessageBox.Show($"Số lượng khách hàng: {list.Count}");
            Customers = new ObservableCollection<Customer>(list);
        }


        private void AddCustomer()
        {
            // TODO: mở popup thêm
            MessageBox.Show("Thêm khách hàng");
        }

        private void EditCustomer(Customer? customer)
        {
            if (customer != null)
            {
                // TODO: mở popup sửa
                MessageBox.Show($"Sửa khách hàng: {customer.CompanyName}");
            }
        }

        private void DeleteCustomer(Customer? customer)
        {
            if (customer != null)
            {
                var result = MessageBox.Show($"Bạn có chắc muốn xoá khách hàng {customer.CompanyName}?", "Xác nhận xoá", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    if (customerService.DeleteCustomer(customer))
                    {
                        Customers.Remove(customer);
                    }
                    else
                    {
                        MessageBox.Show("Xoá thất bại.");
                    }
                }
            }
        }

        private void Search()
        {
            var all = customerService.GetCustomers();
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                all = all.Where(c => c.CompanyName.Contains(SearchText) || c.Phone.Contains(SearchText)).ToList();
            }
            Customers = new ObservableCollection<Customer>(all);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
