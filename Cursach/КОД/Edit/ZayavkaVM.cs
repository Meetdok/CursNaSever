using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Cursach.DTO;
using Cursach.Model;
using Cursach.Tools;
using Cursach.ViewModel;
using Cursach.Данные;
using Cursach.Страницы.View;

namespace Cursach.КОД.Edit
{
    class ZayavkaVM : BaseVM
    {
        public Zayavka EditZay { get; }
        public CommandVM SaveZay { get; set; }
        public User ZayUser
        {
            get => zayUser;
            set
            {
                zayUser = value;
                Signal();
            }
        }

        public Employ ZayEmploy
        {
            get => zayEmploy;
            set
            {
                zayEmploy = value;
                Signal();
            }
        }

        public List<User> Users { get; set; }
        public List<Employ> Employs { get; set; }

        private CurrentPageControl currentPageControl;
        private User zayUser;
        private Employ zayEmploy;

        public ZayavkaVM(CurrentPageControl currentPageControl)
        {
            this.currentPageControl = currentPageControl;
            EditZay = new Zayavka();
            Init();
        }

        public ZayavkaVM(Zayavka editZay, CurrentPageControl currentPageControl)
        {
            EditZay = editZay;
            this.currentPageControl = currentPageControl;
            Init();
            ZayUser = Users.FirstOrDefault(s => s.ID == editZay.UserId);
            ZayEmploy = Employs.FirstOrDefault(s => s.ID == editZay.EmployerId);
        }

        private void Init()
        {
            Users = SqlModel.GetInstance().UserCreate(0, 100);
            Employs = SqlModel.GetInstance().EmployCreate(0, 100);
            SaveZay = new CommandVM(() => {
                if (ZayUser == null || ZayEmploy == null)
                {
                    MessageBox.Show("Введены не все данные");
                    return;
                }
                EditZay.UserId = ZayUser.ID;
                EditZay.EmployerId = ZayEmploy.ID;
                var model = SqlModel.GetInstance();
                if (EditZay.ID == 0)
                    model.Insert(EditZay);
                else
                    model.Update(EditZay);
                currentPageControl.SetPage(new ZayavkaView(ZayUser, ZayEmploy));
            });
        }
    }
}
