using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModel;
public partial class MonkeysViewModel : BaseViewModel
{
    //public Command GetMonkeysCommand { get; }
    public ObservableCollection<Monkey> Monkeys { get; } = new();

    MonkeyService monkeyService;
        
    public MonkeysViewModel(MonkeyService monkeyService)
    {
        Title = "Monkey Finder";
        this.monkeyService = monkeyService;
        //GetMonkeysCommand = new Command(async () => await GetMonkeysAsync());
    }
    [RelayCommand]
    async Task GetMonkeysAsync()
    {
        if (IsBusy)
            return;
        try
        {
            IsBusy = true;
            var monkeys = await monkeyService.GetMonkeys();
            if (Monkeys.Count != 0)
                Monkeys.Clear();

            foreach (var monkey in monkeys)
                Monkeys.Add(monkey);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get monkeys: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }

    }

}
