using Microsoft.IdentityModel.Tokens;
using PROG2500_A2_Chinook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG2500_A2_Chinook.Models
{
    public partial class Invoice
    {
        public int NumOfItems
        {
            get
            {
                ChinookContext _context = new ChinookContext();
                IQueryable InvoiceLines_Queryable = _context.InvoiceLines.Where(invoice => invoice.InvoiceId == InvoiceId);

                foreach (var line in InvoiceLines_Queryable)
                {
                    InvoiceLines.Add((InvoiceLine)line);
                }

                return InvoiceLines.Count;
            }
        }
    }
}
