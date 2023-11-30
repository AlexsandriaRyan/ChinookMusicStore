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
    public partial class Tracks : Page
    {
        ChinookContext _context = new ChinookContext();
        CollectionViewSource trackViewSource = new CollectionViewSource();

        public Tracks()
        {
            InitializeComponent();

            //Tie the markup viewsource object to the code viewsource object
            trackViewSource = (CollectionViewSource)FindResource(nameof(trackViewSource));

            //Use the dbContext to tell EF to load the data we'll use on this page
            _context.Tracks.Load();

            //Set the viewsource data source to use the album data collection
            trackViewSource.Source = _context.Tracks.Local.ToObservableCollection();
        }

        private void btnTrackSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = trackSearch.Text;

            // linq query expression
            var query = _context.Tracks.Where(track => track.Name.Contains(searchTerm)).ToList();
            TrackListView.ItemsSource = query;
        }
    }
}