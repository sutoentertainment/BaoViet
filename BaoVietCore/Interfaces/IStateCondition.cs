using BaoVietCore.Helpers;
using BaoVietCore.Services;
using Windows.UI.Xaml.Controls;

namespace BaoVietCore.Interfaces
{
    public interface IStateCondition
    {
        double TableThreshold { get; }
        AppState GetCurrentState();
        void Configurate(Frame rootFrame, double width);
    }

    public enum DeviceTypes
    {
        Desktop,
        Mobile,
        Other
    }

    public enum AppState
    {
        Tablet,
        Mobile
    }
}