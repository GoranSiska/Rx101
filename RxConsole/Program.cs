using System;
using System.Reactive.Linq;

namespace RxConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            var rnd = new Random();
            var cryptos = new string[] { "BTC", "ETH", "XRP", "DOGE", "LTC" };
            var source = Observable.Interval(TimeSpan.FromSeconds(1)).Select(n =>
            {
                return new CryptoPriceEvent
                {
                    Name = cryptos[rnd.Next(0, cryptos.Length)],
                    Price = rnd.NextDouble() * 10
                };
            });
            var subscription = source
                .GroupBy(cpe => cpe.Name)
                .Subscribe(c => c
                    .Buffer(2, 1)
                    .Select(ccpe => ccpe[1].Price - ccpe[0].Price)
                    .Subscribe(delta =>
                    {
                        switch (delta)
                        {
                            case double d when d > 5.0d:
                                Console.WriteLine($"BUY {c.Key}, it jumped by {delta}!");
                                break;
                            case double d when d < -5.0d:
                                Console.WriteLine($"SELL {c.Key}, it fell by {delta}!");
                                break;
                            default:
                                Console.WriteLine($"INFO {c.Key}, changed by {delta}.");
                                break;
                        }
                    }));


            //var source = new CryptoPriceService().CreateEventSource();
            //var monitor = new CryptoPriceMonitor();
            //source
            //    .WhenLargeChange()
            //    .Subscribe(monitor);

            Console.ReadLine();
        }
    }
}
