using System;
using BaoViet.ViewModels;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Threading;

namespace BaoViet.Models
{
    public class MenuItemBase : IMenuItem
    {
        public MenuItemBase(Symbol icon)
        {
            Icon = icon;
        }
        public MenuItemBase()
        {

        }
        public string MenuTitle { get; set; }
        public string Glyph { get; set; }
        public MenuItemType Type { get; set; }

        public Symbol Icon
        {
            get; set;
        }

        public virtual void OnClicked()
        {

        }
    }

    public enum MenuItemType
    {
        Home,
        Saved,
        Setting
    }


    public class CurrencyMenuItem : MenuItemBase
    {
        public CurrencyMenuItem(Symbol icon) : base(icon)
        {

        }
        public override void OnClicked()
        {
            //Navigate to Currency page
            App.Current.NavigationService.NavigateTo(Pages.Currency);
        }
    }

    public class OCRMenuItem : MenuItemBase
    {
        public OCRMenuItem(Symbol icon) : base(icon)
        {

        }
    }

    public class WeatherMenuItem : MenuItemBase
    {
        public WeatherMenuItem(Symbol icon) : base(icon)
        {

        }
    }

    public class FlashMenuItem : MenuItemBase
    {
        public FlashMenuItem(Symbol icon) : base(icon)
        {
            
        }

        public override void OnClicked()
        {
            App.Current.NavigationService.NavigateTo(Pages.Flash);
        }
    }
}