using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Histogram_ver4._0
{
    class Image_Project
    {
        IState Genstate;
        List<ICLayerState> layState;
        IRange histRange;
        Image image;
        Histogram firstHistogram, secondHistogram, thirdHistogram;
        CLayer firstChannel, secondChannel, thirdChannel;

        public Image_Project()
        {
        }
        public Image_Project(BitmapSource bmps, IState state)
        {
            Update(state);
            image = new Image(bmps, state.ColorState);
            Analize_image();
        }
        public Image_Project(Image img, IState state)
        {
            Update(state);
            image = img;
            Analize_image();
        }
        void Analize_image()
        {
            firstHistogram = new Histogram(image, CTake.GetFirst, histRange.GetFirst);
            secondHistogram = new Histogram(image, CTake.GetSecond, histRange.GetSecond);
            thirdHistogram = new Histogram(image, CTake.GetThird, histRange.GetThird);
            firstChannel = new CLayer(image, firstHistogram, CTake.GetFirst, layState[0]);
            secondChannel = new CLayer(image, secondHistogram, CTake.GetSecond, layState[1]);
            thirdChannel = new CLayer(image, thirdHistogram, CTake.GetThird, layState[2]);
        }
        public Image GetImage
        {
            get => image;
            set => image = value;
        }
        public Histogram GetFirstHistogram
        {
            get => firstHistogram;
        }
        public Histogram GetSecondHistogram
        {
            get => secondHistogram;
        }
        public Histogram GetThirdHistogram
        {
            get => thirdHistogram;
        }
        public CLayer GetFirstLayer
        {
            get => firstChannel;
        }
        public CLayer GetSecondLayer
        {
            get => secondChannel;
        }
        public CLayer GetThirdLayer
        {
            get => thirdChannel;
        }
        void Update(IState state)
        {
            Genstate = state;
            layState = state.GetCLayers;
            histRange = state.GetRange;
        }

        public IState GetState
        {
            get => Genstate;
            set => Genstate = value;
        }
    }
}
