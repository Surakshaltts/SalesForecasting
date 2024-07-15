using ForecastingSales.Database;
using ForecastingSales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastingSales.Services
{
    public class SalesService
    {
        private readonly SqlHelper _sqlHelper;

        public SalesService(SqlHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }

        public decimal TotalSales(int year)
        {
            string query = $"SELECT SUM(Sales) as TotalSales FROM Products p INNER JOIN Orders o ON p.OrderID = o.OrderID WHERE YEAR(o.OrderDate) = {year}";
            var result = _sqlHelper.ExecuteQuery(query);
            return result.Count > 0 && result[0]["TotalSales"] != null ? Convert.ToDecimal(result[0]["TotalSales"]) : 0;
        }

        public Dictionary<string, decimal> SalesByState(int year)
        {
            string query = $"SELECT o.State, SUM(p.Sales) as TotalSales FROM Products p INNER JOIN Orders o ON p.OrderID = o.OrderID WHERE YEAR(o.OrderDate) = {year} GROUP BY o.State";
            var result = _sqlHelper.ExecuteQuery(query);

            return result.ToDictionary(
                row => (string)row["State"],
                row => row["TotalSales"] != null ? Convert.ToDecimal(row["TotalSales"]) : 0
            );
        }

        public decimal ApplySalesIncrement(decimal totalSales, decimal percentage)
        {
            return totalSales * (1 + percentage / 100);
        }

        public Dictionary<string, decimal> SalesIncrementPerState(Dictionary<string, decimal> salesByState, Dictionary<string, decimal> increasedPercentage)
        {
            var ForecastedSalesByState = new Dictionary<string, decimal>();

            foreach (var stateSales in salesByState)
            {
                if (increasedPercentage.TryGetValue(stateSales.Key, out decimal percentageIncrease))
                {
                    ForecastedSalesByState[stateSales.Key] = stateSales.Value * (1 + percentageIncrease / 100);
                }
                else
                {
                    ForecastedSalesByState[stateSales.Key] = stateSales.Value;
                }
            }

            return ForecastedSalesByState;
        }

        public void ExportToCsv(string filePath, List<SalesData> salesData)
        {
            using (var writer = new System.IO.StreamWriter(filePath))
            {
                writer.WriteLine("State,Percentage Increase,Sales Value,Forecasted Sales");

                foreach (var data in salesData)
                {
                    writer.WriteLine($"{data.State},{data.PercentageIncrease},{data.Sales},{data.SalesForecasted}");
                }
            }
        }
    }
}
