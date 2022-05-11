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
    public class ViewEmployVM : BaseVM
    {
        private Post selectedPost;
        private List<Employ> employsByPost;

        public List<Post> Posts { get; set; }
        public Post SelectedPost
        {
            get => selectedPost;
            set
            {
                selectedPost = value;
                EmploysByPost = SqlModel.GetInstance().EmploysByPost(selectedPost);
                Signal();
            }
        }
        public List<Employ> EmploysByPost
        {
            get => employsByPost;
            set
            {
                employsByPost = value;
                Signal();
            }
        }
        public Employ SelectedEmploy{ get; set; }

        public ViewEmployVM(Post selectedPost)
        {
            SqlModel sqlModel = SqlModel.GetInstance();
            Posts = sqlModel.PostCreate(0, 100);
            SelectedPost = Posts.FirstOrDefault(s => s.ID == selectedPost?.ID);
        }
    }
}
