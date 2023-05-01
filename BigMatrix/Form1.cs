using System.Diagnostics;
using System.Drawing;
using System.Globalization;

namespace BigMatrix
{
    public partial class Form1 : Form
    {
        string _size;
        Random r = new Random();
        Graphics g;
        static int k = 1;
        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int diagram_width = panel1.Width / 8;

            List<double> time = new List<double>();

            Stopwatch sw = new Stopwatch();

            int size = int.Parse(_size);

            QuadrateMatrix matrix = new QuadrateMatrix(size, size);

            QuadrateMatrix multy_matrix;

            for (int i = 1; i < 9; i++)
            {
                QuadrateMatrix.threadCount = i;

                sw.Restart();

                multy_matrix = matrix * matrix;

                sw.Stop();

                time.Add((int)sw.ElapsedMilliseconds);

            }

            double[] cloned_time = new double[time.Count];

            time.CopyTo(cloned_time, 0);

            time.Sort();

            List<double> coeffs_for_biggest = new List<double>();

            for (int i = 0; i < time.Count; i++)
            {
                coeffs_for_biggest.Add(time[time.Count - 1] / time[i]);
            }
            coeffs_for_biggest.Sort();

            Brush brush = new SolidBrush(Color.FromArgb(r.Next(255), r.Next(255), r.Next(255), r.Next(255)));

            g.FillRectangle(brush, new Rectangle(0, 1, diagram_width, 340));

            for (int k = 1; k < 8; k++)
            {
                brush = new SolidBrush(Color.FromArgb(r.Next(255), r.Next(255), r.Next(255), r.Next(255)));

                g.FillRectangle(brush, new Rectangle(diagram_width * (k + 1) - diagram_width, panel1.Height - (int)(341 / coeffs_for_biggest[k]), diagram_width, panel1.Height - (panel1.Height - (int)(341 / coeffs_for_biggest[k]))));
            }

            int z = 0;

            int[] index = new int[8];

            int p = 0; 

            while (p != 8)
            {
                for (int j = 0; j < cloned_time.Length; j++)
                {
                    if (cloned_time[j] == time[p])
                    {
                        index[z] = j;

                        z++;

                        j = 0;

                        p++;

                        if (p == 8)
                        {
                            break;
                        }
                    }
                }
            }


            label2.Text = time[time.Count - 1].ToString() + "ms" + '\n' + (index[7] + 1).ToString();

            label3.Text = time[time.Count - 2].ToString() + "ms" + '\n' + (index[6] + 1).ToString();

            label4.Text = time[time.Count - 3].ToString() + "ms" + '\n' + (index[5] + 1).ToString();

            label5.Text = time[time.Count - 4].ToString() + "ms" + '\n' + (index[4] + 1).ToString();

            label6.Text = time[time.Count - 5].ToString() + "ms" + '\n' + (index[3] + 1).ToString();

            label7.Text = time[time.Count - 6].ToString() + "ms" + '\n' + (index[2] + 1).ToString();

            label8.Text = time[time.Count - 7].ToString() + "ms" + '\n' + (index[1] + 1).ToString();

            label9.Text = time[time.Count - 8].ToString() + "ms" + '\n' + (index[0] + 1).ToString();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _size = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}