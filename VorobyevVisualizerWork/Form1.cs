using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VorobyevVisualizerWork
{
    public partial class Form1 : Form
    {
        public static float[,,] convertedData;
        int maxValueArray;
        int minValueArray;
        int size;
        public Form1()
        {
            InitializeComponent();
        }

        private void открытьUnit8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            readerUINT8 reader = new readerUINT8();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "RAW files | *.RAW; | All Files(*.*) | *.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.FileName;
                if (File.Exists(path)) {
                    //string shortfileName = (path).Replace(".raw", "");
                    string[] tmpArray = path.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                    string fileName = tmpArray.Last();
                    tmpArray = fileName.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                    size = Int32.Parse(tmpArray.First());
                    convertedData = reader.getArray(path, size);
                    maxValueArray = getMaxValueArray(convertedData);
                    minValueArray = getMinValueArray(convertedData);
                    this.trackBar1.Maximum = size;
                    this.trackBar2.Maximum = size;
                    this.trackBar3.Maximum = size;
                    this.label3.Text = (size).ToString();
                    this.label4.Text = (size).ToString();
                    this.label5.Text = (size).ToString();
                    this.textBox1.Text = (minValueArray).ToString();
                    this.textBox2.Text = (maxValueArray).ToString();
                }                    
            }
        }




        public int getMaxValueArray(float[,,] array)
        {
            float max = float.MinValue;
            int size = (int)Math.Pow(array.Length, 1.0 / 3.0);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        if (array[i,j,k]>max)
                        {
                            max = array[i,j,k];
                        }
                    }
                }
            }
            return (int)max;
        }
        public int getMinValueArray(float[,,] array)
        {
            float min = int.MaxValue;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        if (array[i, j, k] < min)
                        {
                            min = array[i, j, k];
                        }
                    }
                }
            }
            return (int)min;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int coordinateFirstProection = this.trackBar1.Value;
            int coordinateSeconfProection = this.trackBar2.Value;
            int coordinateThirdProection = this.trackBar3.Value;
            int minValue = int.MinValue;
            int maxValue = int.MaxValue;

            if (this.textBox1.Text != "")
            {
                minValue = Int32.Parse(this.textBox1.Text);
            }
            if (this.textBox2.Text != "")
            {
                maxValue = Int32.Parse(this.textBox2.Text);
            }           
        
            Bitmap image1 = new Bitmap(size, size);
            Bitmap image2 = new Bitmap(size, size);
            Bitmap image3 = new Bitmap(size, size);

           for (int i = 0; i < size; i++)
           {
                for (int j = 0; j < size; j++)
                {
                    float arrayValue = convertedData[coordinateFirstProection, i, j];
                    Color tmp = converterArrayValueToColor(arrayValue, minValue, maxValue);
                    image1.SetPixel(i, j, tmp);
                }
            }
            this.pictureBox1.Image = image1;
            this.pictureBox1.Refresh();
            this.pictureBox1.Width = 449;
            this.pictureBox1.Height = 296;
            this.pictureBox1.Refresh();




            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    float arrayValue = convertedData[i, coordinateSeconfProection, j];
                    Color tmp = converterArrayValueToColor(arrayValue, minValue, maxValue);
                    image2.SetPixel(i, j, tmp);
                }
            }
            this.pictureBox2.Image = image2;
            this.pictureBox2.Refresh();
            this.pictureBox2.Width = 449;
            this.pictureBox2.Height = 296;
            this.pictureBox2.Refresh();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    float arrayValue = convertedData[i, j, coordinateThirdProection];
                    Color tmp = converterArrayValueToColor(arrayValue, minValue, maxValue);
                    image3.SetPixel(i, j, tmp);
                }
            }
            this.pictureBox3.Image = image3;
            this.pictureBox3.Refresh();
            this.pictureBox3.Width = 449;
            this.pictureBox3.Height = 296;
            this.pictureBox3.Refresh();
        }

        private Color converterArrayValueToColor(float arrayValue, int min, int max)
        {
            Color a = new Color();
            if(arrayValue < min)
            {
                return Color.White;
            }
            if(arrayValue > max)
            {
                return Color.Black;
            }
            float tmp = 255 * 100* arrayValue / (max - min);
            int intensity = (int)((0.36 * tmp + 0.53 * tmp + 0.11 * tmp))/3;

            if(intensity<85)
            {
                return Color.FromArgb(Clamp(intensity, 0, 255),0, 0);
            }
            if((intensity>=85 && (intensity<170)))
            {
                return Color.FromArgb(0, Clamp(intensity, 0, 255), 0);
            }
            if(intensity>170)
            {
                return Color.FromArgb(0, 0, Clamp(intensity, 0, 255));
            }

            a = Color.FromArgb(Clamp(intensity,0,255), Clamp(intensity, 0, 255), Clamp(intensity, 0, 255));
            return a;
        }
        public int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }

        private void открытьFloat32ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            readerFLOAT32 reader = new readerFLOAT32();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "RAW files | *.RAW; | All Files(*.*) | *.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.FileName;
                if (File.Exists(path))
                {
                    string[] tmpArray = path.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                    string fileName = tmpArray.Last();
                    tmpArray = fileName.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                    size = Int32.Parse(tmpArray.First());
                    convertedData = reader.getArray(path, size);
                    maxValueArray = getMaxValueArray(convertedData);
                    minValueArray = getMinValueArray(convertedData);
                    this.trackBar1.Maximum = size;
                    this.trackBar2.Maximum = size;
                    this.trackBar3.Maximum = size;
                    this.label3.Text = (size).ToString();
                    this.label4.Text = (size).ToString();
                    this.label5.Text = (size).ToString();
                    this.textBox1.Text = (minValueArray).ToString();
                    this.textBox2.Text = (maxValueArray).ToString();
                }
            }
        }
    }
}
