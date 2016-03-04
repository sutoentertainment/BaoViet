using BaoVietCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace BaoVietCore.Helpers
{
    public class StateCondition : IStateCondition
    {
        private double appWidth;
        public double TableThreshold { get; private set; }

        public StateCondition(double tableThreshold)
        {
            this.TableThreshold = tableThreshold;
        }

        public void Configurate(Frame rootFrame, double currentWidth = 0)
        {
            appWidth = currentWidth;
            rootFrame.SizeChanged += RootFrame_SizeChanged;
        }

        private void RootFrame_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            appWidth = e.NewSize.Width;
        }

        public AppState GetCurrentState()
        {
            if (appWidth >= TableThreshold)
                return AppState.Tablet;
            return AppState.Mobile;
        }
    }
}
