using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RxApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DataService _dataService = new DataService();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new SearchViewModel();

            //var observable = Observable.FromEventPattern<TextChangedEventHandler, TextChangedEventArgs>(
            //    handler => SearchInput.TextChanged += handler,
            //    handler => SearchInput.TextChanged -= handler
            //    );

            //observable
            //    .Select(_ => SearchInput.Text) // observe input textbox text - events of textargs
            //    .Do(_ => SearchResults.Items.Clear()) // clear search results
            //    .Select(async term => await _dataService.SearchAsync(term)) // observe search results - events of tasks of list of results
            //    .Subscribe(item => SearchResults.Items.Add(item)) // add them to search results property
            //    ;

            //.SelectMany(results => results) // get individual results collections

            //.Subscribe(item =>
            // {
            //     if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
            //     {
            //         Dispatcher.Invoke(() => SearchResults.Items.Add(item));
            //     }
            //}) // add them to search results property

            //.SelectMany(items => items) // get individual result items

            //.Throttle(TimeSpan.FromMilliseconds(500)) // default scheduler

            //.Do(_ => SearchProgress.Visibility = Visibility.Visible)
            //.Do(_ => SearchProgress.Visibility = Visibility.Hidden)

            //.ObserveOnDispatcher() // following is executed on dispatcher - in this case UI thread
            //.ObserveOn(Scheduler.Default) // following is executed on ThreadPoolTaskScheduler - so a non UI thread from pool

            //.Where(t => t.Length > 2) // observe when search term is longer then 3 chars

            //.DistinctUntilChanged() // observe only distinct values

            //.Switch() // switch to newer event and flatten sequence

            /* complete
            observable
                .Select(_ => SearchInput.Text) // observe input textbox text - events of textargs
                .Throttle(TimeSpan.FromMilliseconds(500)) // default scheduler
                .Where(t => t.Length > 2) // observe when search term is longer then 3 chars
                .ObserveOnDispatcher() // following is executed on dispatcher - in this case UI thread
                .Do(_ => SearchProgress.Visibility = Visibility.Visible)
                .Do(_ => SearchResults.Items.Clear()) // clear search results
                .ObserveOn(Scheduler.Default) // following is executed on ThreadPoolTaskScheduler - so a non UI thread from pool
                .Select(async term => await _dataService.SearchAsync(term)) // observe search results - events of tasks of list of results
                .Switch() // switch to newer event and flatten sequence
                .ObserveOnDispatcher() // following is executed on dispatcher - in this case UI thread
                .Do(_ => SearchProgress.Visibility = Visibility.Hidden)
                .SelectMany(items => items) // get individual result items
                .Subscribe(item => SearchResults.Items.Add(item)) // add them to search results property
                ;             
             */
        }

        private void SearchOnClick(object sender, RoutedEventArgs e) //async
        {
            SearchProgress.Visibility = Visibility.Visible;
            var results = _dataService.Search(SearchInput.Text);
            //var results = await _dataService.SearchAsync(SearchInput.Text);

            SearchResults.Items.Clear();
            foreach (var entry in results)
            {
                SearchResults.Items.Add(entry);
            }
            SearchProgress.Visibility = Visibility.Hidden;

            //var context = DataContext as SearchViewModel;
            //context?.SearchResults.Clear();
            //foreach (var entry in results)
            //{
            //    context?.SearchResults.Add(entry);
            //}
        }

        private async void SearchInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchProgress.Visibility = Visibility.Visible;
            //var results = _dataService.Search(SearchInput.Text);
            var results = await _dataService.SearchAsync(SearchInput.Text);

            SearchResults.Items.Clear();
            foreach (var entry in results)
            {
                SearchResults.Items.Add(entry);
            }
            SearchProgress.Visibility = Visibility.Hidden;
        }
    }
}
