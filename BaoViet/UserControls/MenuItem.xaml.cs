using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BaoViet.Controls
{
    public sealed partial class MenuItem : UserControl
    {
        /// <summary>
        /// This it how we get the symbol from the xaml binding.
        /// </summary>
        public Symbol SymbolIcon
        {
            get { return (Symbol)GetValue(SymbolIconProperty); }
            set { SetValue(SymbolIconProperty, value); }
        }

        public static readonly DependencyProperty SymbolIconProperty =
            DependencyProperty.Register(
                "SymbolIcon",                     // The name of the DependencyProperty
                typeof(Symbol),                   // The type of the DependencyProperty
                typeof(MenuItem), // The type of the owner of the DependencyProperty
                new PropertyMetadata(           // OnBlinkChanged will be called when Blink changes
                    false                      // The default value of the DependencyProperty
                ));

        public string ButtonLabel
        {
            get { return (string)GetValue(ButtonLabelProperty); }
            set { SetValue(ButtonLabelProperty, value); }
        }

        public static readonly DependencyProperty ButtonLabelProperty =
            DependencyProperty.Register(
                "ButtonLabel",                     // The name of the DependencyProperty
                typeof(string),                   // The type of the DependencyProperty
                typeof(MenuItem), // The type of the owner of the DependencyProperty
                new PropertyMetadata(           // OnBlinkChanged will be called when Blink changes
                    ""                      // The default value of the DependencyProperty
                ));

        public MenuItem()
        {
            this.InitializeComponent();
        }
    }
}
