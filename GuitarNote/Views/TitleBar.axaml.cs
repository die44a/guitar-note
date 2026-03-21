using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using GuitarNote.ViewModels;

namespace GuitarNote.Views;

public partial class TitleBar : UserControl
{
    private Window? _window;
    
    public TitleBar()
    {
        InitializeComponent();
        PointerPressed += TitleBar_PointerPressed;
        DoubleTapped += TitleBar_DoubleClick;
        
        AttachedToVisualTree += (s, e) =>
        {
            _window = this.GetVisualRoot() as Window;
            if (_window == null)
                throw new ArgumentNullException("Window was not found in visual tree");
        };
    }

    private void TitleBar_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            _window!.BeginMoveDrag(e);
    }

    private void TitleBar_DoubleClick(object? sender, TappedEventArgs e)
        => ResizeWindow();
    
    private void TitleButton_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is TitleBarViewModel vm && sender is Button button)
        {
            switch (button.Name)
            {
                case "CloseButton":
                    _window!.Close();
                    break;
                case "ResizeButton":
                    ResizeWindow();  
                    break;
                case "MinimizeButton":
                    MinimizeWindow();
                    break;
            }
        }
    }

    private void MinimizeWindow()
        =>_window!.WindowState = WindowState.Minimized;


    private void ResizeWindow()
        => _window!.WindowState = _window!.WindowState == WindowState.Maximized 
            ? WindowState.Normal 
            : WindowState.Maximized;
}