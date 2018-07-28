using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{
    class RGBState : IState
    {
        public IColor ColorState
        {
            get => new RGB();
        }
        public List<ICLayerState> GetCLayers
        {
            get => new List<ICLayerState>() { new FlatCLayer().Create(2.55), new FlatCLayer().Create(2.55), new FlatCLayer().Create(2.55) };
        }
        public IRange GetRange
        {
            get => new RGBRange();
        }
        public IOutputState GetOutput
        {
            get => new RGBOutput();
        }
    }
}
