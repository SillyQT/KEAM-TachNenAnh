using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEAM_Anh
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Đặt giá trị tối thiểu và tối đa cho NumericUpDown
            //numericUpDown1.Minimum = 1; // Giá trị tối thiểu
            //numericUpDown1.Maximum = 30; // Giá trị tối đa
        }

        KMean KMeanRun = new KMean();
        public int soCum;

        private void btn_Taianh_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Title = "Mở file Ảnh";
                    dialog.Filter = "Files Ảnh (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        pictureBox1.Image = new Bitmap(dialog.FileName);
                        btn_Xoa1.Enabled = true;
                        btn_Clear.Enabled = true;

                    }
                }
            }
            catch
            {
                MessageBox.Show("Ảnh bị lỗi !.", "LỖI!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_XulyKMean_Click(object sender, EventArgs e)
        {
            soCum = (int)numericUpDown1.Value;

            pictureBox2.Image = KMeanRun.PhanDoanHinhAnh(new Bitmap(pictureBox1.Image), soCum);
            btn_Luu2.Enabled = true;
            btn_Xoa2.Enabled = true;

            pictureBox3.Image = KMeanRun.ChuyenTrangDen(new Bitmap(pictureBox2.Image));
            btn_Luu3.Enabled = true;
            btn_Xoa3.Enabled = true;

            btn_MauNen.Enabled = true;
            btn_Tachnen.Enabled = true;
            btn_Clear.Enabled = true;
        }

        private void btn_MauNen_Click(object sender, EventArgs e)
        {
            pictureBox5.Image = KMeanRun.PhanDoanHinhAnh(new Bitmap(pictureBox3.Image), 2);
        }

        private void btn_Tachnen_Click(object sender, EventArgs e)
        {
            pictureBox4.Image = KMeanRun.TachNen(new Bitmap(pictureBox3.Image), new Bitmap(pictureBox5.Image).GetPixel(0,0),new Bitmap(pictureBox1.Image));
            btn_Luu4.Enabled = true;
            btn_Xoa4.Enabled = true;

            btn_Clear.Enabled=true;
        }

        private void btn_Luu2_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox2.Image == null)
                    throw new ArgumentNullException();

                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Title = "Open image";
                    dialog.Filter = "Image Files (*.png)|*.PNG";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox2.Image.Save(dialog.FileName);
                    }
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Không tìm thấy hình ảnh.", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Luu3_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox3.Image == null)
                    throw new ArgumentNullException();

                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Title = "Open image";
                    dialog.Filter = "Image Files (*.png)|*.PNG";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox3.Image.Save(dialog.FileName);
                    }
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Không tìm thấy hình ảnh.", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Luu4_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox4.Image == null)
                    throw new ArgumentNullException();

                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Title = "Open image";
                    dialog.Filter = "Image Files (*.png)|*.PNG";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox4.Image.Save(dialog.FileName);
                    }
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Không tìm thấy hình ảnh.", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Xoa1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            btn_Xoa1.Enabled = false;
        }

        private void btn_Xoa2_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
            btn_Xoa2.Enabled = false;
        }

        private void btn_Xoa3_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            btn_Xoa3.Enabled = false;
        }

        private void btn_Xoa4_Click(object sender, EventArgs e)
        {
            pictureBox4.Image = null;
            btn_Xoa4.Enabled = false;
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = pictureBox2.Image = pictureBox3.Image = pictureBox4.Image = pictureBox5.Image = null;
            btn_Clear.Enabled = false;
            btn_Xoa1.Enabled = btn_Xoa2.Enabled = btn_Xoa3.Enabled = btn_Xoa4.Enabled = false;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            soCum = (int)numericUpDown1.Value;
        }

        
    }
}
