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
using System.Diagnostics;

namespace BSWindows
{
    public class Browser
    {
        public string Name { get; set; }
        public string Processname { get; set; }

        public Browser(string name, string processname)
        {
            this.Name = name;
            this.Processname = processname;
        }

        public void OpenUrl(string url)
        {
            Process.Start(this.Processname, url);
        }

        public override string ToString()
        {
            return Name;
        }
    }
    public enum NavigationState
    {
        SERIES,
        SEASON,
        EPISODE
    }

    public partial class MainWindow : Window
    {
        private NavigationState nagivationState = NavigationState.SERIES;

        private List<SeriesInformation> lSeries = new List<SeriesInformation>();
        private List<Browser> lBrowser = new List<Browser>();
        private string url = "";
        private Season s = null;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            InitBrowsers();
        }

        private void InitBrowsers()
        {
            this.lBrowser.Add(new Browser("Chrome", "chrome.exe"));
            this.lBrowser.Add(new Browser("Firefox", "firefox.exe"));
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private void Init()
        {
            lSeries = Api.GetSeries();
            this.InitStackPanel(lSeries);
        }

        private Button CreateButton(object content, RoutedEventHandler clickEvent)
        {
            Button b = new Button();
            b.Content = content;
            b.HorizontalContentAlignment = HorizontalAlignment.Left;
            b.Click += clickEvent;

            return b;
        }

        private void InitStackPanel(IEnumerable<SeriesInformation> lSeriesInformation)
        {
            this.spSeries.Children.Clear();
            foreach (var series in lSeriesInformation)
            {

                this.spSeries.Children.Add(
                    this.CreateButton(series, SeriesClick));
            }
        }

        private void InitStackPanel(SeriesInformation seriesInformation)
        {
            this.spSeries.Children.Clear();
            foreach (var season in Api.GetSeasons(seriesInformation))
            {
                this.spSeries.Children.Add(
                    this.CreateButton(season, SeasonClick));
            }
        }

        private void InitStackPanel(Season season)
        {
            this.s = season;
            this.spSeries.Children.Clear();
            foreach (var episode in season.epi)
            {
                this.spSeries.Children.Add(
                    this.CreateButton(episode, EpisodeClick));
            }
        }

        private void InitStackPanelBrowser(Episode episode)
        {
            
            this.spSeries.Children.Clear();
            foreach (var browser in this.lBrowser)
            {
                this.spSeries.Children.Add(
                    this.CreateButton(browser, BrowserClick));
            }
        }

        private void BrowserClick(object sender, RoutedEventArgs e)
        {
            ((sender as Button).Content as Browser).OpenUrl(url);
        }

        private void SeriesClick(object sender, RoutedEventArgs e)
        {
            this.nagivationState = NavigationState.SEASON;
            this.InitStackPanel((sender as Button).Content as SeriesInformation);
        }

        private void SeasonClick(object sender, RoutedEventArgs e)
        {
            this.nagivationState = NavigationState.EPISODE;
            this.InitStackPanel((sender as Button).Content as Season);
        }

        private void EpisodeClick(object sender, RoutedEventArgs e)
        {
            Episode episode = ((sender as Button).Content as Episode);
            EpisodeInformation eInformation = Api.GetEpisode(this.s, Convert.ToInt32(episode.epi));

            LinkInformation linkInfo = eInformation.links.Find(item => item.hoster == "Streamcloud");
           Link l  = Api.GetLink(linkInfo);
            this.url = l.fullurl;

            this.InitStackPanelBrowser((sender as Button).Content as Episode);
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return && !String.IsNullOrEmpty(this.txtSearch.Text) && this.nagivationState == NavigationState.SERIES)
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
            if(!String.IsNullOrEmpty(this.txtSearch.Text) && this.nagivationState == NavigationState.SERIES)
                this.UpdateStackPanel();
        }
    }
}
