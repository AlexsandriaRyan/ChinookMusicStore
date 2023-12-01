using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG2500_A2_Chinook.Models
{
    public partial class Customer
    {
        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        public string? CityState
        {
            get
            {
                return String.IsNullOrEmpty(this.State) ? this.City : this.City + ", " + this.State;
            }
        }

    }
}
