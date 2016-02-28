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
