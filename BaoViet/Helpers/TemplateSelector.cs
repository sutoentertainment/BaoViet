using BaoViet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BaoViet.Helpers
{
    public abstract class TemplateSelector : ContentControl
    {
        public abstract DataTemplate SelectTemplate(object item, DependencyObject container);

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            ContentTemplate = SelectTemplate(newContent, this);
        }
    }

    //public class WeatherTemplateSelector : TemplateSelector
    //{
    //    public DataTemplate CurrentWeather
    //    {
    //        get;
    //        set;
    //    }

    //    public DataTemplate WeatherForecast
    //    {
    //        get;
    //        set;
    //    }


    //    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    //    {
    //        var contact = item as WeatherModel;
    //        if (contact != null)
    //        {
    //            return contact.IsCurrent ? CurrentWeather : WeatherForecast;
    //        }

    //        return null;
    //    }
    //}

    //public class LottoTemplateSelector : TemplateSelector
    //{
    //    public DataTemplate SpecialPrize
    //    {
    //        get;
    //        set;
    //    }

    //    public DataTemplate NormalPrize
    //    {
    //        get;
    //        set;
    //    }


    //    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    //    {
    //        var contact = item as LottoInfo;
    //        if (contact != null)
    //        {
    //            return contact.IsSpecialPrize ? SpecialPrize : NormalPrize;
    //        }

    //        return null;
    //    }
    //}

    //public class GoldTemplateSelector : TemplateSelector
    //{
    //    public DataTemplate HeaderItem
    //    {
    //        get;
    //        set;
    //    }

    //    public DataTemplate NormalItem
    //    {
    //        get;
    //        set;
    //    }


    //    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    //    {
    //        var contact = item as GoldInfo;
    //        if (contact != null)
    //        {
    //            return contact.IsHeader ? HeaderItem : NormalItem;
    //        }

    //        return null;
    //    }
    //}

    public class CurrencyTemplateSelector : TemplateSelector
    {
        public DataTemplate HeaderItem
        {
            get;
            set;
        }

        public DataTemplate NormalItem
        {
            get;
            set;
        }


        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var contact = item as CurrencyInfo;
            if (contact != null)
            {
                return contact.IsHeader ? HeaderItem : NormalItem;
            }

            return null;
        }
    }
}
