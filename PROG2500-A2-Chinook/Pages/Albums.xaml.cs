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
    public partial class Albums : Page
    {
        ChinookContext _context = new ChinookContext();
        CollectionViewSource albumViewSource = new CollectionViewSource();

        public Albums()
        {
            InitializeComponent();

            //Tie the markup viewsource object to the code viewsource object
            albumViewSource = (CollectionViewSource)FindResource(nameof(albumViewSource));

            //Use the dbContext to tell EF to load the data we'll use on this page
            _context.Albums.Load();

            //Set the viewsource data source to use the album data collection
            albumViewSource.Source = _context.Albums.Local.ToObservableCollection();
        }

        private void btnAlbumSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = albumSearch.Text;

            // linq query expression
            var query = _context.Albums.Where(album => album.Title.Contains(searchTerm)).ToList();
            AlbumListView.ItemsSource = query;
        }
    }
}
