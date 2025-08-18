using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace api.Models
{
    public class Portfolio
    {
        public string AppUserId { get; set; } = string.Empty;
        public int StockId { get; set; }
        public AppUser AppUser { get; set; } = new AppUser();
        public Stock Stock { get; set; } = new Stock();
    }
}