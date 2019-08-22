using System;
using System.Windows;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Reduce the resolution of the file to half
            String path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            String[] fileNames = Directory.GetFiles(path,"*.bmp");
            foreach (var name in fileNames)
            {
                Bitmap oldBitmap = new Bitmap(name);
                int old_wid = oldBitmap.Width;
                int old_hgt = oldBitmap.Height;
                
                //using (Bitmap newBitmap = new Bitmap((int)Math.Ceiling(oldBitmap.HorizontalResolution * 2), (int)Math.Ceiling(oldBitmap.VerticalResolution * 2)))
                using (Bitmap newBitmap = new Bitmap((old_wid * 2), (old_hgt * 2)))
                {
                    
                    System.Drawing.Point[] points =
                      {
                        new System.Drawing.Point(0, 0),
                        new System.Drawing.Point((int)Math.Ceiling(oldBitmap.HorizontalResolution * 2), 0),
                        new System.Drawing.Point(0, (int)Math.Ceiling(oldBitmap.VerticalResolution * 2)),
                      }; 
                    using (Graphics gr = Graphics.FromImage(newBitmap))
                    {
                        gr.DrawImage(oldBitmap, points);
                    }
                    newBitmap.SetResolution(oldBitmap.HorizontalResolution * 2, oldBitmap.VerticalResolution * 2);
                    newBitmap.Save(name + ".bmp");
                }
            }
           
            
        }

        private void buttonOpenFile_Click(object sender, RoutedEventArgs e)
        {
            Bitmap originalImage;
            Uri filePath;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                //txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
                //originalImage = new Bitmap(File.Open(openFileDialog.FileName, FileMode.Open));
                filePath = new Uri(openFileDialog.FileName);
                ImageSource imageSource = new BitmapImage(filePath);
                imgBox.Source = imageSource;
            } 
        }
    }
}
