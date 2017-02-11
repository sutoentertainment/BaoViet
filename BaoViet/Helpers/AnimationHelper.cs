using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace BaoViet.Helpers
{
    class AnimationHelper
    {
        public static void AnimateThumbnail(UIElement element)
        {
            var transform = element.RenderTransform as ScaleTransform;
            if (transform == null)
            {
                return;
            }
            DoubleAnimation scaleY = new DoubleAnimation();
            DoubleAnimation scaleX = new DoubleAnimation();

            scaleY.From = scaleX.From = 0.3;
            scaleY.To = scaleX.To = 1;

            QuinticEase easing = new QuinticEase();
            easing.EasingMode = EasingMode.EaseOut;
            scaleY.EasingFunction = scaleX.EasingFunction = easing;

            Storyboard storyboard = new Storyboard();
            Storyboard.SetTarget(scaleY, transform);
            Storyboard.SetTargetProperty(scaleY, "ScaleY");

            Storyboard.SetTarget(scaleX, transform);
            Storyboard.SetTargetProperty(scaleX, "ScaleX");

            storyboard.Children.Add(scaleX);
            storyboard.Children.Add(scaleY);
            storyboard.Begin();
        }
    }
}
