using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class Pixel
    {
        public byte Alpha { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }

        public Pixel()
        {
            R = G = B = 0;
            RowNumber = ColumnNumber = 0;
        }

        public Pixel(byte R, byte G, byte B)
        {
            this.R = R;
            this.G = G;
            this.B = B;
            RowNumber = ColumnNumber = 0;
        }
    }
}
