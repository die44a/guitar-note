using Avalonia.Controls;
using Avalonia.Interactivity;
using GuitarNote.ViewModels;

namespace GuitarNote.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        TitleBar.DataContext = new TitleBarViewModel();
        DataContext = new MainWindowViewModel();
        
        ControlPanelView.CatalogButton.Click += OpenCatalog;
        ControlPanelView.TunerButton.Click += OpenTuner;
        ControlPanelView.ConstructorButton.Click += OpenConstructor;
        
        TitleBar.CloseRequested += CloseWindow;
        TitleBar.MinimizeRequested += MinimizeWindow;
        TitleBar.ResizeRequested += ResizeWindow;
    }

    private void OpenCatalog(object? sender, RoutedEventArgs routedEventArgs)
    {
        var vm = (MainWindowViewModel)DataContext!;
        vm.ShowSongListField();
    }

    private void OpenTuner(object? sender, RoutedEventArgs routedEventArgs)
    {
        var vm = (MainWindowViewModel)DataContext!;
        vm.ShowTunerField();
    }

    private void OpenConstructor(object? sender, RoutedEventArgs routedEventArgs)
    {
        var vm = (MainWindowViewModel)DataContext!;
        vm.ShowContructorView();
    }
    
    //TODO : bind Title buttons directly
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