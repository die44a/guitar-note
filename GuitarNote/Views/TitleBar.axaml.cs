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
    public event Action? MinimizeRequested;
    public event Action? ResizeRequested;
    public event Action? CloseRequested;

    private void TitleButton_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button) 
            return;
        
        switch (button.Name)
        {
            case "MinimizeButton":
                MinimizeRequested?.Invoke();
                break;
            case "ResizeButton":
                ResizeRequested?.Invoke();
                break;
            case "CloseButton":
                CloseRequested?.Invoke();
                break;
        }
    }
    
    public TitleBar()
    {
        InitializeComponent();
        
        PointerPressed += TitleBar_PointerPressed;
        DoubleTapped += TitleBar_DoubleClick;
    }

    private void TitleBar_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            (this.GetVisualRoot() as Window)?.BeginMoveDrag(e);
    }

    private void TitleBar_DoubleClick(object? sender, TappedEventArgs e)
        => ResizeRequested?.Invoke();
}