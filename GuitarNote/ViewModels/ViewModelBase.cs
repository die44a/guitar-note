using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GuitarNote.ViewModels;

public abstract class ViewModelBase : ObservableObject
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}