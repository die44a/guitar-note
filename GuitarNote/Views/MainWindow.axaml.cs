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
    }
}