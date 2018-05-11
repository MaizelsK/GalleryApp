using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace GalleryApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<MyImage> images;
        private ScrollViewer imagesListScroll;
        public MainWindow()
        {
            images = new ObservableCollection<MyImage>();
            InitializeComponent();

            ImagesList.ItemsSource = images;

            Thread thread = new Thread(new ThreadStart(DownloadImages));
            thread.Start();
        }

        private void DownloadImages()
        {
            string imagesPath = @"\\second-dc\ПКО\SMP-171\Test";
            //string imagesPath = @"C:\Users\МайзельсК\Documents\Visual Studio 2017\Projects\GalleryApp\Images";

            if (Directory.Exists(imagesPath))
            {
                IEnumerable<string> filesPaths = Directory.EnumerateFiles(imagesPath);

                Parallel.ForEach(filesPaths, filePath =>
                {
                    FileInfo file = new FileInfo(filePath);

                    Dispatcher.Invoke(() =>
                    {
                        images.Add(new MyImage
                        {
                            FileName = file.Name,
                            FilePath = filePath
                        });
                    });
                });

                Dispatcher.Invoke(() =>
                {
                    Border border = (Border)VisualTreeHelper.GetChild(ImagesList, 0);
                    imagesListScroll = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                    imagesListScroll.ScrollChanged += ImagesListScroll_ScrollChanged;
                });
            }
        }

        private void ImagesListScroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            double offset = imagesListScroll.VerticalOffset;

            double heigth = imagesListScroll.ScrollableHeight;
        }
    }
}
