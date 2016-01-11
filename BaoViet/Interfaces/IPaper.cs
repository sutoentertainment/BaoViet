using BaoViet.Models;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;

namespace BaoViet.Interfaces
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