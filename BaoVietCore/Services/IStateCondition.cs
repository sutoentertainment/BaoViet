using Windows.UI.Xaml.Controls;

namespace BaoVietCore.Helpers
{
    public interface IStateCondition
    {
        AppState GetCurrentState();
        void Configurate(Frame rootFrame, double width);
    }
}