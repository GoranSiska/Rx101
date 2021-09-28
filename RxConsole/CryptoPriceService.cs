using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace RxConsole
{
    public class CryptoPriceService
    {
        public IObservable<CryptoPriceEvent> CreateEventSource()
        {
            var rnd = new Random();
            var cryptos = new string[] { "BTC", "ETH", "XRP", "DOGE", "LTC" };
            return Observable
                    .Interval(TimeSpan.FromSeconds(1))
                    .Select(_ =>
                    {
                        return new CryptoPriceEvent
                        {
                            Name = cryptos[rnd.Next(0, cryptos.Length)],
                            Price = rnd.NextDouble() * 10
                        };
                    });

            //return Observable.Create<CryptoPriceEvent>(observer =>
            //{
            //    try
            //    {
            //        var i = 0;
            //        while (i < 1000)
            //        {
            //            i++;
            //            var cpe = new CryptoPriceEvent
            //            {
            //                Name = cryptos[rnd.Next(0, cryptos.Length)],
            //                Price = rnd.NextDouble() * 10
            //            };
            //            observer.OnNext(cpe);
            //        }
            //        observer.OnCompleted();
            //        return Disposable.Empty;
            //    }
            //    catch (Exception ex)
            //    {
            //        observer.OnError(ex);
            //        return Disposable.Empty;
            //    }
            //});
        }
    }
}
