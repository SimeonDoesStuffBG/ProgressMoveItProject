using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProgressTestApp.Functions
{
    class Warnings
    {
        public static void ShowError(string ErrorText, string ErrorTitle="Error")
        {
            MessageBox.Show(ErrorText, ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}
