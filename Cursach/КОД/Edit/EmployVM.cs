using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cursach.Model;
using Cursach.Tools;
using Cursach.ViewModel;
using Cursach.Данные;
using Cursach.Страницы.View;

namespace Cursach.КОД.Edit
{
    public class EmployVM : BaseVM
    {
        public Employ EditEmploy { get; }
        public CommandVM SaveEmploy { get; set; }
        public Post EmployPost
        {
            get => employPost;
            set
            {
                employPost = value;
                Signal();
            }
        }

        public List<Post> Posts { get; set; }

        private CurrentPageControl currentPageControl;
        private Post employPost;

        public EmployVM(CurrentPageControl currentPageControl)
        {
            this.currentPageControl = currentPageControl;
            EditEmploy = new Employ();
            Init();
        }

        public EmployVM(Employ editEmploy, CurrentPageControl currentPageControl)
        {
            EditEmploy = editEmploy;
            this.currentPageControl = currentPageControl;
            Init();
            EmployPost = Posts.FirstOrDefault(s => s.ID == editEmploy.PostId);
        }

        private void Init()
        {
            Posts = SqlModel.GetInstance().PostCreate(0, 100);
            SaveEmploy = new CommandVM(() => {
                if (EmployPost == null)
                {
                    System.Windows.MessageBox.Show("Нужно выбрать должность для продолжения");
                    return;
                }
                EditEmploy.PostId = EmployPost.ID;
                var model = SqlModel.GetInstance();
                if (EditEmploy.ID == 0)
                    model.Insert(EditEmploy);
                else
                    model.Update(EditEmploy);
                currentPageControl.SetPage(new EmployView(EmployPost));
            });
        }
    }
}
