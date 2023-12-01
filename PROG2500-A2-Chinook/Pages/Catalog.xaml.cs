using Microsoft.EntityFrameworkCore;
using PROG2500_A2_Chinook.Data;
using PROG2500_A2_Chinook.Models;
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
    public partial class Catalog : Page
    {
        ChinookContext _context = new ChinookContext();
        CollectionViewSource catalogViewSource = new CollectionViewSource();

        public Catalog()
        {
            InitializeComponent();

            //Tie the markup viewsource object to the code viewsource object
            catalogViewSource = (CollectionViewSource)FindResource(nameof(catalogViewSource));

            //Use the dbContext to tell EF to load the data we'll use on this page
            _context.Artists.Load();
            _context.Albums.Load();
            _context.Tracks.Load();

            //Set the viewsource data source to use the album data collection
            catalogViewSource.Source = _context.Artists.Local.ToObservableCollection();

            //Automatically display artists grouped by letter of artist name
            var query =
                from artist in _context.Artists
                group artist by artist.Name.ToUpper().Substring(0, 1) into newGroup
                select new
                {
                    Index = newGroup.Key,
                    artistCount = newGroup.Count().ToString(),
                    artist = newGroup.ToList<Artist>()
                };

            catalogViewSource.Source = query.ToList();
        }

        private void btnCatalogSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = catalogSearch.Text;

            // linq query expression
            var query =
                from artist in _context.Artists
                where artist.Name.Contains(searchTerm)
                group artist by artist.Name.ToUpper().Substring(0,1) into newGroup
                select new { Index = newGroup.Key,
                             artistCount = newGroup.Count().ToString(),
                             artist = newGroup.ToList<Artist>() };

            catalogViewSource.Source = query.ToList();
        }
    }
}
