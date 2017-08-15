using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageProcessing.Filtering
{
    public abstract class BaseBlurFilter : IFilter
    {
        protected SquareMatrix<int> kernel;
        protected int offset = 1;

        protected BaseBlurFilter(){}

        public BaseBlurFilter(Size size):this(new SquareMatrix<int>(size.Width)){}

        public BaseBlurFilter(SquareMatrix<int> kernel)
        {
            if (kernel.Width != kernel.Height) return;

            this.kernel = kernel;
            offset = CalculateOffset(this.kernel.Width);
        }

        public abstract Image Apply(Image image);

        public void SetSize(int kernelSize)
        {
            SquareMatrix<int> newKernel = new SquareMatrix<int>(kernelSize);
            this.kernel = newKernel;
            offset = CalculateOffset(this.kernel.Width);
        }

        // 2*offset+1=matrixDegree
        // matrixDegree = size
        protected int CalculateOffset(int size)
        {
            return (size - 1) / 2;
        }
    }
}
