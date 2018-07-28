using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Histogram_ver5._0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Acceptence_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Filter(object sender, KeyEventArgs e)
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

        private void Accept_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
