using BaoVietCore.Helpers;
using BaoVietCore.Services;
using System;

namespace BaoVietCore.Interfaces
{
    public interface IKeyboardService
    {
        event EventHandler<KeyboardEventArgs> KeyDown;
        event EventHandler<KeyboardEventArgs> RefreshRequest;
    }
}