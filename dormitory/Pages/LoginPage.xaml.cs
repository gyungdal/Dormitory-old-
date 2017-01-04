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
using System.Collections;
using System.IO;
using System.Net;
using System.Windows.Media.Animation;
using Newtonsoft.Json.Linq;

namespace Dormitory.Pages
{
    /// <summary>
    /// Interaction logic for InitializePage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {
        public LoginPage()
        {
            InitializeComponent();
            PageSwitcher.SetTitle(this.GetType().Name);
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            //checkLogin();

            Panel.SetZIndex(panel, 0);

            ChangeOpacity(panel, 1, 0);
            MoveAnim(panel, 0, loginPanel.TransformToAncestor(this).Transform(new Point(0, 0)).Y);
            //createRepo();
        }

        private void checkLogin()
        {   
            Hashtable param = new Hashtable();
            param.Add("id", emailTextbox.Text);
            param.Add("password", pwTextBox.Password);

            string querystring = paramToString(param);
            string url = "http://localhost:8080/main/trylogin";

            string responseFromServer = "";
            HttpWebRequest Hwr = (HttpWebRequest)WebRequest.Create(url);
            Hwr.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            Hwr.Method = "POST";

            System.IO.Stream str = Hwr.GetRequestStream();
            System.IO.StreamWriter stwr = new System.IO.StreamWriter(str, new UTF8Encoding(false));
            stwr.Write(querystring);

            stwr.Flush(); stwr.Close(); stwr.Dispose();
            str.Flush(); str.Close(); str.Dispose();

            HttpWebResponse response = (HttpWebResponse)Hwr.GetResponse();

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);

            responseFromServer = reader.ReadToEnd();
        }

        private void passwordTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                LoginButton_Click(null, null);
        }

        /*  */
        private void registerText_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Panel.SetZIndex(shadowPanel, 2);
            Panel.SetZIndex(modal, 2);
            ChangeOpacity(shadowPanel, 0, 0.5);
            ChangeOpacity(modal, 0.2, 1);
            r_emailTextbox.Focus();
            MoveAnim(modal, loginPanel.TransformToAncestor(this).Transform(new Point(-5, 0)).X, loginPanel.TransformToAncestor(this).Transform(new Point(0, 0)).Y);
            modal.KeyDown += registerView_keyListener;
        }


        private void registerView_keyListener(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Panel.SetZIndex(shadowPanel, 0);
                Panel.SetZIndex(modal, 0);
                ChangeOpacity(shadowPanel, 0.5, 0);
                ChangeOpacity(modal, 1,  0);
                modal.KeyDown -= registerView_keyListener;
            }
        }

        private void registAccount()
        {
            Hashtable param = new Hashtable();
            param.Add("id", emailTextbox.Text);
            param.Add("password", pwTextBox.Password);

            string querystring = paramToString(param);
            string url = "http://localhost:8080/main/trysignup";

            string responseFromServer = "";
            HttpWebRequest Hwr = (HttpWebRequest)WebRequest.Create(url);
            Hwr.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            Hwr.Method = "POST";

            System.IO.Stream str = Hwr.GetRequestStream();
            System.IO.StreamWriter stwr = new System.IO.StreamWriter(str, new UTF8Encoding(false));
            stwr.Write(querystring);

            stwr.Flush(); stwr.Close(); stwr.Dispose();
            str.Flush(); str.Close(); str.Dispose();

            HttpWebResponse response = (HttpWebResponse)Hwr.GetResponse();

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);

            responseFromServer = reader.ReadToEnd();
        }
        
        private string paramToString(Hashtable parameters)
        {
            string str = "";
            foreach (DictionaryEntry entry in parameters)
            {
                if (str.Length != 0)
                    str += "&";
                str += entry.Key + "=" + Uri.EscapeDataString(entry.Value.ToString());
            }

            return str;
        }

        public void MoveAnim(UIElement target, double newX, double newY)
        {
            // UIElement => Contorl
            // UIElementCollection => Grid, Panel

            Vector offset = VisualTreeHelper.GetOffset(target);

            var top = offset.Y;
            var left = offset.X;

            TranslateTransform trans = new TranslateTransform();
            target.RenderTransform = trans;

            DoubleAnimation anim1 = new DoubleAnimation(0, newY - top, TimeSpan.FromSeconds(1));
            anim1.AccelerationRatio = 0.1;
            anim1.DecelerationRatio = 0.9;
            DoubleAnimation anim2 = new DoubleAnimation(0, newX - left, TimeSpan.FromSeconds(1));
            anim2.AccelerationRatio = 0.1;
            anim2.DecelerationRatio = 0.9;
            anim2.Completed += (s, e) =>
            {
                if(target == panel)
                    PageSwitcher.SetPage(PageSwitcher.View.MAIN);
            };

            trans.BeginAnimation(TranslateTransform.YProperty, anim1);
            trans.BeginAnimation(TranslateTransform.XProperty, anim2);
        }
        
        public void ChangeOpacity(UIElement target, double from, double to)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = from;
            da.To = to;
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            target.BeginAnimation(OpacityProperty, da);
        }
    }
}
