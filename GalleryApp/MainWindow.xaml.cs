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
using WpfAnimatedGif;

namespace GalleryApp
{
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
            //string imagesPath = @"C:\Users\MAIZELS\Pictures\Covers";
            //string imagesPath = @"C:\Users\MAIZELS\Pictures\ITE";

            if (Directory.Exists(imagesPath))
            {
                IEnumerable<string> filesPaths = Directory.EnumerateFiles(imagesPath);

                Parallel.ForEach(filesPaths, filePath =>
                {
                    FileInfo file = new FileInfo(filePath);

                    if (file.Extension != ".db")
                    {
                        Dispatcher.Invoke(() =>
                        {
                            MyImage newImage = new MyImage
                            {
                                FileName = file.Name,
                                FilePath = filePath
                            };

                            images.Add(newImage);
                        });
                    }
                });

                Dispatcher.Invoke(() =>
                {
                    Border border = (Border)VisualTreeHelper.GetChild(ImagesList, 0);
                    imagesListScroll = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                    imagesListScroll.ScrollChanged += ImagesListScroll_ScrollChanged;
                    imagesListScroll.CanContentScroll = false;
                });
            }
        }

        private void ImagesListScroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            double scrollActualHeigth = imagesListScroll.ActualHeight; // Текущая высота скрола
            double VerticalOffset = imagesListScroll.VerticalOffset; // Позиция скрола по ввертикали
            double scrollableHeight = imagesListScroll.ScrollableHeight; // Максимальная позиция скрола
            double listActualHeight = ImagesList.ActualHeight; // Высота списка картинок

            int scrollPosPercent = (int)((VerticalOffset * 100) / scrollableHeight);
            int itemsCount = ImagesList.Items.Count;
            int visibleItemsCount = (int)(scrollActualHeigth / 100);
            int imagesToDownloadPercent = itemsCount / 100;

            //for (int i = 0; i < 8; i++)
            //{
            //    ((MyImage)(ImagesList.Items[i])).GetImage();
            //}
        }
    }
}
