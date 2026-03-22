using Avalonia.Controls;
using GuitarNote.ViewModels;

namespace GuitarNote.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        TitleBar.DataContext = new TitleBarViewModel();
        DataContext = new MainWindowViewModel();
        
        TitleBar.CloseRequested += CloseWindow;
        TitleBar.MinimizeRequested += MinimizeWindow;
        TitleBar.ResizeRequested += ResizeWindow;
    }

    private void CloseWindow()
    {
        Close();
    }

    private void MinimizeWindow()
    {
        WindowState = WindowState.Minimized;
    }

    private void ResizeWindow()
    {
        WindowState = WindowState == WindowState.Maximized
            ? WindowState.Normal
            : WindowState.Maximized;
    }
}