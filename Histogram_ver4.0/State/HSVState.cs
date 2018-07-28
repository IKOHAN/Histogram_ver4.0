using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{
    class HSVState : IState
    {
        public IColor ColorState
        {
            get => new HSV();
        }
        public List<ICLayerState> GetCLayers
        {
            get => new List<ICLayerState>() { new LapCLayer().Create(3.6), new FlatCLayer().Create(1), new FlatCLayer().Create(1) };
        }
        public IRange GetRange
        {
            get => new HSVRange();
        }
        public IOutputState GetOutput
        {
            get => new HSVOutput();
        }
    }
}
