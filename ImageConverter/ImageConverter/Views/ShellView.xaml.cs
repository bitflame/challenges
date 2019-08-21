using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace ImageConverter.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            String[] fileNames = Directory.GetFiles(path);
            foreach (var name in fileNames)
            {

                if (name.EndsWith(".png"))
                {
                    System.Drawing.Image image = System.Drawing.Image.FromFile(name);
                    String tempName = name.Remove(name.LastIndexOf("."));
                    image.Save(tempName +".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    image.Save(tempName +".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                    image.Save(tempName +".gif", System.Drawing.Imaging.ImageFormat.Gif);
                    image.Save(tempName +".tiff", System.Drawing.Imaging.ImageFormat.Tiff);
                }
            }
            
            //ImageFormat class can convert to so many different types definately read the documentation
        }
    }
}
