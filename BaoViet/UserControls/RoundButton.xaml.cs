using BaoVietCore.Helpers;
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
    public sealed partial class RoundButton : UserControl
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
                typeof(RoundButton), // The type of the owner of the DependencyProperty
                new PropertyMetadata(           // OnBlinkChanged will be called when Blink changes
                      null                    // The default value of the DependencyProperty
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
                typeof(RoundButton), // The type of the owner of the DependencyProperty
                new PropertyMetadata(           // OnBlinkChanged will be called when Blink changes
                    ""                      // The default value of the DependencyProperty
                ));

        #region Font Icon Glyph

        /// <summary>
        /// This it how we get the font glyph from the xmal binding.
        /// </summary>
        public string FontIconGlyph
        {
            get { return (string)GetValue(FontIconGlyphProperty); }
            set { SetValue(FontIconGlyphProperty, value); }
        }

        public static readonly DependencyProperty FontIconGlyphProperty =
            DependencyProperty.Register(
                "FontIconGlyph",                     // The name of the DependencyProperty
                typeof(string),                   // The type of the DependencyProperty
                typeof(RoundButton), // The type of the owner of the DependencyProperty
                new PropertyMetadata(           // OnBlinkChanged will be called when Blink changes
                    "",                      // The default value of the DependencyProperty
                    new PropertyChangedCallback(OnFontIconGlyphChangedStatic)
                ));

        private static void OnFontIconGlyphChangedStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = d as RoundButton;
            if (instance != null)
            {
                instance.OnFontIconGlyphChanged((string)e.NewValue);
            }
        }

        private void OnFontIconGlyphChanged(string newGlyph)
        {
            ClearIcon();

            bool isShowing = !String.IsNullOrWhiteSpace(newGlyph);
            if (isShowing)
            {
                ui_fontIcon.Visibility = Visibility.Visible;
                ui_fontIcon.Glyph = newGlyph;
            }
        }

        private void ClearIcon()
        {
            ui_symbolTextBlock.Visibility = Visibility.Collapsed;
            ui_fontIcon.Visibility = Visibility.Collapsed;
            ui_fontIcon.Glyph = "";
        }

        #endregion

        public event EventHandler<EventArgs> OnIconTapped
        {
            add { m_onTapped.Add(value); }
            remove { m_onTapped.Remove(value); }
        }
        SmartWeakEvent<EventHandler<EventArgs>> m_onTapped = new SmartWeakEvent<EventHandler<EventArgs>>();



        public RoundButton()
        {
            this.InitializeComponent();
        }
    }
}
