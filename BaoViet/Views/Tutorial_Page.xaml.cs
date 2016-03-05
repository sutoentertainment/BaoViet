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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BaoViet.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Tutorial_Page : Page
    {
        public Tutorial_Page()
        {
            this.InitializeComponent();
            tutorialTextBlock.Text = "M\u1ED9t s\u1ED1 ph\u00EDm t\u1EAFt c\u01A1 b\u1EA3n:\r\n\r\nF5: refresh\r\nAlt + Left: back\r\n\r\nVu\u1ED1t tin t\u1EEB ph\u1EA3i qua tr\u00E1i \u0111\u1EC3 l\u01B0u tin.";
        }
    }
}
