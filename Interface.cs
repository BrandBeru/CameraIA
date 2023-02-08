using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public Interface()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if(file.ShowDialog() == DialogResult.OK)
            {
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
    }
}
