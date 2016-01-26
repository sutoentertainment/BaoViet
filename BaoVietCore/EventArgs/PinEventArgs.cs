using BaoVietCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoVietCore.CustomEventArgs
{
    public class PinEventArgs : EventArgs
    {
        public IPaper Paper { get; set; }

        public PinEventArgs(IPaper p)
        {
            Paper = p;
        }
    }
}
