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
using System.Windows.Media.Animation;

namespace Histogram_ver5._0
{
    public enum Norma
    {
        Default=0,
        Brightness,
        Evklid,
        Manhatten,
        Max
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
                InitializeComponent();
        }
        Norma GetNorma=Norma.Default;
        Window PartSelect;
        Window Image_Show;
        private void Filter(object sender, KeyEventArgs e)
        {

        }

        private void UncheckedFilters(object sender, RoutedEventArgs e)
        {
            bool? Neighbors = Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_Neighbors_CB.IsChecked;
            bool? MoveWin = Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_MoveWindow_CB.IsChecked;
            bool? Tang = Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_TanSign_CB.IsChecked;
            bool? Triang = Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_Triangle_CB.IsChecked;
            bool? Relatives = Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_RelativeHeight_CB.IsChecked;
            bool? CrossLvl = Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_CrossLevelLine_CB.IsChecked;
            if (!Neighbors.Value && 
                !MoveWin.Value && 
                !Tang.Value && 
                !Triang.Value && 
                !Relatives.Value && 
                !CrossLvl.Value)
                Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Original_RB.IsChecked = true;
        }
        private void FiltersUnchecked(object sender, RoutedEventArgs e)
        {
            if (!Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB.IsChecked.Value)
            {
                Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_Neighbors_CB.IsChecked = false;
                Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_MoveWindow_CB.IsChecked = false;
                Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_TanSign_CB.IsChecked = false;
                Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_Triangle_CB.IsChecked = false;
                Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_RelativeHeight_CB.IsChecked = false;
                Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_CrossLevelLine_CB.IsChecked = false;
            }           
        }

        private void Loading_SP_Load_BTN_Click(object sender, RoutedEventArgs e)
        {
            Image_Show = new ImageAndHistogramWindow();
            Image_Show.Owner = this;
            Image_Show.Show();
        }

        private void Loading_SP_Restore_BTN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Loading_SP_Params_EXPNDR_Peaks_Search_GB_AllPossible_RB_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Loading_SP_Params_EXPNDR_Peaks_Search_GB_HillClimbing_RB_Checked(object sender, RoutedEventArgs e)
        {
            Column1Show(Loading_SP_Params_EXPNDR_Peaks_Search_GB_PeakSearch_SP, Loading_SP_Params_EXPNDR_Peaks_Search_GB_HillClimb_SP);
        }

        private void Loading_SP_Params_EXPNDR_Peaks_Search_GB_HillClimb_SP_Accept_BTN_Click(object sender, RoutedEventArgs e)
        {
            Column0Show(Loading_SP_Params_EXPNDR_Peaks_Search_GB_PeakSearch_SP, Loading_SP_Params_EXPNDR_Peaks_Search_GB_HillClimb_SP);
        }

        private void Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Original_RB_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_Neighbors_CB_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_MoveWindow_CB_Checked(object sender, RoutedEventArgs e)
        {

            Column1Show(Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP, Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_WindSize_SP);
        }

        private void Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_WindSize_SP_Accept_BTN_Click(object sender, RoutedEventArgs e)
        {

            Column0Show(Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP, Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_WindSize_SP);
        }

        private void Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_TanSign_CB_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_Triangle_CB_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_RelativeHeight_CB_Checked(object sender, RoutedEventArgs e)
        {
            Column1Show(Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP, Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_RelativeLvl_SP);
        }

        private void Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_RelativeLvl_SP_Accept_BTN_Click(object sender, RoutedEventArgs e)
        {
            Column0Show(Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP, Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_RelativeLvl_SP);
        }

        private void Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_CrossLevelLine_CB_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Scanning_SP_Analize_BTN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Scanning_SP_Image_RB_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Scanning_SP_Parts_RB_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Scanning_SP_Parameters_EXPNDR_Dist_GB_Dist_SP__NormDifference_RB_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Scanning_SP_Parameters_EXPNDR_Dist_GB_Dist_SP_DifferenceNorm_RB_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Scanning_SP_Parameters_EXPNDR_Norma_GB_FilterMenu_SP_Brightness_RB_Checked(object sender, RoutedEventArgs e)
        {
            GetNorma = Norma.Brightness;
            //ShowThreshold(sender, e);
            Column1Show(Scanning_SP_Parameters_EXPNDR_Norma_GB_FilterMenu_SP, Scanning_SP_Parameters_EXPNDR_Norma_GB_Threshold_SP);
        }

        private void Scanning_SP_Parameters_EXPNDR_Norma_GB_FilterMenu_SP_Evklid_RB_Checked(object sender, RoutedEventArgs e)
        {
            GetNorma = Norma.Evklid;
            //ShowThreshold(sender, e);
            Column1Show(Scanning_SP_Parameters_EXPNDR_Norma_GB_FilterMenu_SP, Scanning_SP_Parameters_EXPNDR_Norma_GB_Threshold_SP);
        }

        private void Scanning_SP_Parameters_EXPNDR_Norma_GB_FilterMenu_SP_Manhatten_RB_Checked(object sender, RoutedEventArgs e)
        {
            GetNorma = Norma.Manhatten;
            //ShowThreshold(sender, e);
            Column1Show(Scanning_SP_Parameters_EXPNDR_Norma_GB_FilterMenu_SP, Scanning_SP_Parameters_EXPNDR_Norma_GB_Threshold_SP);
        }

