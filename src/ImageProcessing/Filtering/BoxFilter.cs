using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Filtering
{
    public class BoxFilter : BaseBlurFilter
    {

        public BoxFilter(Size size):base(size)
        {
            if (size.Width != size.Height) return;
            kernel.Fill(1);
        }

        public BoxFilter(SquareMatrix<int> kernel) : base(kernel){}

        public override Image Apply(Image image)
        {
            if (image == null || image.Source == null || kernel == null || kernel.Width != kernel.Height) return null;

            Bitmap bitmap = new Bitmap(image.Width, image.Height, image.PixelFormat);
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
                                byte* filterItemPtr = offsetItemPtr + sizeof(byte) * ((image.RowSize * filterY) + (filterX * depth));

                                totalRed += *(filterItemPtr + sizeof(byte) * 2) * kernel.GetItem(filterX + offset, filterY + offset);
                                totalGreen += *(filterItemPtr + sizeof(byte) * 1) * kernel.GetItem(filterX + offset, filterY + offset);
                                totalBlue += *(filterItemPtr) * kernel.GetItem(filterX + offset, filterY + offset);
                            }
                        }

                        lock (padlock) { 
                            if (bitmap.PixelFormat == PixelFormat.Format32bppArgb)
                                resultRow[i + 3] = *(offsetItemPtr + sizeof(byte) * 3);

                            resultRow[i + 2] = (byte)(totalRed / kernel.Length);
                            resultRow[i + 1] = (byte)(totalGreen / kernel.Length);
                            resultRow[i] = (byte)(totalBlue / kernel.Length);
                        }
                    });
                });
            }
            result.Close();
            return result;
        }
    }
}
