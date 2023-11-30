using Microsoft.EntityFrameworkCore;
using PROG2500_A2_Chinook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PROG2500_A2_Chinook.Pages
{
    public partial class Orders : Page
    {
        ChinookContext _context = new ChinookContext();
        CollectionViewSource ordersViewSource = new CollectionViewSource();

        public Orders()
        {
            InitializeComponent();

            //Tie the markup viewsource object to the code viewsource object
            ordersViewSource = (CollectionViewSource)FindResource(nameof(ordersViewSource));

            //Use the dbContext to tell EF to load the data we'll use on this page
            _context.Customers.Load();
            _context.Invoices.Load();

            //Set the viewsource data source to use the album data collection
            ordersViewSource.Source = _context.Customers.Local.ToObservableCollection();
            ordersViewSource.Source = _context.Invoices.Local.ToObservableCollection();
        }

        private void btnOrderSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = orderSearch.Text;

            // linq query expression
            var query =
                from customer in _context.Customers
                where customer.FullName.Contains(searchTerm)
                select customer;

            OrderListView.Items.Clear();

            foreach (var customer in query)
            {
                OrderListView.Items.Add(customer);
            }
        }
    }
}
