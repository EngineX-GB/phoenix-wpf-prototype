using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phoenix_prototype
{
    public class AppDataService
    {

        public event Action<SearchEntry> SearchEntrySelected;

        public void NotifySearchEntrySelected(SearchEntry searchEntry)
        {
            SearchEntrySelected?.Invoke(searchEntry);
        }


        public ObservableCollection<OrderEntry> OrderEntries { get; } = new();
        public ObservableCollection<WatchlistEntry> WatchlistCollection { get; } = new();

        public ObservableCollection<FeedbackEntry> FeedbackEntries { get; } = new();
        public ObservableCollection<ServiceReportHeadlineEntry> ServiceReportHeadlineEntries { get; } = new();

        public ObservableCollection<SearchEntry> SearchEntries { get; } = new();
    }

}
