using Cursach.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cursach.ViewModel
{
    public class WindowVM : BaseVM
    {
        CurrentWindowControl currentWindowControl;


        public Window CurrentWindow
        {
            get => currentWindowControl.Window;
        }

        
        public CommandVM MainMenu { get; set; }

        public WindowVM()
        {
            currentWindowControl = new CurrentWindowControl();
            currentWindowControl.WindowChanged += CurrentWindowControl_WindowChanged;

            MainMenu = new CommandVM(() => {
                currentWindowControl.SetWindow(new MainWindow());
            });



        }

        private void CurrentWindowControl_WindowChanged(object sender, EventArgs e)
        {
            Signal(nameof(CurrentWindow));
        }
    }
}
