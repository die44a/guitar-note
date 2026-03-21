using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using GuitarNote.ViewModels;

namespace GuitarNote.Views;

public partial class TitleBar : UserControl
{
    public TitleBar()
    {
        InitializeComponent();
    }

    private void TitleButton_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is TitleBarViewModel vm && sender is Button button)
        {
            var window = this.GetVisualRoot() as Window;
            if (window == null)
                throw new ArgumentNullException("Window was not found in parent window");

            switch (button.Name)
            {
                case "CloseButton":
                    vm.OnCloseButtonClicked(window);
                    break;
                case "ResizeButton":
                    vm.OnResizeButtonClicked(window);
                    break;
                case "MinimizeButton":
                    vm.OnMinimizeButtonClick(window);
                    break;
            }
        }
    }
}