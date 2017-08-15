using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Filtering
{
    public class GaussianBlur : BaseBlurFilter
    {
        private int totalWeight = 0;
        private double _sigma = 1.4;

        public double Sigma { get; }

        public GaussianBlur(Size size):this(new SquareMatrix<int>(size.Width), 1.4){} //defaul sigma value =1.4

        public GaussianBlur(Size size, double sigma):this(new SquareMatrix<int>(size.Width),sigma){}

        public GaussianBlur(SquareMatrix<int> kernel, double sigma) : base(kernel)
        {
            if (kernel.Width != kernel.Height) return;

            _sigma = sigma;
            CalculateWeightedKernel();
        }

        public override Image Apply(Image image)
        {
            if (image == null || image.Source == null || kernel == null || kernel.Width != kernel.Height) return null;

            Bitmap bitmap = new Bitmap(image.Width, image.Height,image.PixelFormat);
            Image result = new Image(bitmap);
            result.Open(AccessMode.ReadWrite);
            object padlock = new object();

            int height = image.Height;
            int width = image.Width;
            int depth = image.Depth;
            int rowsize = width * depth;
            unsafe
            {
                Parallel.For(offset, height - offset, Y =>
                {
                    byte* row = image.GetRowBytes(Y);
                    byte* resultRow = result.GetRowBytes(Y);
                    Parallel.For(depth, rowsize - (depth - 1), i =>
                    {
                        if (i % depth != 0) return;

                        double totalRed = 0.0;
                        double totalGreen = 0.0;
                        double totalBlue = 0.0;

                        byte* offsetItemPtr = row + sizeof(byte) * i;

                        for (int filterY = -offset; filterY <= offset; filterY++)
                        {
                            for (int filterX = -offset; filterX <= offset; filterX++)
                            {
                                byte* filterItemPtr = offsetItemPtr + sizeof(byte) * ((image.RowSize * filterY) + (filterX * image.Depth));

                                totalRed += *(filterItemPtr + sizeof(byte) * 2) * kernel.GetItem(filterX + offset, filterY + offset);
                                totalGreen += *(filterItemPtr + sizeof(byte) * 1) * kernel.GetItem(filterX + offset, filterY + offset);
                                totalBlue += *(filterItemPtr) * kernel.GetItem(filterX + offset, filterY + offset);
                            }
                        }

                        lock (padlock) {
                            if (bitmap.PixelFormat == PixelFormat.Format32bppArgb)
                                resultRow[i + 3] = *(row + sizeof(byte) * (i + 3));
                            resultRow[i + 2] = (byte)(totalRed / totalWeight);
                            resultRow[i + 1] = (byte)(totalGreen / totalWeight);
                            resultRow[i] = (byte)(totalBlue / totalWeight);
                        }
                    });

                });
            }
            result.Close();
            return result;
        }

        private void CalculateWeightedKernel()
        {
            double factor = Math.Round(1 / GetGaussianKernelValue(-offset, -offset, _sigma));

            for (int x = -offset; x <= offset; x++)
            {
                for (int y = -offset; y <= offset; y++)
                {
                    double kernelValue = GetGaussianKernelValue(x, y, _sigma);
                    kernel[x + offset, y + offset] = (int)Math.Round(kernelValue * factor);
                    totalWeight += kernel[x + offset, y + offset];
                }
            }
        }

        private double GetGaussianKernelValue(int x, int y, double sigma, int digit = 6)
        {
            /*
               (1 / (2*PI*sigma^2)) * e^(-(x*x + y*y) / (2*sigma*sigma))
            */
            double pi = Math.PI;
            double e = Math.E;
            double result = (1.0 / (2*pi*sigma*sigma)) * Math.Pow(e, -((x * x + y * y) / (double)(2 * sigma * sigma)));            
            return Utils.Round(result,digit);
        }



    }
}
