using BaoVietCore.Models;
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
    }
}