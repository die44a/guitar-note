
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GuitarNote.ViewModels;

public class TitleBarViewModel : ViewModelBase
{
    public void OnMinimizeButtonClick(Window window)
    {
        window.WindowState = WindowState.Minimized;
    }

    public void OnCloseButtonClicked(Window window)
    {
        window.Close();
    }

    // TODO: implement this better
    public void OnResizeButtonClicked(Window window)
    {
        window.WindowState = WindowState.Maximized;
    }
}   