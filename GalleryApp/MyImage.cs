using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace GalleryApp
{
    public class MyImage
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public Uri Source { get; set; }

        public MyImage()
        {
            //Source = new Uri(@"D:\Visual projects\GalleryApp\loading.gif");
            Source = new Uri(Directory.GetCurrentDirectory() + "\\loading4.gif");
        }

        public void GetImage()
        {
            if (Source == null)
            {
                Thread thread = new Thread(new ThreadStart(DownloadImage));
                thread.Start();
            }
        }

        public void DownloadImage()
        {
            Source = new Uri(FilePath);
        }
    }
}