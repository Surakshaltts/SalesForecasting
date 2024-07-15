   using System;
   using System.Collections.Generic;
   using ForecastingSales.Database;
   using ForecastingSales.Models;
   using ForecastingSales.Services;

    namespace ForecastingSales
    {
        class Program
        {
            static void Main(string[] args)
            {
            string connectionString = "Server=DESKTOP-UKDE1JQ\\MSSQLSERVER01;Database=SalesForecastingDb;Trusted_Connection=True;TrustServerCertificate=Yes";
                var sqlHelper = new SqlHelper(connectionString);
                var salesService = new SalesService(sqlHelper);
            
                while (true)
                {
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1. Query total sales by year");
                    Console.WriteLine("2. Add increment to sales in percentage");
                    Console.WriteLine("3. Export sales data to CSV");
                    Console.WriteLine("4. Exit");

                    var choice = Console.ReadLine();

                    if (choice == "1")
                    {
                        Console.Write("Enter year: ");
                        int year = int.Parse(Console.ReadLine());

                        var totalSales = salesService.TotalSales(year);
                        Console.WriteLine($"Total Sales for {year}: {totalSales}");

                        var salesByState = salesService.SalesByState(year);
                        Console.WriteLine("Sales by state:");
                        foreach (var stateSales in salesByState)
                        {
                            Console.WriteLine($"{stateSales.Key}: {stateSales.Value}");
                        }
                    }
                    else if (choice == "2")
                    {
                        Console.Write("Enter year: ");
                        int year = int.Parse(Console.ReadLine());

                        Console.Write("Enter percentage increment: ");
                        decimal percentage = decimal.Parse(Console.ReadLine());

                        var totalSales = salesService.TotalSales(year);
                        var ForecastedSales = salesService.ApplySalesIncrement(totalSales, percentage);

                        Console.WriteLine($"Sales for {year + 1} after {percentage}% increment: {ForecastedSales}");
                    }
                    else if (choice == "3")
                    {
                        Console.Write("Enter year: ");
                        int year = int.Parse(Console.ReadLine());

                        Console.Write("Enter percentage increment: ");
                        decimal percentage = decimal.Parse(Console.ReadLine());

                        var salesByState = salesService.SalesByState(year);
                        var salesData = new List<SalesData>();

                        foreach (var stateSales in salesByState)
                        {
                            var SalesForecasted = stateSales.Value * (1 + percentage / 100);
                            salesData.Add(new SalesData
                            {
                                State = stateSales.Key,
                                Sales = stateSales.Value,
                                PercentageIncrease = percentage,
                                SalesForecasted = SalesForecasted
                            });
                        }

                        Console.Write("Enter CSV file path: ");
                        string filePath = Console.ReadLine();
                        salesService.ExportToCsv(filePath, salesData);

                        Console.WriteLine("Data exported successfully.");
                    }
                    else if (choice == "4")
                    {
                        break;
                    }
                }
            }
        }
    }
