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
using System.Windows.Navigation;
using System.Windows.Shapes;


using Emgu.CV;
using Emgu.CV.Structure;
using System.Runtime.InteropServices;
using Emgu.CV.CvEnum;

namespace MultiViewerEmguExampleWpf
{
    public partial class MainWindow : Window
    {
        VideoCapture m_capture = new VideoCapture();

        public MainWindow()
        {
            InitializeComponent();

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();

            m_capture = new VideoCapture();
        }




        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            using (Image<Bgr, byte> nextFrame = m_capture.QueryFrame().ToImage<Bgr, Byte>())
            {
                if (nextFrame != null)
                {
                    //  Image<Gray, byte> grayframe = nextFrame.Convert<Gray, byte>();

                    //   var faces = m_cascade.DetectMultiScale(grayframe, 1.1, 3, new System.Drawing.Size(20, 20));

                    //foreach (var face in faces)
                    //{
                    //    nextFrame.Draw(face, new Bgr(0, 0, 0), 3);
                    //}



                    displayCameraViewOne(nextFrame.Clone());
                    displayCameraViewTwo(nextFrame.Clone());
                    displayCameraViewThree(nextFrame.Clone());
                    displayCameraViewFour(nextFrame.Clone());

                    //TestImage2.Source = ToBitmapSource(nextFrame.Clone());
                    //TestImage3.Source = ToBitmapSource(nextFrame.Clone());
                    //TestImage4.Source = ToBitmapSource(nextFrame.Clone());

                    //  TestImage2.Source = ToBitmapSource(nextFrame);

                }
            }
        }

        private void displayCameraViewOne(Image<Bgr, byte> nextFrame1)
        {

           // Image<Bgr, byte> copy = nextFrame1;
       
            CvInvoke.PutText(
            nextFrame1,
            "CameraView One",
            new System.Drawing.Point(10, 80),
            FontFace.HersheyComplex,
            2.0,
            new Bgr(0, 255, 0).MCvScalar);



  double fps = m_capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);
            
            
            CvInvoke.PutText(
        nextFrame1,
       "FPS: " + fps.ToString(),
        new System.Drawing.Point(10, 160),
        FontFace.HersheyComplex,
        2.0,
        new Bgr(0, 255, 0).MCvScalar);


          


            TestImage1.Source = ToBitmapSource(nextFrame1);
        }

        private void displayCameraViewTwo(Image<Bgr, byte> nextFrame1)
        {

            // Image<Bgr, byte> copy = nextFrame1;

            CvInvoke.PutText(
            nextFrame1,
            "CameraView Two",
            new System.Drawing.Point(10, 80),
            FontFace.HersheyComplex,
            2.0,
            new Bgr(0, 255, 0).MCvScalar);

            TestImage2.Source = ToBitmapSource(nextFrame1);
        }

        private void displayCameraViewThree(Image<Bgr, byte> nextFrame1)
        {

            // Image<Bgr, byte> copy = nextFrame1;

            CvInvoke.PutText(
            nextFrame1,
            "CameraView Three",
            new System.Drawing.Point(10, 80),
            FontFace.HersheyComplex,
            2.0,
            new Bgr(0, 255, 0).MCvScalar);

            TestImage3.Source = ToBitmapSource(nextFrame1);
        }

        private void displayCameraViewFour(Image<Bgr, byte> nextFrame1)
        {

            // Image<Bgr, byte> copy = nextFrame1;

            CvInvoke.PutText(
            nextFrame1,
            "CameraView Four",
            new System.Drawing.Point(10, 80),
            FontFace.HersheyComplex,
            2.0,
            new Bgr(0, 255, 0).MCvScalar);

            TestImage4.Source = ToBitmapSource(nextFrame1);
        }



        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        public static BitmapSource ToBitmapSource(Image<Bgr, Byte> image)
        {
            using (System.Drawing.Bitmap source = image.ToBitmap())
            {
                IntPtr ptr = source.GetHbitmap(); //obtain the Hbitmap  

                BitmapSource bs = System.Windows.Interop
                  .Imaging.CreateBitmapSourceFromHBitmap(
                  ptr,
                  IntPtr.Zero,
                  Int32Rect.Empty,
                  System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

                DeleteObject(ptr); //release the HBitmap  
                return bs;
            }
        }



    }
}