        private void Scanning_SP_Parameters_EXPNDR_Norma_GB_FilterMenu_SP_Max_RB_Checked(object sender, RoutedEventArgs e)
        {
            GetNorma = Norma.Max;
            //ShowThreshold(sender, e);
            Column1Show(Scanning_SP_Parameters_EXPNDR_Norma_GB_FilterMenu_SP, Scanning_SP_Parameters_EXPNDR_Norma_GB_Threshold_SP);
        }

        private void Displaying_SP_Channel_CBOX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Displaying_SP_Segment_CBOX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Mode_SP_RGB_RB_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Mode_SP_HSV_RB_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Scanning_SP_Parameters_EXPNDR_Norma_GB_Threshold_SP_Accept_BTN_Click(object sender, RoutedEventArgs e)
        {
            Column0Show(Scanning_SP_Parameters_EXPNDR_Norma_GB_FilterMenu_SP, Scanning_SP_Parameters_EXPNDR_Norma_GB_Threshold_SP);
        }

        private void Scanning_SP_Parameters_EXPNDR_Norma_GB_FilterMenu_SP_Default_RB_Checked(object sender, RoutedEventArgs e)
        {
            GetNorma = Norma.Default;
        }

        private void Loading_SP_Params_EXPNDR_Peaks_Search_GB_HillClimb_SP_WindSize_TB_LostFocus(object sender, RoutedEventArgs e)
        {          

        }

        private void Loading_SP_Params_EXPNDR_Peaks_Search_GB_HillClimb_SP_NumbOfStarts_TB_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void Splitting_SP_Rows_TB_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void Splitting_SP_Columns_TB_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void Scanning_SP_Parameters_EXPNDR_Norma_GB_Threshold_SP_FilterValue_TB_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void Modifying_SP_Kernelsize_TB_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_RelativeLvl_SP_RelativeLvl_TB_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_WindSize_SP_WinSize_TB_LostFocus(object sender, RoutedEventArgs e)
        {

        }
        private void ColorCheck(Control sender, bool value)
        {
            ColorAnimation doubleAnimation;
            if (value)
                doubleAnimation = new ColorAnimation(( (SolidColorBrush) sender.Background ).Color, Colors.Green, new Duration(new TimeSpan(0, 0, 0, 1)), FillBehavior.HoldEnd);
            else
                doubleAnimation = new ColorAnimation(( (SolidColorBrush) sender.Background ).Color, Colors.Red, new Duration(new TimeSpan(0, 0, 0, 2)), FillBehavior.HoldEnd);
            sender.Background.BeginAnimation(SolidColorBrush.ColorProperty, doubleAnimation);
        }
        private void Column0Show(Panel control0,Panel control1)
        {
            double target = control1.ActualWidth;
            var da = new DoubleAnimation(0, target, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
            var da_ = new DoubleAnimation(target, 0, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
            control0.BeginAnimation(WidthProperty, da);
            control1.BeginAnimation(WidthProperty, da_);
        }
        private void Column1Show(Panel control0, Panel control1)
        {
            double target = control0.ActualWidth;
            var da = new DoubleAnimation(0, target, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
            var da_ = new DoubleAnimation(target, 0, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
            control1.BeginAnimation(WidthProperty, da);
            control0.BeginAnimation(WidthProperty, da_);
        }

        private void Loading_SP_Params_EXPNDR_Peaks_Search_GB_HillClimb_SP_Deny_BTN_Click(object sender, RoutedEventArgs e)
        {
            Column0Show(Loading_SP_Params_EXPNDR_Peaks_Search_GB_PeakSearch_SP, Loading_SP_Params_EXPNDR_Peaks_Search_GB_HillClimb_SP);
            Loading_SP_Params_EXPNDR_Peaks_Search_GB_AllPossible_RB.IsChecked = true;
        }

        private void Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_RelativeLvl_SP_Deny_BTN_Click(object sender, RoutedEventArgs e)
        {
            Column0Show(Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP, Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_RelativeLvl_SP);
            Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_RelativeHeight_CB.IsChecked = false;
        }

        private void Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_WindSize_SP_Deny_BTN_Click(object sender, RoutedEventArgs e)
        {
            Column0Show(Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP, Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_WindSize_SP);
            Loading_SP_Params_EXPNDR_Peaks_Filter_GB_PeaksFilter_SP_Filter_RB_MoveWindow_CB.IsChecked = false;
        }

        private void Scanning_SP_Parameters_EXPNDR_Norma_GB_Threshold_SP_Deny_BTN_Click(object sender, RoutedEventArgs e)
        {
            Column0Show(Scanning_SP_Parameters_EXPNDR_Norma_GB_FilterMenu_SP, Scanning_SP_Parameters_EXPNDR_Norma_GB_Threshold_SP);
            Scanning_SP_Parameters_EXPNDR_Norma_GB_FilterMenu_SP_Default_RB.IsChecked = true;
        }
    }
}
