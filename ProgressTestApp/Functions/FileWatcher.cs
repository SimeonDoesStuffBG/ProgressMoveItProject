using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressTestApp.Functions
{
    class FileWatcher
    {
        FileSystemWatcher fileWatcher;

        public FileWatcher(string path)
        {
            fileWatcher = new FileSystemWatcher(path);
        }
    }
}
