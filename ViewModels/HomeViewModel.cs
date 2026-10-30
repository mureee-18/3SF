using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Microsoft.Maui.Dispatching;
using ThreeSF.Models;

namespace ThreeSF.ViewModels;

public sealed class HomeViewModel : ViewModelBase
{
    private readonly AppSettings _settings;
    private readonly IDispatcherTimer _uptimeTimer;
    private readonly DateTime _startedAt = DateTime.Now;
    private int _sampleCounter;
    private string _greeting = string.Empty;
    private string _uptimeText = "00:00:00";
    private string _statusMessage = "準備完了";

    public HomeViewModel(AppSettings settings)
    {
        _settings = settings;

        Items = new ObservableCollection<string> { "この画面はテンプレートのサンプルです。" };
        Items.CollectionChanged += OnItemsChanged;

        AddItemCommand = new RelayCommand(AddItem);
        ClearItemsCommand = new RelayCommand(ClearItems, () => Items.Count > 0);

        _uptimeTimer = Application.Current!.Dispatcher.CreateTimer();
        _uptimeTimer.Interval = TimeSpan.FromSeconds(1);
        _uptimeTimer.Tick += (_, _) => UptimeText = (DateTime.Now - _startedAt).ToString(@"hh\:mm\:ss");
        _uptimeTimer.Start();

        RefreshGreeting();
    }

    public string Title => "ホーム";

    public string Subtitle => $"{Greeting} ・ {DateTime.Now:yyyy/M/d (ddd)}";

    public string Greeting
    {
        get => _greeting;
        private set => SetProperty(ref _greeting, value);
    }

    public ObservableCollection<string> Items { get; }

    public int ItemCount => Items.Count;

    public bool IsEmpty => Items.Count == 0;

    public string UptimeText
    {
        get => _uptimeText;
        private set => SetProperty(ref _uptimeText, value);
    }

    public string Version => AppMetadata.Version;

    public string StatusMessage
    {
        get => _statusMessage;
        private set => SetProperty(ref _statusMessage, value);
    }

    public RelayCommand AddItemCommand { get; }

    public RelayCommand ClearItemsCommand { get; }

    public void RefreshGreeting()
    {
        var name = string.IsNullOrWhiteSpace(_settings.UserName) ? "ゲスト" : _settings.UserName.Trim();
        var hour = DateTime.Now.Hour;
        var timeOfDay = hour switch
        {
            < 5 => "こんばんは",
            < 11 => "おはようございます",
            < 18 => "こんにちは",
            _ => "こんばんは",
        };

        Greeting = $"{timeOfDay}、{name} さん";
        OnPropertyChanged(nameof(Subtitle));
    }

    private void AddItem()
    {
        _sampleCounter++;
        Items.Insert(0, $"サンプル項目 {_sampleCounter}（{DateTime.Now:HH:mm:ss}）");
        StatusMessage = $"項目を追加しました（合計 {Items.Count} 件）。";
    }

    private void ClearItems()
    {
        Items.Clear();
        StatusMessage = "項目をクリアしました。";
    }

    private void OnItemsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(ItemCount));
        OnPropertyChanged(nameof(IsEmpty));
        ClearItemsCommand.RaiseCanExecuteChanged();
    }
}
