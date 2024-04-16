using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;

namespace CNY8MP_Project
{
    public class StockManager
    {
        private readonly OrderedDictionary _stocks = new OrderedDictionary();
        
        public void AddStock(Stock stock)
        {
            _stocks.Add(stock.Abbreviation, stock);
        }

        private Stock GetStock(string abbreviation)
        {
            return (Stock) _stocks[abbreviation];
        }
        
        public void UpdateStock(string abbreviation, double change)
        {
            var stock = GetStock(abbreviation);
            stock.UpdatePrice(change);
        }
        
        public void ClearStocks()
        {
            _stocks.Clear();
        }
        
        public void SimulateForDays(int days)
        {
            if (_stocks.Count == 0)
            {
                Console.Error.WriteLine("No stocks to simulate");
                return;
            }

            _stocks.Values
                .Cast<Stock>()
                .ToList()
                .ForEach(stock => stock.SimulatePriceChange(days));
        }
        
        public override string ToString()
        {
            var result = "";
            _stocks.Values
                .Cast<Stock>()
                .ToList()
                .ForEach(stock => result += stock + "\n" + stock.GetChangeHistory() + "\n");
            return result;
        }
        
        public string GetStocksByName()
        {
            var result = "";
            _stocks.Keys
                .Cast<string>()
                .ToList()
                .ForEach(abbreviation => result += abbreviation + " " + GetStock(abbreviation).Name + "\n");
            return result;
        }

        public void SimulateForDaysInParallel(int days)
        {
            if (_stocks.Count == 0)
            {
                Console.Error.WriteLine("No stocks to simulate");
                return;
            }

            var threads = new List<Thread>();
            _stocks.Values
                .Cast<Stock>()
                .ToList()
                .ForEach(stock =>
                {
                    var thread = new Thread(() => stock.SimulatePriceChange(days));
                    thread.Start();
                    threads.Add(thread);
                });
            
            while (threads.Any(thread => thread.ThreadState == ThreadState.Running))
            {
                Thread.Sleep(50);
            }
        }
    }
}