using Microsoft.Win32;
using ProgressTestApp.Functions;
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
        }
        void GetFilePath(object sender, RoutedEventArgs e)
        {
            string filePath = FilePath.Text;
            if (IO_Path.Exists(filePath))
            {
                if (!isWatching)
                {
                    FileManagerToggle.Content = "Stop Tracking";

                    watcher = new FileSystemWatcher(filePath);
                    watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName;
                    watcher.Changed += new FileSystemEventHandler(Changed);
                    watcher.Created += new FileSystemEventHandler(FileCreated);
                    watcher.Deleted += new FileSystemEventHandler(Deleted);
                    watcher.Renamed += new RenamedEventHandler(Renamed);
                    watcher.EnableRaisingEvents = true;
                }
                else
                {
                    watcher = null;
                    FileManagerToggle.Content = "Start Watching file";
                }

                isWatching = !isWatching;
            }
            else
            {
                Warnings.ShowError("The Text Entered is not a valid path", "Invalid Path");
            }
        }

        private void Renamed(object sender, RenamedEventArgs e)
        {
            Debug.WriteLine("File Renamed" + e.FullPath.ToString());
        }

        private void Deleted(object sender, FileSystemEventArgs e)
        {
            Debug.WriteLine("File Deleted: " + e.FullPath.ToString());
        }

        private void FileCreated(object sender, FileSystemEventArgs e)
        {
            Debug.WriteLine("File created: " + e.FullPath.ToString());
        }

        private void Changed(object sender, FileSystemEventArgs e)
        {
            Debug.WriteLine("Change made to " + e.FullPath.ToString());
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
