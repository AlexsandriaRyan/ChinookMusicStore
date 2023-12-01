using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG2500_A2_Chinook.Models
{
    public partial class Track
    {
        public string? Minutes
        {
            get
            {
                TimeSpan convert = TimeSpan.FromMilliseconds(this.Milliseconds);
                string minutes = string.Format("{0:D2}m:{1:D2}s",
                    convert.Minutes,
                    convert.Seconds);

                return minutes;
            }
        }

        public string? Megabytes
        {
            get
            {
                double kilo = (double)this.Bytes / 1024;
                double mega = kilo / 1024;
                string megabytes = string.Format("{0:0.##} MB", mega);

                return megabytes;
            }
        }

        public string? Price
        {
            get { return "$" + this.UnitPrice; }
        }

        public string? Artist
        {
            get
            {
                return String.IsNullOrEmpty(this.Composer) ? "N/A" : this.Composer;
            }
        }
    }
}