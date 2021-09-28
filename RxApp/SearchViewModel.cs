using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace RxApp
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        private readonly DataService _dataService = new DataService();
        public SearchViewModel()
        {
            SearchTerm = string.Empty;
            SearchResults = new BindingList<string>
            {
                "Hello world"
            };
            SearchInProgress = Visibility.Hidden;
            SearchCommand = new RelayCommand(term => true, async term => {
                var results = await _dataService.SearchAsync(SearchTerm);
                foreach (var entry in results)
                {
                    SearchResults.Add(entry);
                }
            });
        }

        /// <summary>
        /// SearchTerm
        /// </summary>
        private string _searchTerm;
        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                _searchTerm = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// SearchResults
        /// </summary>
        public BindingList<string> SearchResults { get; set; }

        /// <summary>
        /// SearchCommand
        /// </summary>
        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get { return _searchCommand;}
            set
            {
                _searchCommand = value;
                OnPropertyChanged();
            }
        }

        private Visibility _searchInProgress;
        public Visibility SearchInProgress 
        {
            get
            {
                return _searchInProgress;
            }
            set
            {
                _searchInProgress = value;
                OnPropertyChanged();
            }
        }

        #region Helpers

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
