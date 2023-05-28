using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraIA
{
    public partial class Interface : Form
    {
        string path = "";

        public static string predictionText;
        public static string percentageText;

        VideoCapture capture;
        Mat frame;
        Bitmap image;

        private Thread camera;
        private void CaptureCamera()
        {
            camera = new Thread(new ThreadStart(this.CaptureCameraCallBack));
            camera.Start();
        }
        private void CaptureCameraCallBack()
        {
            frame = new Mat();
            capture = new VideoCapture(0);
            capture.Open(0);

            if (capture.IsOpened())
            {
                while(isCameraRunning)
                {
                    capture.Read(frame);
                    image = frame.ToBitmap();
                    
                    picture.Image = image;
                }
            }
        }
        bool isCameraRunning = false;
        public Interface()
        {
            InitializeComponent();
            CaptureCamera();
            isCameraRunning= true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if(file.ShowDialog() == DialogResult.OK)
            {
                isCameraRunning = false;
                path = file.FileName;
                imagePath.Text = path;

                Bitmap image = new Bitmap(path);
                picture.Image = image;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ImagePrediction prediction = new ImagePrediction();
            outputText.Text = prediction.StartPrediction(path);

            predictionTXT.Text = predictionText;
            percentageTXT.Text = percentageText;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int value = new Random().Next(0, 500);
            if (isCameraRunning)
            {
                Bitmap snapshot = new Bitmap(image);
                string imagePath = string.Format(@"R:\_IA\image" + value + ".jpg", Guid.NewGuid());
                snapshot.Save(imagePath, ImageFormat.Jpeg);
                capture.Release();
                path = imagePath;
                isCameraRunning= false;
                camera.Interrupt(); 
            }
        }
    }
}
