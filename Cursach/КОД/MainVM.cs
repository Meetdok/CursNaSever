
using System;
using System.Windows.Controls;
using Cursach.Pages;
using Cursach.Tools;
using Cursach.КОД.Edit;
using Cursach.Страницы.Edit;
using Cursach.Страницы.View;

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
        
        public CommandVM Department { get; set; }
        public CommandVM ViewDepartment { get; set; }

        public CommandVM Post { get; set; }
        public CommandVM ViewPost { get; set; }

        public CommandVM Employ { get; set; }
        public CommandVM ViewEmploy { get; set; }

        public CommandVM User { get; set; }
        public CommandVM ViewUser { get; set; }

        public CommandVM Zayavka { get; set; }
        public CommandVM ViewZayavka { get; set; }

        public MainVM()
        {
            currentPageControl = new CurrentPageControl();
            currentPageControl.PageChanged += CurrentPageControl_PageChanged;

            Connection = new CommandVM(() =>
            {
                currentPageControl.SetPage(new ConnectionPage(currentPageControl));
            });

            Department = new CommandVM(() =>
            {
                currentPageControl.SetPage(new DepPage(new DepVM(currentPageControl)));
            });

            ViewDepartment = new CommandVM(() =>
            {
                currentPageControl.SetPage(new DepView(currentPageControl));
            });

            Post = new CommandVM(() =>
            {
                currentPageControl.SetPage(new PostPage(new PostVM(currentPageControl)));
            });

            ViewPost = new CommandVM(() =>
            {
                currentPageControl.SetPage(new PostView(null));
            });

            Employ = new CommandVM(() =>
            {
                currentPageControl.SetPage(new EmployPage(new EmployVM(currentPageControl)));
            });

            ViewEmploy = new CommandVM(() =>
            {
                currentPageControl.SetPage(new EmployView(null));

            });

            User = new CommandVM(() =>
            {
                currentPageControl.SetPage(new UserPage(new UserVM(currentPageControl)));
            });

            ViewUser = new CommandVM(() =>
            {
                currentPageControl.SetPage(new UserView(currentPageControl));
            });

            
        }

        
        private void CurrentPageControl_PageChanged(object sender, EventArgs e)
        {
            Signal(nameof(CurrentPage));
        }

    }
}
