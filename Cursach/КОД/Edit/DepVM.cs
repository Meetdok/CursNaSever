using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cursach.Model;
using Cursach.Tools;
using Cursach.Данные;

namespace Cursach.КОД.Edit
{
    public class DepVM 
    {
        public Department EditDep { get; set; }
        public CommandVM SaveDep { get; set; }

        private CurrentPageControl currentPageControl;
        public DepVM(CurrentPageControl currentPageControl)
        {
            this.currentPageControl = currentPageControl;
            EditDep = new Department();
            InitCommand();
        }
        public DepVM(Department editDep, CurrentPageControl currentPageControl)
        {
            this.currentPageControl = currentPageControl;
            EditDep = editDep;
            InitCommand();
        }

        private void InitCommand()
        {
            SaveDep = new CommandVM(() => {
                var model = SqlModel.GetInstance();
                if (EditDep.ID == 0)
                    model.Insert(EditDep);
                else
                    model.Update(EditDep);
                currentPageControl.Back();
            });
        }
    }
}
