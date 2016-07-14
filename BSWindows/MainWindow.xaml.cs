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
using BSApi;
using BSApi.Data;

namespace BSWindows
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<SeriesInformation> lSeries = new List<SeriesInformation>();

        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            lSeries = Api.GetSeries();
            this.InitStackPanel(lSeries);
        }

        private void InitStackPanel(IEnumerable<SeriesInformation> lSeriesInformation)
        {
            this.spSeries.Children.Clear();
            foreach (var series in lSeriesInformation)
            {
                Button b = new Button();
                b.Content = series.series;
                b.Click += SeriesClick;

                this.spSeries.Children.Add(b);
            }
        }

        private void SeriesClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((sender as Button).Content.ToString());
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return && !String.IsNullOrEmpty(this.txtSearch.Text))
            {
                this.UpdateStackPanel();
            }
        }

        private void UpdateStackPanel()
        {
            this.InitStackPanel(
                this.lSeries.Where(item => item.series.ToLower().Contains(this.txtSearch.Text.ToLower())));
        }

        private void imgOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(!String.IsNullOrEmpty(this.txtSearch.Text))
                this.UpdateStackPanel();
        }
    }
}
