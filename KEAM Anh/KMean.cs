using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEAM_Anh
{
    internal class KMean
    {

        private int KhoangCachNhoNhat(int[] KhoangCach, int k)
        {
            int minKhoangCach = 0;
            for (int i = 1; i < k; i++)
            {
                if (KhoangCach[i] < KhoangCach[minKhoangCach])
                {
                    minKhoangCach = i;
                }
            }
            return minKhoangCach;
        }


        public Bitmap PhanDoanHinhAnh_Old(Bitmap bitm, int k)
        {
            Random ranDom = new Random();
            Point[] pointTrungTam = new Point[k];
            // tạo số điểm = k  bất kì trên ảnh
            for (int i = 0; i < k; i++)
            {
                pointTrungTam[i] = new Point(ranDom.Next(bitm.Width), ranDom.Next(bitm.Height));
            }

            int[] KhoangCach = new int[k];

            for (int y = 0; y < bitm.Height; y++)
            {
                for (int x = 0; x < bitm.Width; x++)
                {

                    for (int i = 0; i < k; i++)
                    {

                        int r = Math.Abs(bitm.GetPixel(x, y).R - bitm.GetPixel(pointTrungTam[i].X, pointTrungTam[i].Y).R);
                        int g = Math.Abs(bitm.GetPixel(x, y).G - bitm.GetPixel(pointTrungTam[i].X, pointTrungTam[i].Y).G);
                        int b = Math.Abs(bitm.GetPixel(x, y).B - bitm.GetPixel(pointTrungTam[i].X, pointTrungTam[i].Y).B);

                        //Khoảng cách diểm
                        // Tìm số lượng và áp dụng công thức Euclid
                        KhoangCach[i] = (int)(Math.Sqrt(r * r + g * g) + Math.Sqrt(g * g + b * b) + Math.Sqrt(r * r + b * b));

                    }

                    int nearest = KhoangCachNhoNhat(KhoangCach, k);
                    Color clr = bitm.GetPixel(pointTrungTam[nearest].X, pointTrungTam[nearest].Y);      // take centroid color
                    bitm.SetPixel(x, y, clr);       // set pixel centroid color
                }
            }


            return bitm;
        }

        public Bitmap PhanDoanHinhAnh(Bitmap bitm, int k)
        {
            Random ranDom = new Random();
            Point[] pointTrungTam = new Point[k];

            // Tạo số điểm = k bất kì trên ảnh
            for (int i = 0; i < k; i++)
            {
                pointTrungTam[i] = new Point(ranDom.Next(bitm.Width), ranDom.Next(bitm.Height));
            }

            Bitmap resultBitmap = new Bitmap(bitm.Width, bitm.Height);

            for (int y = 0; y < bitm.Height; y++)
            {
                for (int x = 0; x < bitm.Width; x++)
                {
                    int minDistance = int.MaxValue;
                    int nearest = 0;

                    for (int i = 0; i < k; i++)
                    {
                        Color currentPixelColor = bitm.GetPixel(x, y);
                        Color centroidColor = bitm.GetPixel(pointTrungTam[i].X, pointTrungTam[i].Y);

                        int r = Math.Abs(currentPixelColor.R - centroidColor.R);
                        int g = Math.Abs(currentPixelColor.G - centroidColor.G);
                        int b = Math.Abs(currentPixelColor.B - centroidColor.B);

                        int distance = r * r + g * g + b * b;

                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            nearest = i;
                        }
                    }

                    Color nearestCentroidColor = bitm.GetPixel(pointTrungTam[nearest].X, pointTrungTam[nearest].Y);
                    resultBitmap.SetPixel(x, y, nearestCentroidColor);
                }
            }

            return resultBitmap;
        }


        public Bitmap ChuyenTrangDen(Bitmap bitm)
        {
            for (int y = 0; y < bitm.Height; y++)
            {
                for (int x = 0; x < bitm.Width; x++)
                {
                    Color pixelColor = bitm.GetPixel(x, y);
                    int grayscale = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11); // Chuyển đổi sang giá trị xám dựa trên công thức weighted grayscale

                    Color newColor = Color.FromArgb(grayscale, grayscale, grayscale); // Tạo màu mới từ giá trị xám

                    bitm.SetPixel(x, y, newColor); // Gán màu mới cho pixel tương ứng trong ảnh
                }
            }

            return bitm;
        }

        public Bitmap TachNen(Bitmap bitmap, Color targetColor, Bitmap resultBitmap)
        {


            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);

                    // So sánh màu pixel với màu target cần chuyển đổi
                    if (pixelColor.ToArgb() == targetColor.ToArgb())
                    {
                        // Đặt pixel này thành transparent
                        resultBitmap.SetPixel(x, y, Color.Transparent);
                    }
                }
            }
            return resultBitmap;
        }
    }
}