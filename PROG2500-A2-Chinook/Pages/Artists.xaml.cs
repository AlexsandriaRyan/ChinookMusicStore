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
    public partial class Artists : Page
    {
        ChinookContext _context = new ChinookContext();
        CollectionViewSource artistViewSource = new CollectionViewSource();

        public Artists()
        {
            InitializeComponent();

            //Tie the markup viewsource object to the code viewsource object
            artistViewSource = (CollectionViewSource)FindResource(nameof(artistViewSource));

            //Use the dbContext to tell EF to load the data we'll use on this page
            _context.Artists.Load();

            //Set the viewsource data source to use the album data collection
            artistViewSource.Source = _context.Artists.Local.ToObservableCollection();
        }

        private void btnArtistSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = artistSearch.Text;

            // linq query expression
            var query = _context.Artists.Where(artist => artist.Name.Contains(searchTerm)).ToList();
            ArtistListView.ItemsSource = query;
        }
    }
}
