using System;
using System.Reactive.Linq;

namespace RxConsole
{
    public static class CryptoServiceExtensions
    {
        public static IObservable<CryptoPriceChangeEvent> WhenLargeChange(this IObservable<CryptoPriceEvent> source)
        {
            return
                source
                    .GroupBy(cpe => cpe.Name)
                    .Select(c => c
                        .Buffer(2, 1)
                        .Select(ccpe => ccpe[1].Price - ccpe[0].Price)
                        .Where(delta => Math.Abs(delta) > 5.0)
                        .Select(delta => new CryptoPriceChangeEvent { Name = c.Key, Delta = delta }))
                    .SelectMany(cpe => cpe);
        }
    }
}
