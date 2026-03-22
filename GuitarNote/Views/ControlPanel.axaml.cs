using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GuitarNote.Views;

public partial class ControlPanel : UserControl
{
    public event Action? OpenCatalogRequested;
    public event Action? OpenContructRequested;
    public event Action? OpenTunerRequested;
    public event Action? OpenSettingsRequested;
    
    public ControlPanel()
    {
        InitializeComponent();
    }
    
    private void ControlPanelButton_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button) 
            return;
        
        switch (button.Name)
        {
            case "CatalogButton":
                OpenCatalogRequested?.Invoke();
                break;
            case "ContructorButton":
                OpenContructRequested?.Invoke();
                break;
            case "TunerButton":
                OpenTunerRequested?.Invoke();
                break;
            case "SettingsButton":
                OpenSettingsRequested?.Invoke();
                break;
        }
    }
}