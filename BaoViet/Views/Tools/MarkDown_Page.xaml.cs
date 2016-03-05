using BaoViet.ViewModels;
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
    public sealed partial class MarkDown_Page : BindablePage
    {
        public MarkDown_ViewModel ViewModel
        {
            get
            {
                return this.DataContext as MarkDown_ViewModel;
            }
        }
        public MarkDown_Page()
        {
            this.InitializeComponent();


            ViewModel.SourceInput = "\r\n\r\nH\u01B0\u1EDBng d\u1EABn vi\u1EBFt Markdown:\r\n\r\n# T\u1EF1a \u0111\u1EC1 c\u1EE1 l\u1EDBn nh\u1EA5t\r\n## T\u1EF1a \u0111\u1EC1 nh\u1ECF h\u01A1n m\u1ED9t ch\u00FAt\r\n###### T\u1EF1a \u0111\u1EC1 nh\u1ECF nh\u1EA5t\r\n\r\n**Ch\u1EEF in \u0111\u1EADm**\r\n\r\n*Ch\u1EEF in nghi\u00EAng*\r\n\r\n**ch\u1EEF in _nghi\u00EAng v\u00E0 \u0111\u1EADm_**\r\n\r\n## tr\u00EDch d\u1EABn\r\n\r\nIn the words of Abraham Lincoln:\r\n\r\n> Pardon my French\r\n\r\n## link\r\n\r\n[GitHub](https://github.com/).\r\n\r\n## danh s\u00E1ch\r\n\r\n- George Washington\r\n- John Adams\r\n- Thomas Jefferson\r\n\r\n## danh s\u00E1ch c\u00F3 \u0111\u00E1nh s\u1ED1\r\n\r\n1. James Madison\r\n2. James Monroe\r\n3. John Quincy Adams\r\n\r\n## danh s\u00E1ch nhi\u1EC1u t\u1EA7ng\r\n\r\n1. Make my changes\r\n  1. Fix bug\r\n  2. Improve formatting\r\n    * Make the headings bigger\r\n2. Push my commits to GitHub\r\n3. Open a pull request\r\n  * Describe my changes\r\n  * Mention all the members of my team\r\n    * Ask for feedback\r\n\r\n## \u1EA3nh\r\n\r\n![](http://i.imgur.com/MU2dD8E.jpg)\r\n\r\n\r\n";
            var md = App.Current.Manager.MarkDownService.ConvertToMarkDown(ViewModel.SourceInput);
            ViewModel.PreviewMarkDown = md;
            ViewModel.IsPreviewOpen = true;
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.Current.Manager.DeviceService.GetAppState() == BaoVietCore.Interfaces.AppState.Tablet)
            {
                var md = App.Current.Manager.MarkDownService.ConvertToMarkDown(InputTextBox.Text);
                ViewModel.PreviewMarkDown = md;
                ViewModel.IsPreviewOpen = true;
            }
        }
    }
}
