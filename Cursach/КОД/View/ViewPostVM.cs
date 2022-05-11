using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cursach.Model;
using Cursach.ViewModel;
using Cursach.Данные;

namespace Cursach.КОД.View
{
   public class ViewPostVM : BaseVM
    {
        private Department selectedDep;
        private List<Post> postsBySelectedDepartment;

        public List<Department> Deps { get; set; }
        public Department SelectedDep
        {
            get => selectedDep;
            set
            {
                selectedDep = value;
                PostsBySelectedDepartment = SqlModel.GetInstance().SelectPostByDepartment(selectedDep);
                Signal();
            }
        }
        public List<Post> PostsBySelectedDepartment
        {
            get => postsBySelectedDepartment;
            set
            {
                postsBySelectedDepartment = value;
                Signal();
            }
        }
        public Post SelectedPost { get; set; }

        public ViewPostVM(Department selectedDep)
        {
            SqlModel sqlModel = SqlModel.GetInstance();
            Deps = sqlModel.DepartmentCreate(0, 100);
            SelectedDep = Deps.FirstOrDefault(s => s.ID == selectedDep?.ID);
        }
    }
}
