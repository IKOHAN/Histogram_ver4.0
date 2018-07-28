using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{
    interface IState
    {
        IColor ColorState
        {
            get;
        }
        List<ICLayerState> GetCLayers
        {
            get;
        }
        IRange GetRange
        {
            get;
        }
        IOutputState GetOutput
        {
            get;
        }
    }
}
