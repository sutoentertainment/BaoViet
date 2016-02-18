using BaoViet.Models;
using Windows.UI.Xaml.Controls;

namespace BaoViet.ViewModels
{
    public interface IMenuItem
    {
        string MenuTitle { get; set; }
        string Glyph { get; set; }
        Symbol Icon { get; set; }
        MenuItemType Type { get; set; }
        void OnClicked();
    }
}