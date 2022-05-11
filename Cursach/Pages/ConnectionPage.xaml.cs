using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cursach.ViewModel;

namespace Cursach.Pages
{
    /// <summary>
    /// Логика взаимодействия для ConnectionPage.xaml
    /// </summary>
    public partial class ConnectionPage : Page
    {
        public ConnectionPage()
        {
            InitializeComponent();
            DataContext = new MainVM();
            DataContext = new Connection(passwordBox);
            
        }

        
    }
}
