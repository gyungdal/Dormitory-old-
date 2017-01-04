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
using System.IO;
using Newtonsoft.Json.Linq;

namespace Dormitory.Pages
{
    /// <summary>
    /// MainPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            PageSwitcher.SetTitle(this.GetType().Name);
        }

        private void MenuHandler(object sender, MouseButtonEventArgs e)
        {
            Label item = sender as Label;
            Console.WriteLine(item.Content.ToString().ToUpper());
            switch (item.Content.ToString().ToUpper())
            {
                case "TRANSLATE":
                    break;
                case "CALENDAR":
                    break;
            }
        }
    }
}
