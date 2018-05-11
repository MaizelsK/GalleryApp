using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GalleryApp
{
    public class MyImage
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public BitmapImage Image { get; set; }

        public void GetImage()
        {
            if (Image == null)
            {
                Thread thread = new Thread(new ThreadStart(DownloadImage));
                thread.Start();
            }
        }

        private void DownloadImage()
        {
            Image = new BitmapImage(new Uri(FilePath));
        }
    }
}
