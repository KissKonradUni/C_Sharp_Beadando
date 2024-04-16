using System;
using System.Diagnostics;
using System.IO;
// ReSharper disable CommentTypo

namespace CNY8MP_Project
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var program = new Program();
            program.Run();
        }

        private readonly StockManager _stockManager = new StockManager();
        
        private void Run()
        {
            _stockManager.AddStock(new Stock("Apple Inc.", "AAPL", 1000.0));
            _stockManager.AddStock(new Stock("Microsoft Corporation", "MSFT", 2000.0));
            _stockManager.AddStock(new Stock("Amazon.com Inc.", "AMZN", 3000.0));
            _stockManager.AddStock(new Stock("Alphabet Inc.", "GOOGL", 4000.0));
            _stockManager.AddStock(new Stock("Meta Platforms Inc.", "META", 5000.0));
            _stockManager.AddStock(new Stock("Tesla Inc.", "TSLA", 6000.0));
            _stockManager.AddStock(new Stock("Bank of America Corporation", "BAC", 2500.0));
            _stockManager.AddStock(new Stock("JPMorgan Chase & Co.", "JPM", 3200.0));
            _stockManager.AddStock(new Stock("Johnson & Johnson", "JNJ", 4100.0));
            _stockManager.AddStock(new Stock("Berkshire Hathaway Inc.", "BRK.A", 5200.0));
            _stockManager.AddStock(new Stock("NVIDIA Corporation", "NVDA", 2800.0));
            _stockManager.AddStock(new Stock("Broadcom Inc.", "AVGO", 3500.0));
            _stockManager.AddStock(new Stock("Cisco Systems Inc.", "CSCO", 2300.0));
            _stockManager.AddStock(new Stock("Intel Corporation", "INTC", 2900.0));
            _stockManager.AddStock(new Stock("PayPal Holdings Inc.", "PYPL", 4400.0));
            _stockManager.AddStock(new Stock("Abbott Laboratories", "ABT", 3700.0));
            _stockManager.AddStock(new Stock("Exxon Mobil Corporation", "XOM", 4200.0));
            _stockManager.AddStock(new Stock("The Walt Disney Company", "DIS", 5400.0));
            _stockManager.AddStock(new Stock("Merck & Co. Inc.", "MRK", 3100.0));
            _stockManager.AddStock(new Stock("Home Depot Inc.", "HD", 2700.0));

            var stopwatch = Stopwatch.StartNew();

            Console.WriteLine("Simulating for 1000 days in a single thread...\n");
            _stockManager.SimulateForDays(1000);
            Console.WriteLine(_stockManager);
            
            stopwatch.Stop();
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms\n\n");
            
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            
            stopwatch.Reset();
            stopwatch.Start();
            
            Console.WriteLine("Simulating for 1000 days in parallel...\n");
            _stockManager.SimulateForDaysInParallel(1000);
            Console.WriteLine(_stockManager);
            
            stopwatch.Stop();
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms\n\n");
            
            // A google stock CSVként az excelhez
            // var googleStock = _stockManager.GetStock("GOOGL");
            // File.WriteAllText("GOOGL.csv", "Date;Price;Change\n");
            // for (var i = 0; i < googleStock.PriceHistory.Length; i++)
            // {
            //     File.AppendAllText("GOOGL.csv", $"{i};{googleStock.PriceHistory[i]};{googleStock.ChangeHistory[i]}\n");
            // }
        }
    }
}