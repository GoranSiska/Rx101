using System;

namespace RxConsole
{
    public class CryptoPriceMonitor : IObserver<CryptoPriceChangeEvent>
    {
        public void OnCompleted()
        {
            // do nothing;
        }

        public void OnError(Exception error)
        {
            Console.WriteLine(error);
        }

        public virtual void OnNext(CryptoPriceChangeEvent priceChange)
        {
            if (priceChange.Delta > 5.0d)
            {
                Console.WriteLine($"BUY {priceChange.Name}, it jumped by {priceChange.Delta}!");
            } else
            {
                Console.WriteLine($"SELL {priceChange.Name}, it fell by {priceChange.Delta}!");
            }
        }
    }
}
