using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Filtering
{
    public class MedianFilter : BaseBlurFilter
    {
        public MedianFilter(Size size) : base(size)
        {
        }

        public override Image Apply(Image image)
        {
            if (image == null || image.Source == null) return null;

            Bitmap bitmap = new Bitmap(image.Width, image.Height, image.PixelFormat);
            Image result = new Image(bitmap);
            result.Open(AccessMode.ReadWrite);
            object padlock = new object();

            int height = image.Height;
            int width = image.Width;
            int depth = image.Depth;
            int rowsize = width * depth;
            bool is32bppFormat = bitmap.PixelFormat == PixelFormat.Format32bppArgb;

            unsafe
            {
                Parallel.For(offset, height - offset, Y =>
                {
                    byte* row = image.GetRowBytes(Y);
                    byte* resultRow = result.GetRowBytes(Y);
                    Parallel.For(depth, rowsize - (depth - 1), i =>
                    {
                        if (i % depth != 0) return;

                        List<int> pixels = new List<int>();

                        byte* offsetItemPtr = row + sizeof(byte) * i;

                        for (int filterY = -offset; filterY <= offset; filterY++)
                        {
                            for (int filterX = -offset; filterX <= offset; filterX++)
                            {
                                byte* filterItemPtr = offsetItemPtr + sizeof(byte) * ((image.RowSize * filterY) + (filterX * image.Depth));

                                byte alpha = is32bppFormat
                                                    ? *(filterItemPtr + sizeof(byte) * 3)
                                                    : (byte)255;
                                byte[] pixel =  new byte[4];

                                pixel[3] = alpha;
                                pixel[2] = *(filterItemPtr + sizeof(byte) * 2);
                                pixel[1] = *(filterItemPtr + sizeof(byte) * 1);
                                pixel[0] = *(filterItemPtr);
                                lock (padlock)
                                {
                                    var p = BitConverter.ToInt32(pixel, 0);
                                    pixels.Add(p);
                                }
                            }
                        }

                        pixels.Sort();
                        lock (padlock)
                        {
                            var medianPixel = BitConverter.GetBytes(pixels[offset]);
                            if (is32bppFormat)
                                resultRow[i + 3] = medianPixel[3];

                            resultRow[i + 2] = medianPixel[2];
                            resultRow[i + 1] = medianPixel[1];
                            resultRow[i] = medianPixel[0];
                        }
                    });
                    
                
                });
            }
            result.Close();
            return result;
        }
    }
}
