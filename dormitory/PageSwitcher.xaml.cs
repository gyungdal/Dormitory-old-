using Dormitory.Pages;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace Dormitory
{
    /// <summary>
    /// Interaction logic for PageSwitcher.xaml
    /// </summary>
    public partial class PageSwitcher : Window
    {
        private static PageSwitcher switcher = null;
        public enum View { LOGIN = 0, MAIN, CALENDAR};
        public static int changeView;
        public PageSwitcher()
        {
            InitializeComponent();

            switcher = this;
            SetPage(View.LOGIN);
        }

        public static void SetTitle(String title)
        {
            switcher.Title = title;
        }
        public static void SetPage(View change){
                switcher.main.Children.Clear();
            UserControl page = null;
            changeView = (int)change;
            switch ((View)changeView) {
                case View.LOGIN:
                    page = new LoginPage();
                    break;
                case View.MAIN:
                    page = new MainPage();
                    break;
                default: break;
            }
            if (page != null)
                switcher.main.Children.Add(page);
        }
    }
}
