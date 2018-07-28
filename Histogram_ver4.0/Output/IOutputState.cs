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
    interface IOutputState
    {
        IColor GenColor(IColor color);
        IColor FirstColor(IColor color);
        IColor SecondColor(IColor color);
        IColor ThirdColor(IColor color);
        LinearGradientBrush FirstHist(Histogram_Part clust = null);
        LinearGradientBrush SecondHist(Histogram_Part clust = null);
        LinearGradientBrush ThirdHist(Histogram_Part clust = null);
    }
}
