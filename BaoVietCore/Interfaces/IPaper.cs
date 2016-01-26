using BaoVietCore.Models;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;

namespace BaoVietCore.Interfaces
{
    public interface IPaper : IRssSource
    {
        ObservableCollection<Category> Categories { get; set; }
        double CellWidth { get; set; }
        string ImageSource { get; set; }
        Thickness Margin { get; set; }
        string Title { get; set; }
        PaperType Type { get; set; }
        string HomePage { get; set; }
        int Index { get; set; }
        string TypeString { get; }

        RelayCommand PinCommand { get; set; }
        RelayCommand<FrameworkElement> ShowMenuCommand { get; set; }
    }
}