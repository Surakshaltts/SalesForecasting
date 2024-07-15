using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastingSales.Models
{
    public class SalesData
    {
        public string State { get; set; }
        public decimal Sales { get; set; }
        public decimal PercentageIncrease { get; set; }
        public decimal SalesForecasted { get; set; }
    }
}
