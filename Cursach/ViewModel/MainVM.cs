
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Cursach.Tools;
using Cursach.Pages;
using Cursach.ViewModel;

namespace Cursach.ViewModel
{
    public  class MainVM : BaseVM
    {
        CurrentPageControl currentPageControl;
        

        public Page CurrentPage
        {
            get => currentPageControl.Page;
        }

        public CommandVM Connection { get; set; }
        public CommandVM MainMenu { get; set; }

        public MainVM()
        {
            currentPageControl = new CurrentPageControl();
            currentPageControl.PageChanged += CurrentPageControl_PageChanged;

            Connection = new CommandVM(() => {
                currentPageControl.SetPage(new ConnectionPage());
            });

            

        }

        private void CurrentPageControl_PageChanged(object sender, EventArgs e)
        {
            Signal(nameof(CurrentPage));
        }

    }
}
