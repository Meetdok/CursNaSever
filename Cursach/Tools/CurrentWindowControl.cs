using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cursach.Tools
{
    public class CurrentWindowControl
    {
        Stack<Window> windows = new Stack<Window>();

        public Window Window { get; internal set; }
        public event EventHandler WindowChanged;

        internal void SetWindow(Window optionWindow)
        {
            if (Window != null)
                windows.Push(Window);
            Window = optionWindow;
            WindowChanged?.Invoke(this, new EventArgs());
        }

        internal void Back()
        {
            if (windows.Count > 0)
                Window = windows.Pop();
            else
                Window = null;
            WindowChanged?.Invoke(this, new EventArgs());
        }
    }
}
