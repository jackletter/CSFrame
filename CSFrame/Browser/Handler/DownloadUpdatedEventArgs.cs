using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSFrame.Browser
{
    public class DownloadUpdatedEventArgs : EventArgs
    {
        public DownloadItem downloadItem { get; set; }
        public IDownloadItemCallback callback { get; set; }
    }
}