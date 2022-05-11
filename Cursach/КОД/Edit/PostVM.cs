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
    public class PostVM : BaseVM
    {
        public Post EditPost { get; }
        public CommandVM SavePost { get; set; }
        public Department PostDep
        {
            get => postDep;
            set
            {
                postDep = value;
                Signal();
            }
        }

        public List<Department> Deps { get; set; }

        private CurrentPageControl currentPageControl;
        private Department postDep;

        public PostVM(CurrentPageControl currentPageControl)
        {
            this.currentPageControl = currentPageControl;
            EditPost = new Post();
            Init();
        }

        public PostVM(Post editPost, CurrentPageControl currentPageControl)
        {
            EditPost = editPost;
            this.currentPageControl = currentPageControl;
            Init();
            PostDep = Deps.FirstOrDefault(s => s.ID == editPost.DepartmentId);
        }

        private void Init()
        {
            Deps = SqlModel.GetInstance().DepartmentCreate(0, 100);
            SavePost = new CommandVM(() => {
                if (PostDep == null)
                {
                    System.Windows.MessageBox.Show("Нужно выбрать должность для продолжения");
                    return;
                }
                EditPost.DepartmentId = PostDep.ID;
                var model = SqlModel.GetInstance();
                if (EditPost.ID == 0)
                    model.Insert(EditPost);
                else
                    model.Update(EditPost);
                currentPageControl.SetPage(new PostView(PostDep));
            });
        }

    }
}
