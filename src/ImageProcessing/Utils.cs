using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    class Utils
    {
        public static double Round(double number, int digit)
        {
            double k = Math.Pow(10, digit);
            return Math.Round(number * k) / k;
        }
    }
}
