using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;

namespace BaoViet.UserControls
{
    public class WebViewExtensions
    {

        #region Fields 
        /// <summary> 
        /// IsLooping attached property for FlipView 
        /// </summary> 
        public static readonly DependencyProperty SourceBindingProperty =
            DependencyProperty.RegisterAttached(
                "SourceBinding",
                typeof(string),
                typeof(WebViewExtensions),
                new PropertyMetadata("", new PropertyChangedCallback(OnSourceBindingChanged)));
        #endregion

        #region Methods 
        /// <summary> 
        /// Gets a value indicating whether this is a looping FlipView 
        /// </summary> 
        /// <param name="obj">the flipView</param> 
        /// <returns>true if the list loops</returns> 
        public static bool GetSourceBinding(WebView obj)
        {
            return (bool)obj.GetValue(SourceBindingProperty);
        }

        /// <summary> 
        /// Sets a value indicating whether the FlipView loops 
        /// </summary> 
        /// <param name="obj">the FlipView</param> 
        /// <param name="value">true if the list loops</param> 
        public static void SetSourceBinding(WebView obj, bool value)
        {
            obj.SetValue(SourceBindingProperty, value);
        }
        #endregion

        #region Implementation 

        /// <summary> 
        /// Initialize the selection changed handler and the list if the ItemsSource is set 
        /// </summary> 
        /// <param name="dependencyObject">the FlipView</param> 
        /// <param name="args">the dependency property changed event arguments</param> 
        private static void OnSourceBindingChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var web_view = dependencyObject as WebView;

            if (!string.IsNullOrEmpty(args.NewValue.ToString()))
            {
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(args.NewValue.ToString()));
                httpRequestMessage.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; XBLWP7; ZuneWP7)");
                web_view.NavigateWithHttpRequestMessage(httpRequestMessage);
            }
        }
        #endregion
    }
}
