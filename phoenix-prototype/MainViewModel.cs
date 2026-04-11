using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly WebSocketService _wsService = new WebSocketService();

    private string _latestStatus;


    private Brush _ingestionStatusColor = Brushes.DarkGray;
    
    // colour for service ping status
    public Brush IngestionStatusColor
    {
        get => _ingestionStatusColor;
        set { _ingestionStatusColor = value; OnPropertyChanged(); }
    }


    public string LatestStatus
    {
        get => _latestStatus;
        set { _latestStatus = value; OnPropertyChanged(); }
    }

    public async Task InitializeAsync()
    {
        _wsService.OnNotificationReceived += HandleNotification;
        await _wsService.ConnectAsync("ws://localhost:8081/ws");
    }

    private void HandleNotification(Notification notif)
    {
        Debug.WriteLine($"WS NOTIFICATION RECEIVED: {notif.Content.ServiceName} - {notif.Content.Status}");

        Application.Current.Dispatcher.Invoke(() =>
        {
            if (notif.Content.ServiceName == "ingestion")
            {
                Debug.WriteLine("Updating IngestionStatusColor...");

                switch (notif.Content.Status)
                {
                    case "OK":
                        IngestionStatusColor = Brushes.Aqua;
                        break;

                    case "ERROR":
                        IngestionStatusColor = Brushes.Orange;
                        break;

                    case "STOPPED":
                        IngestionStatusColor = Brushes.LightGray;
                        break;
                }

                Debug.WriteLine("New Color: " + IngestionStatusColor.ToString());
            }
        });
    }



    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
