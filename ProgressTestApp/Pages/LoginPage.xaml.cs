using ProgressTestApp.Functions;
using ProgressTestApp.HTTP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ProgressTestApp.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void DoLogin(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = Username.Text;
                string password = Password.Password;

                await HttpUserControls.Login(username, password);

                await HttpFileControls.GetHomeFolder();
                if (this.NavigationService != null)
                {
                    NavigationService.Navigate(new MainPage());
                }
            }
            catch(Exception err)
            {
                Warnings.ShowError(err.Message);
            }
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                DoLogin(sender, e);
            }
        }
    }
}
