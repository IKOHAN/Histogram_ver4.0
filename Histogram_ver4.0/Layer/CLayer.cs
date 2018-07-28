using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{
    class CLayer
    {
        ICLayerState state;
        List<Histogram_Part> clusters;
        GetComponent getCComponent;
        public CLayer()
        {

        }
        public CLayer(Image img, Histogram histogram, GetComponent colorComponent, ICLayerState state)
        {
            List<int[]> clustBase = state.ClusterBorders(histogram.GetHistogram);
            clusters = new List<Histogram_Part>();
            for (int i = 0; i < clustBase.Count; i++)
            {
                clusters.Add(new Histogram_Part(clustBase[i],img.Width,img.Height));
            }
            getCComponent = colorComponent;
            this.state = state;
            state.ClusterSearch(img, colorComponent, clusters);
        }
        //public GetComponent GetOut
        //{
        //    get => getCComponent;
        //}
        public List<Histogram_Part> GetClust
        {
            get => clusters;
            set => clusters = value;
        }
        //public ICLayerState GetState
        //{
        //    get => state;
        //    set => state = value;
        //}
        //public Histogram_Part this[int x]
        //{
        //    get => clusters[x];
        //}
    }
}
