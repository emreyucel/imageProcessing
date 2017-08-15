using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Filtering
{
    public interface IFilter
    {
        Image Apply(Image image);
    }
}
