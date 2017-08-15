using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class SquareMatrix<T> : Matrix<T>
    {
        public SquareMatrix(int size) : base(size, size) {}

    }
}
