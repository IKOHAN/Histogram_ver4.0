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
    delegate IColor GetOutColor(IColor color);
    class Output : IOutput
    {
        static Random rnd = new Random();
        static IOutputState state;
        public BitmapSource GetOutputImage(Image img)
        {
            var wbmp = new WriteableBitmap(img.Width, img.Height, 96, 96, PixelFormats.Bgr24, null);
            wbmp.Lock();
            try
            {
                unsafe
                {
                    int bytesPerPixel = 24 / 8;
                    int heightInPixels = img.Height;
                    int widthInBytes = img.Width * bytesPerPixel;
                    byte* ptrFirstPixel = (byte*) wbmp.BackBuffer;

                    for (int y = 0; y < img.Height; y++)
                    {
                        byte* currentLine = ptrFirstPixel + ( y * wbmp.BackBufferStride );
                        for (int x = 0, x1 = 0; x < widthInBytes; x = x + bytesPerPixel, x1++)
                        {
                            if (!img.GetMap[y, x1]|| img[y, x1] == null)
                            {
                                    currentLine[x] = 146;
                                    currentLine[x + 1] = 146;
                                    currentLine[x + 2] = 146;
                            }
                            else
                            {
                                IColor tmp = OutputColor(img[y, x1].GetColor);
                                currentLine[x] = (byte) tmp.GetVal3;
                                currentLine[x + 1] = (byte) tmp.GetVal2;
                                currentLine[x + 2] = (byte) tmp.GetVal1;
                            }
                        }
                    }
                }
                wbmp.AddDirtyRect(new Int32Rect(0, 0, img.Width, img.Height));
                return wbmp;
            }
            finally { wbmp.Unlock(); }
        }
        public BitmapSource GetOutputClusters(Image img, List<IStats> stats)
        {
            var colors = new List<IColor>();
            for (int i = 0; i < stats.Count; i++)
            {
                colors.Add(new RGB().Create(rnd.Next(256), rnd.Next(256), rnd.Next(256)));
            }
            var wbmp = new WriteableBitmap(img.Width, img.Height, 96, 96, PixelFormats.Bgr24, null);
            wbmp.Lock();
            try
            {
                unsafe
                {
                    int bytesPerPixel = 24 / 8;
                    int heightInPixels = img.Height;
                    int widthInBytes = img.Width * bytesPerPixel;
                    byte* ptrFirstPixel = (byte*) wbmp.BackBuffer;

                    for (int y = 0; y < img.Height; y++)
                    {
                        byte* currentLine = ptrFirstPixel + ( y * wbmp.BackBufferStride );
                        for (int x = 0, x1 = 0; x < widthInBytes; x = x + bytesPerPixel, x1++)
                        {
                            for (int n = 0; n < stats.Count; n++)
                            {
                                if (stats[n].GetMap[y, x1])
                                {
                                    currentLine[x] = (byte) colors[n].GetVal3;
                                    currentLine[x + 1] = (byte) colors[n].GetVal2;
                                    currentLine[x + 2] = (byte) colors[n].GetVal1;
                                }
                            }
                        }
                    }
                }
                wbmp.AddDirtyRect(new Int32Rect(0, 0, img.Width, img.Height));
                return wbmp;
            }
            finally { wbmp.Unlock(); }
        }
        public BitmapSource GetOutputPart(Image_Project img)
        {
            int startx = img.GetImage.GetMap.GetWBound.X;
            int starty = img.GetImage.GetMap.GetHBound.X;
            int height = img.GetImage.GetMap.GetHBound.Y - img.GetImage.GetMap.GetHBound.X;
            int width = img.GetImage.GetMap.GetWBound.Y - img.GetImage.GetMap.GetWBound.X;
            var wbmp = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgr24, null);
            wbmp.Lock();
            try
            {
                unsafe
                {
                    int bytesPerPixel = 24 / 8;
                    int heightInPixels = height;
                    int widthInBytes = width * bytesPerPixel;
                    byte* ptrFirstPixel = (byte*) wbmp.BackBuffer;

                    for (int y = 0; y < height; y++)
                    {
                        byte* currentLine = ptrFirstPixel + ( y * wbmp.BackBufferStride );
                        for (int x = 0, x1 = 0; x < widthInBytes; x = x + bytesPerPixel, x1++)
                        {
                            if (!img.GetImage.GetMap[y+starty, x1+startx] || img.GetImage[y+starty, x1+startx] == null)
                            {
                                currentLine[x] = 146;
                                currentLine[x + 1] = 146;
                                currentLine[x + 2] = 146;
                            }
                            else
                            {
                                IColor tmp = OutputColor(img.GetImage[y+starty, x1+startx].GetColor);
                                currentLine[x] = (byte) tmp.GetVal3;
                                currentLine[x + 1] = (byte) tmp.GetVal2;
                                currentLine[x + 2] = (byte) tmp.GetVal1;
                            }
                        }
                    }
                }
                wbmp.AddDirtyRect(new Int32Rect(0, 0, width, height));
                return wbmp;
            }
            finally { wbmp.Unlock(); }
        }

        public SeriesCollection GetFirstHistogram(Image_Project project, int indx = -1)
        {
            double[] bmp = project.GetFirstHistogram.Scale();
            var cv = new ChartValues<double>();
            cv.AddRange(bmp);
            int tmp = project.GetFirstLayer.GetClust.Count;
            var mv = new ChartValues<ObservablePoint>();
            for (int i = 0; i < tmp; i++)
            {
                int pos = project.GetFirstLayer.GetClust[i].GetPos;
                mv.Add(new ObservablePoint(pos, bmp[pos]));
            }
            Histogram_Part tmp_clust = null;
            if (indx != -1)
                tmp_clust = project.GetFirstLayer.GetClust[indx];
            return GetHistogram(cv, state.FirstHist(tmp_clust), mv);
        }

        public SeriesCollection GetSecondHistogram(Image_Project project, int indx = -1)
        {
            double[] bmp = project.GetSecondHistogram.Scale();
            var cv = new ChartValues<double>();
            cv.AddRange(bmp);
            int tmp = project.GetSecondLayer.GetClust.Count;
            var mv = new ChartValues<ObservablePoint>();
            for (int i = 0; i < tmp; i++)
            {
                int pos = project.GetSecondLayer.GetClust[i].GetPos;
                mv.Add(new ObservablePoint(pos, bmp[pos]));
            }
            Histogram_Part tmp_clust = null;
            if (indx != -1)
                tmp_clust = project.GetSecondLayer.GetClust[indx];
            return GetHistogram(cv, state.SecondHist(tmp_clust), mv);
        }

        public SeriesCollection GetThirdHistogram(Image_Project project, int indx = -1)
        {
            double[] bmp = project.GetThirdHistogram.Scale();
            var cv = new ChartValues<double>();
            cv.AddRange(bmp);
            int tmp = project.GetThirdLayer.GetClust.Count;
            var mv = new ChartValues<ObservablePoint>();
            for (int i = 0; i < tmp; i++)
            {
                int pos = project.GetThirdLayer.GetClust[i].GetPos;
                mv.Add(new ObservablePoint(pos, bmp[pos]));
            }
            Histogram_Part tmp_clust = null;
            if (indx != -1)
                tmp_clust = project.GetThirdLayer.GetClust[indx];
            return GetHistogram(cv, state.ThirdHist(tmp_clust), mv);
        }
        SeriesCollection GetHistogram(ChartValues<double> cv, LinearGradientBrush fill, ChartValues<ObservablePoint> mv = null)
        {
            var res = new SeriesCollection() {
                new LineSeries {
                    Fill = fill,
                    StrokeThickness=1,
                    PointGeometrySize = 0,
                    Values = cv,
                    PointGeometry = null,
                    IsEnabled = true
                }
        };
            if (mv != null)
                res.Add(new ScatterSeries() {
                    Fill = Brushes.White,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                    Values = mv
                });
            return res;
        }
        public BitmapSource GetFirstChannel(Image_Project img, int indx = -1)
        {
            if (indx == -1)
                return GetChannel(img.GetImage, img.GetImage.GetMap, state.FirstColor);
            else
                return GetChannel(img.GetImage, img.GetFirstLayer.GetClust[indx].GetMap, state.FirstColor);
        }

        public BitmapSource GetSecondChannel(Image_Project img, int indx = -1)
        {
            if (indx == -1)
                return GetChannel(img.GetImage, img.GetImage.GetMap, state.SecondColor);
            else
                return GetChannel(img.GetImage,img.GetSecondLayer.GetClust[indx].GetMap, state.SecondColor);
        }

        public BitmapSource GetThirdChannel(Image_Project img, int indx = -1)
        {
            if (indx == -1)
                return GetChannel(img.GetImage, img.GetImage.GetMap, state.ThirdColor);
            else
                return GetChannel(img.GetImage,img.GetThirdLayer.GetClust[indx].GetMap, state.ThirdColor);
        }
        BitmapSource GetChannel(Image img,Mask mask, GetOutColor outColor)
        {
            var wbmp = new WriteableBitmap(img.Width, img.Height, 96, 96, PixelFormats.Bgr24, null);
            wbmp.Lock();
            try
            {
                unsafe
                {
                    int bytesPerPixel = 24 / 8;
                    int heightInPixels = img.Height;
                    int widthInBytes = img.Width * bytesPerPixel;
                    byte* ptrFirstPixel = (byte*) wbmp.BackBuffer;

                    for (int y = 0; y < img.Height; y++)
                    {
                        byte* currentLine = ptrFirstPixel + ( y * wbmp.BackBufferStride );
                        for (int x = 0, x1 = 0; x < widthInBytes; x = x + bytesPerPixel, x1++)
                        {
                            if (mask[y,x1])
                            {
                                IColor tmp = outColor(img[y, x1].GetColor);
                                currentLine[x] = (byte) tmp.GetVal3;
                                currentLine[x + 1] = (byte) tmp.GetVal2;
                                currentLine[x + 2] = (byte) tmp.GetVal1;
                            }
                            else
                            {
                                currentLine[x] = 146;
                                currentLine[x + 1] = 146;
                                currentLine[x + 2] = 146;
                            }
                        }
                    }
                }
                wbmp.AddDirtyRect(new Int32Rect(0, 0, img.Width, img.Height));
                return wbmp;
            }
            finally { wbmp.Unlock(); }
        }

        public void Update(IState state)
        {
            Output.state = state.GetOutput;
        }

        public IColor OutputColor(IColor color)
        {
            if (color is RGB)
                return color;
            else
            if (color is HSV)
                return ColorConvert.HSVtoRGB(color);
            else
                throw new IncorrectSchemeInput();

        }

        public IOutputState GetState
        {
            get => state;
        }

        public BitmapSource GetOutputImage(Image img, Mask map)
        {
            return GetChannel(img,map, state.GenColor);
        }

        public SeriesCollection GetFirstHistogram(Histogram project)
        {
            double[] bmp = project.Scale();
            var cv = new ChartValues<double>();
            cv.AddRange(bmp);
            return GetHistogram(cv, state.FirstHist());
        }

        public SeriesCollection GetSecondHistogram(Histogram project)
        {
            double[] bmp = project.Scale();
            var cv = new ChartValues<double>();
            cv.AddRange(bmp);
            return GetHistogram(cv, state.SecondHist());
        }

        public SeriesCollection GetThirdHistogram(Histogram project)
        {
            double[] bmp = project.Scale();
            var cv = new ChartValues<double>();
            cv.AddRange(bmp);
            return GetHistogram(cv, state.ThirdHist());
        }
    }
}
