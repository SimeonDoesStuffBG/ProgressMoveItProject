using Microsoft.Win32;
using ProgressTestApp.Functions;
using ProgressTestApp.HTTP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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
using IO_Path = System.IO.Path;
namespace ProgressTestApp.Pages
{
    
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        FileSystemWatcher watcher;
        bool isWatching = false;
        public MainPage()
        {
            InitializeComponent();
            UserName.Content = HttpUserControls.Username;
        }
        void ToggleTrackFolder(object sender, RoutedEventArgs e)
        {
            string filePath = FilePath.Text;
            Debug.WriteLine(HttpFileControls.HomeFolderID);
            if (IO_Path.Exists(filePath))
            {
                if (!isWatching)
                {
                    FileManagerToggle.Content = "Stop Tracking";
                    InfoLabel.Content = $"Tracking {filePath}. Files added here will be uploaded to your profile's Home folder. The name of the tracked folder will be appended at the start of the file name";
                    FilePath.IsEnabled = false;
                    BrowseButton.IsEnabled = false;
                    watcher = new FileSystemWatcher(filePath);
                    watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName;
                    watcher.Created += new FileSystemEventHandler(FileCreated);
                   
                    watcher.EnableRaisingEvents = true;
                }
                else
                {
                    watcher = null;
                    FileManagerToggle.Content = "Start Watching file";
                    InfoLabel.Content = "Tracking stopped";
                    FilePath.IsEnabled = true;
                    BrowseButton.IsEnabled = true;
                }

                isWatching = !isWatching;
            }
            else
            {
                Warnings.ShowError("The Text Entered is not a valid path", "Invalid Path");
            }
        }
        private async void FileCreated(object sender, FileSystemEventArgs e)
        {
            try 
            {
                await HttpFileControls.AddNewFile(e.FullPath,e.Name);

                Debug.WriteLine("Success");
            }
            catch(Exception err)
            {
                Warnings.ShowError(err.Message);
            }
        }

        private void DoLogout(object sender, RoutedEventArgs e)
        {
            if(this.NavigationService!=null)
            {
                this.NavigationService.Navigate(new LoginPage());
            }
        }

        private void BrowseFolders(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFolderDialog();

            openFileDialog.Title = "Select Folder to monitor";
            string folderPath = "";
            bool? isSuccessfulOpen = openFileDialog.ShowDialog();

            if(isSuccessfulOpen == true)
            {
                folderPath = openFileDialog.FolderName;
            }

            FilePath.Text = folderPath;
        }
    }
}
