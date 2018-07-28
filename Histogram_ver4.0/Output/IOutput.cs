using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Windows;

namespace Histogram_ver4._0
{
    interface IOutput : IObserver
    {
        BitmapSource GetOutputImage(Image img);
        BitmapSource GetOutputClusters(Image img, List<IStats> stats);
        BitmapSource GetOutputPart(Image_Project img);
        BitmapSource GetOutputImage(Image img, Mask map);
        SeriesCollection GetFirstHistogram(Image_Project project, int indx = -1);
        SeriesCollection GetSecondHistogram(Image_Project project, int indx = -1);
        SeriesCollection GetThirdHistogram(Image_Project project, int indx = -1);

        SeriesCollection GetFirstHistogram(Histogram project);
        SeriesCollection GetSecondHistogram(Histogram project);
        SeriesCollection GetThirdHistogram(Histogram project);
        BitmapSource GetFirstChannel(Image_Project img, int indx = -1);
        BitmapSource GetSecondChannel(Image_Project img, int indx = -1);
        BitmapSource GetThirdChannel(Image_Project img, int indx = -1);
        IColor OutputColor(IColor color);
        IOutputState GetState
        {
            get;
        }
    }
}
