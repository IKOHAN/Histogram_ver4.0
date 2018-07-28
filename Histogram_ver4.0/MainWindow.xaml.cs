using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using LiveCharts;
using LiveCharts.Defaults;

namespace Histogram_ver4._0
{
    delegate double Adder(int sizedemension, int parts);
    delegate Image_Project PartSelect(Button btn, Parts parts);
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        bool PartworkFlag = false;
        IState state;
        OpenFileDialog OFD;
        Image_Project RGBproject;
        Image_Project HSVproject;
        Image_Project current_canvas;
        Parts parts;
        IOutput GetOutput;
        Controller control;
        IPack pack;
        Image_Processing GetProcess;
        Adder add = (int worh, int partt) => {
            int width = worh / partt;
            int rest = worh % width;
            return ( width + rest ) / width;
        };
        PartSelect Showpart = (Button btn, Parts parts) => {
            string name = btn.Name;
            int[] num_btn = new int[2];
            int.TryParse(name.Split('_')[1], out num_btn[0]);
            int.TryParse(name.Split('_')[2], out num_btn[1]);
            return parts[num_btn[0], num_btn[1]];
        };
        /// <summary>
        /// Start program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Observer_load();
            RGBchannelcBox();
        }
        /// <summary>
        /// Loading image from file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Load_btn_Click(object sender, RoutedEventArgs e)
        {
            string path = System.IO.Path.GetFullPath(@"..\..\..\Image");
            OFD = new OpenFileDialog {
                Filter = "Изображения |*.jpg;*.jpeg;*.bmp;*.png ",
                InitialDirectory = path,
                CheckPathExists = true
            };
            bool? res = OFD.ShowDialog();
            if (res == true)
            {
                RGBproject = null;
                HSVproject = null;
                RGBproject = new Image_Project();
                HSVproject = new Image_Project();
                BitmapSource bmpsourse = new BitmapImage(new Uri(OFD.FileName));
                if (RGB_rb.IsChecked == true)
                {
                    RGBchannelcBox();
                    state = new RGBState();
                }
                else
                {
                    if (HSV_rb.IsChecked == true)
                    {
                        HSVchannelcBox();
                        state = new HSVState();
                    }
                    else
                        throw new Exception("Выберите цветовую схему.");
                }
                control.NotifyObservers(state);
                RGBproject = new Image_Project(bmpsourse, new RGBState());
                HSVproject = new Image_Project(bmpsourse, new HSVState());
                GetProcess = new Image_Processing();
                GetOutput = new Output();
                current_canvas = RGBproject;
                OutputImgandHist();
            }
        }
        /// <summary>
        /// Choose red,green,blue scheme.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RGB_rb_Checked(object sender, RoutedEventArgs e)
        {
            state = new RGBState();
            if (control != null)
                control.NotifyObservers(state);
            if (current_canvas != null)
            {
                RGBchannelcBox();
                current_canvas = RGBproject;
                OutputImgandHist();
            }
        }
        /// <summary>
        /// Choose hue,saturation,value scheme.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HSV_rb_Checked(object sender, RoutedEventArgs e)
        {
            state = new HSVState();
            if (control != null)
                control.NotifyObservers(state);
            if (current_canvas != null)
            {
                HSVchannelcBox();
                current_canvas = HSVproject;
                OutputImgandHist();
            }
        }
        /// <summary>
        /// Choose channel for output.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Channel_cbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (current_canvas != null && ( Channel_cbox.IsMouseCaptured || Channel_cbox.IsKeyboardFocused ))
            {
                ClearSegments();
                int channel = ( sender as ComboBox ).SelectedIndex - 1;
                int segments = Segments_Count(channel);
                ChannelOut(channel);
                LoadSegments(segments);
            }
        }
        void ClearSegments()
        {
            Segment_cbox.Items.Clear();
            var CBI = new ComboBoxItem { Content = string.Format("Choose segment") };
            Segment_cbox.Items.Add(CBI);
            Segment_cbox.SelectedItem = Segment_cbox.Items[0];
        }
        void LoadSegments(int num)
        {
            ComboBoxItem CBI;
            for (int i = 0; i < num; i++)
            {
                CBI = new ComboBoxItem {
                    Content = string.Format("Segment {0}", i + 1)
                };
                Segment_cbox.Items.Add(CBI);
            }
        }
        /// <summary>
        /// Choose channel's segment for output.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Segment_cbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (current_canvas != null && ( Segment_cbox.IsMouseCaptured || Segment_cbox.IsKeyboardFocused ))
            {
                if (Channel_cbox.Items.Count != 1)
                {

                    int channel = Channel_cbox.SelectedIndex - 1;
                    int clust = ( sender as ComboBox ).SelectedIndex - 1;
                    int tmp = Segments_Count(channel, clust);
                    ChannelOut(channel, clust);
                }
                else
                {
                    int clust = ( sender as ComboBox ).SelectedIndex - 1;
                    if (clust == -1)
                        OutputImgandHist(current_canvas,pack.GetClusters);
                    else
                    {
                        System.Windows.Controls.Image img = ( PartworkFlag ) ? Part_img : Source_img;
                        img.Source = GetOutput.GetOutputImage(pack[clust].GetImage, pack[clust].GetMap);
                       // OutputImgandHist(pack[clust].GetImage, pack[clust].GetMap);
                        OutputImg(pack[clust].GetImage);
                        Hist_drawer(pack[clust].GetFirstHist, pack[clust].GetSecondHist, pack[clust].GetThirdHist);
                        stats.Text = pack[clust].ToString();
                    }
                }
            }
        }
        /// <summary>
        /// Restore start image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Restore_btn_Click(object sender, RoutedEventArgs e)
        {
            Nulling();
        }

        private void Split_btn_Click(object sender, RoutedEventArgs e)
        {
            int row = int.Parse(Row_tbox.Text);
            int col = int.Parse(Column_tbox.Text);
            parts = new Parts(current_canvas, row, col);
            #region GridPart
            double horisontal = add(current_canvas.GetImage.Width, col);
            double vertical = add(current_canvas.GetImage.Height, row);
            var grd = new Grid {
                Width = Source_img.ActualWidth,
                Height = Source_img.ActualHeight,
                ShowGridLines = true
            };
            for (int i = 0; i < row; i++)
            {
                var r = new RowDefinition();
                if (i == row - 1)
                    r.Height = new GridLength(vertical, GridUnitType.Star);
                grd.RowDefinitions.Add(r);
            }
            for (int i = 0; i < col; i++)
            {
                var c = new ColumnDefinition();
                if (i == col - 1)
                    c.Width = new GridLength(horisontal, GridUnitType.Star);
                grd.ColumnDefinitions.Add(c);
            }
            Part_grd.Children.Add(grd);
            Source_img.Visibility = Visibility.Collapsed;
            Part_grd.Visibility = Visibility.Visible;
            grd.Visibility = Visibility.Visible;
            #endregion
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                {
                    var img = new System.Windows.Controls.Image {
                        Source = GetOutput.GetOutputImage(parts[i, j].GetImage)
                    };
                    var parting = new Button {
                        Style = FindResource("myBtntrue") as Style,
                        Name = string.Format("Btn_{0}_{1}", i, j),

                        HorizontalContentAlignment = HorizontalAlignment.Stretch,
                        VerticalContentAlignment = VerticalAlignment.Stretch,
                        Content = img
                    };
                    parting.Click += Parting_Click;
                    grd.Children.Add(parting);
                    Grid.SetColumn(parting, j);
                    Grid.SetRow(parting, i);
                }
        }
        private void Parting_Click(object sender, RoutedEventArgs e)
        {
            PartworkFlag = true;
            Img_col.Width = new GridLength(0, GridUnitType.Star);
            Part_show_col.Width = new GridLength(1, GridUnitType.Star);
            current_canvas = Showpart(sender as Button, parts);
            OutputImgandHist();
        }
        private void Unite_btn_Click(object sender, RoutedEventArgs e)
        {
            PartworkFlag = false;
            Source_img.Visibility = Visibility.Visible;
            Part_grd.Visibility = Visibility.Collapsed;
            Img_col.Width = new GridLength(1, GridUnitType.Star);
            Part_show_col.Width = new GridLength(0, GridUnitType.Star);
            current_canvas = RGBproject;
            OutputImgandHist();
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            PartworkFlag = false;
            Img_col.Width = new GridLength(1, GridUnitType.Star);
            Part_show_col.Width = new GridLength(0, GridUnitType.Star);
            current_canvas = RGBproject;
            Hist_drawer();
        }

        private void Digit_Filter(object sender, KeyEventArgs e)
        {
            bool flag = ( ( sender as TextBox ).Text.Contains(',') );
            bool shiftflag = false;
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                shiftflag = true;
            }
            if (!( ( e.Key <= Key.D9 && e.Key >= Key.D0 ) || ( e.Key <= Key.NumPad9 && e.Key >= Key.NumPad0 ) ) && ( e.Key != Key.OemComma ) && ( e.Key != Key.Back ) || shiftflag)
            {
                e.Handled = true;
            }
            else
            {
                if (flag && e.Key == Key.OemComma)
                    e.Handled = true;
            }
        }


        private void Negate_Click(object sender, RoutedEventArgs e)
        {
            current_canvas = GetProcess.Negate_Filter(current_canvas);
            OutputImgandHist();
        }

        private void Blur_btn_Click(object sender, RoutedEventArgs e)
        {
            int size = int.Parse(Blur_tbox.Text);
            current_canvas = GetProcess.Median_Filter(current_canvas, size);
            OutputImgandHist();
        }

        private void Cluster_analize_Click(object sender, RoutedEventArgs e)
        {

            pack = new UnitePack().Create(current_canvas);
            Channel_cbox.Items.Clear();
            Channel_cbox.Items.Add(new ComboBoxItem { Content = string.Format("Segments") });
            Channel_cbox.SelectedItem = Channel_cbox.Items[0];
            ClearSegments();
            LoadSegments(pack.GetClusters.Count);

        }

        private void Cluster_parts_analize_Click(object sender, RoutedEventArgs e)
        {
            int row = int.Parse(Row_tbox.Text);
            int col = int.Parse(Column_tbox.Text);
            parts = new Parts(current_canvas, row, col);
            pack = new UnitePack().Create(current_canvas, parts);
            Channel_cbox.Items.Clear();
            Channel_cbox.Items.Add(new ComboBoxItem { Content = string.Format("Segments") });
            Channel_cbox.SelectedItem = Channel_cbox.Items[0];
            ClearSegments();
            LoadSegments(pack.GetClusters.Count);
        }

        #region Additional 
        void Nulling()
        {
            PartworkFlag = false;
            current_canvas = RGBproject;
            RGB_rb.IsChecked = true;
            RGBchannelcBox();
            OutputImgandHist();
            parts = null;
            //pack = null;
        }
        int Segments_Count(int channel, int clust = -1)
        {
            switch (channel)
            {
                case 0:
                    return current_canvas.GetFirstLayer.GetClust.Count;
                case 1:
                    return current_canvas.GetSecondLayer.GetClust.Count;
                case 2:
                    return current_canvas.GetThirdLayer.GetClust.Count;
                default:
                    return 0;
            }
        }
        void ChannelOut(int channel, int clust = -1)
        {
            System.Windows.Controls.Image img = ( PartworkFlag ) ? Part_img : Source_img;
            switch (channel)
            {
                case 0:
                    img.Source = GetOutput.GetFirstChannel(current_canvas, clust);
                    FirstGraph.Series = GetOutput.GetFirstHistogram(current_canvas, clust);
                    break;
                case 1:
                    img.Source = GetOutput.GetSecondChannel(current_canvas, clust);
                    SecondGraph.Series = GetOutput.GetSecondHistogram(current_canvas, clust);
                    break;
                case 2:
                    img.Source = GetOutput.GetThirdChannel(current_canvas, clust);
                    ThirdGraph.Series = GetOutput.GetThirdHistogram(current_canvas, clust);
                    break;
                default:
                    OutputImgandHist();
                    break;

            }
        }
        void HSVchannelcBox()
        {
            Channel_cbox.Items.Clear();
            var CBI = new ComboBoxItem { Content = string.Format("Choose channel") };
            Channel_cbox.Items.Add(CBI);
            CBI = new ComboBoxItem { Content = string.Format("Hue") };
            Channel_cbox.Items.Add(CBI);
            CBI = new ComboBoxItem { Content = string.Format("Saturation") };
            Channel_cbox.Items.Add(CBI);
            CBI = new ComboBoxItem { Content = string.Format("Value") };
            Channel_cbox.Items.Add(CBI);
            Channel_cbox.SelectedItem = Channel_cbox.Items[0];
        }
        void RGBchannelcBox()
        {
            Channel_cbox.Items.Clear();
            var CBI = new ComboBoxItem { Content = string.Format("Choose channel") };
            Channel_cbox.Items.Add(CBI);
            CBI = new ComboBoxItem { Content = string.Format("Red") };
            Channel_cbox.Items.Add(CBI);
            CBI = new ComboBoxItem { Content = string.Format("Green") };
            Channel_cbox.Items.Add(CBI);
            CBI = new ComboBoxItem { Content = string.Format("Blue") };
            Channel_cbox.Items.Add(CBI);
            Channel_cbox.SelectedItem = Channel_cbox.Items[0];
        }
        void OutputImgandHist(Image_Project img, List<IStats> mask)
        {
            System.Windows.Controls.Image img_source = ( PartworkFlag ) ? Part_img : Source_img;
            img_source.Source = GetOutput.GetOutputClusters(img.GetImage , mask);
            Hist_drawer();
        }
        void OutputImgandHist()
        {
            if (PartworkFlag)
            {
                Part_img.Source = GetOutput.GetOutputImage(current_canvas.GetImage);
                Hist_drawer();
            }
            else
            {
                Source_img.Source = GetOutput.GetOutputImage(current_canvas.GetImage);
                Hist_drawer();
            }
        }
        void OutputImg(Image image)
        {
            System.Windows.Controls.Image img = ( PartworkFlag ) ? Part_img : Source_img;
            img.Source = GetOutput.GetOutputImage(image);
        }
        void Hist_drawer(Histogram h1, Histogram h2, Histogram h3)
        {
            FirstGraph.Series = GetOutput.GetFirstHistogram(h1);
            var y = new LiveCharts.Wpf.AxesCollection {
                new LiveCharts.Wpf.Axis() {
                    MinValue =0,
                    MaxValue = h1.GetHistogram.Max()*100
                }
            };
            FirstGraph.AxisY = y;
            SecondGraph.Series = GetOutput.GetSecondHistogram(h2);
            y = new LiveCharts.Wpf.AxesCollection {
                new LiveCharts.Wpf.Axis() {
                    MinValue =0,
                    MaxValue = h2.GetHistogram.Max()*100
                }
            };
            SecondGraph.AxisY = y;
            ThirdGraph.Series = GetOutput.GetThirdHistogram(h3);
            y = new LiveCharts.Wpf.AxesCollection {
                new LiveCharts.Wpf.Axis() {
                    MinValue =0,
                    MaxValue = h3.GetHistogram.Max()*100
                }
            };
            ThirdGraph.AxisY = y;
        }
        void Hist_drawer()
        {
            FirstGraph.Series = GetOutput.GetFirstHistogram(current_canvas);
            var y = new LiveCharts.Wpf.AxesCollection {
                new LiveCharts.Wpf.Axis() {
                    MinValue =0,
                    MaxValue = current_canvas.GetFirstHistogram.GetHistogram.Max()*100
                }
            };
            FirstGraph.AxisY = y;
            SecondGraph.Series = GetOutput.GetSecondHistogram(current_canvas);
            y = new LiveCharts.Wpf.AxesCollection {
                new LiveCharts.Wpf.Axis() {
                    MinValue =0,
                    MaxValue = current_canvas.GetSecondHistogram.GetHistogram.Max()*100
                }
            };
            SecondGraph.AxisY = y;
            ThirdGraph.Series = GetOutput.GetThirdHistogram(current_canvas);
            y = new LiveCharts.Wpf.AxesCollection {
                new LiveCharts.Wpf.Axis() {
                    MinValue =0,
                    MaxValue = current_canvas.GetThirdHistogram.GetHistogram.Max()*100
                }
            };
            ThirdGraph.AxisY = y;
        }
        void Observer_load()
        {
            control = new Controller();
            control.AddObserver(new Output());
            control.AddObserver(new Image_Processing());
            control.AddObserver(new UnitePack());
        }
        #endregion

        private void ChannelandSegm_Click(object sender, RoutedEventArgs e)
        {
            if (HSV_rb.IsChecked == true)
            {
                HSVchannelcBox();
                current_canvas = HSVproject;
            }
            else
            {
                if (RGB_rb.IsChecked == true)
                {
                    RGBchannelcBox();
                    current_canvas = RGBproject;
                }
                else
                    throw new Exception("");
            }

            OutputImgandHist();
        }
    }
}
