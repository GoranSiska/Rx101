using Microsoft.Reactive.Testing;
using NUnit.Framework;
using RxConsole;
using System;
using System.Reactive;
using System.Reactive.Linq;

namespace RxUnitTests
{
    public class Tests101
    {
        //CryptoServiceExtensionsTests
        [Test]
        public void GivenWhenLargeChangeQuery_WhenLargeChange_ObserverIsNotified()
        {
            // arrange
            var scheduler = new TestScheduler();
            scheduler.Stop();
            var source = scheduler.CreateHotObservable(
                new Recorded<Notification<CryptoPriceEvent>>(1, Notification.CreateOnNext(new CryptoPriceEvent { Name = "BTC", Price = 1.0 })),
                new Recorded<Notification<CryptoPriceEvent>>(2, Notification.CreateOnNext(new CryptoPriceEvent { Name = "BTC", Price = 7.0 }))
                );

            var monitor = new CryptoPriceMonitor();
            var observerNotified = false;
            source
                .WhenLargeChange()
                .Subscribe(cpc => {
                    //Assert.AreEqual(6.0, cpc.Delta);
                    observerNotified = true;
                });

            // act
            scheduler.AdvanceBy(2);

            // assert
            Assert.IsTrue(observerNotified);
        }

        [Test]
        public void GivenErrorInObservable_CatchError()
        {
            // arrange
            var scheduler = new TestScheduler();
            scheduler.Stop();
            var source = scheduler.CreateHotObservable(
                new Recorded<Notification<CryptoPriceEvent>>(1, Notification.CreateOnNext(new CryptoPriceEvent { Name = "BTC", Price = 1.0 })),
                new Recorded<Notification<CryptoPriceEvent>>(2, Notification.CreateOnError<CryptoPriceEvent>(new Exception("Error happened!")))
                );

            var monitor = new CryptoPriceMonitor();
            var exceptionHappened = false;
            source
                .WhenLargeChange()
                .Catch((Exception ex) =>
                {
                    exceptionHappened = true;
                    return Observable.Empty<CryptoPriceChangeEvent>();
                })
                .Subscribe(cpc => {});

            // act
            scheduler.AdvanceBy(2);

            // assert
            Assert.IsTrue(exceptionHappened);
        }
    }
}