namespace GuitarNote.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private object? _currentView;

    private TextFieldViewModel TextFieldVm { get; } = new();
    private TunerFieldViewModel TunerFieldVm { get; } = new();
    private SongListFieldViewModel SongListFieldVm { get; } = new();
    
    public object? CurrentView
    {
        get => _currentView;
        set => SetProperty(ref _currentView, value);
    }

    public MainWindowViewModel()
    {
        _currentView = TunerFieldVm;
    }
    
    public void ShowContructorView()
        => CurrentView = TextFieldVm;
    
    public void ShowTunerField()
        => CurrentView = TunerFieldVm;
    
    public void ShowSongListField()
        => CurrentView = SongListFieldVm;
}