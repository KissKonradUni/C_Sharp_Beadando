using System;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable CommentTypo

namespace CNY8MP_Project
{
    public class Stock
    {
        public string Name { get; }
        public string Abbreviation { get; }
        private double Price { get; set; }

        private readonly List<double> _changeHistory = new List<double>();
        public double[] ChangeHistory => _changeHistory.ToArray();
        
        private readonly List<double> _priceHistory = new List<double>();
        public double[] PriceHistory => _priceHistory.ToArray();

        public Stock(string name, string abbreviation, double price)
        {
            Name = name;
            Abbreviation = abbreviation;
            Price = price;
        }
        
        public void UpdatePrice(double change)
        {
            _priceHistory.Add(Price);
            Price += change * 100;
            _changeHistory.Add(change);
        }
        
        public void SimulatePriceChange(int steps)
        {
            for (var i = 0; i < steps; i++)
            {
                var change = GenerateChange();
                UpdatePrice(change);
            }
        }

        private double GenerateChange()
        {
            var random = new Random();
            // Ha nincs elég adat, akkor a részvény ára véletlenszerűen változik
            if (_changeHistory.Count < 2)
                return random.NextDouble() * 0.2 - 0.1;
            // Ha a részvény az elmúlt 6 napban emelkedett, akkor véletlenszerűen változik
            // Ha a részvény az elmúlt 3 napban csökkent, stabilizálódni kezd
            // Ha a részvény az elmúlt 4 napban stabil volt, véletlenszerű irányba kezd mozogni
            var backwardsHistory = _changeHistory.Cast<double>().Reverse().ToList();
            var directionBase = CalculateDirection(backwardsHistory[0]);
                backwardsHistory = backwardsHistory.Skip(1).ToList();
            var stableDays = backwardsHistory.FindLastIndex(
                change => Math.Abs(CalculateDirection(change) - directionBase) < 0.001
            ) + 1;

            if (directionBase < 0.0)
            {
                if (stableDays > 3) 
                    return random.NextDouble() * 0.2 - 0.1;
                return random.NextDouble() * 0.3 - 0.3; 
            } 
            
            if (directionBase > 0.0)
            {
                if (stableDays > 6) 
                    return random.NextDouble() * 0.2 - 0.1;
                return random.NextDouble() * 0.3; 
            }
            
            if (stableDays > 4) 
                return random.NextDouble() * 0.2 - 0.1;
            
            return random.NextDouble() * 0.02 - 0.01; 
        }

        private static double CalculateDirection(double history)
        {
            var directionBase = history > 1 ? 1.0 : -1.0;
            if (Math.Abs(directionBase) < 0.02f)
                directionBase = 0.0;
            return directionBase;
        }

        public override string ToString() => $"{Name} ({Abbreviation}): ${Price:F2}";
        
        public string GetChangeHistory(int limit = 5)
        {
            var result = "";
            _changeHistory
                .Cast<double>()
                .Reverse()    
                .Take(limit)
                .Reverse()    
                .ToList()
                .ForEach(change => result += $"{(change * 100.0):F2}; ");
            return result;
        }
    }
}