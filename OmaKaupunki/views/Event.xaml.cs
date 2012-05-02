using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using OmaKaupunki.model;
using OmaKaupunki.controller;
using Newtonsoft.Json;
using Microsoft.Phone.Controls.Maps;
using System.Device.Location;
using Microsoft.Phone.Tasks;

namespace OmaKaupunki
{
    public partial class Event : PhoneApplicationPage
    {
        int id;
        string uri = "";
        GeoCoordinateWatcher watcher;
        bool first = true;
        Pushpin pushpin;

        public Event()
        {
            InitializeComponent();
            watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
            watcher.MovementThreshold = 20;
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(onPositionChanged);
            watcher.Start();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string tmp;

            if (NavigationContext.QueryString.TryGetValue("id", out tmp) && int.TryParse(tmp, out id))
                download();
        }

        private void download()
        {
            try
            {
                Dataprovider dataprovider = new Dataprovider();
                WebClient webClient = new WebClient();
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(downloadCompleted);
                webClient.DownloadStringAsync(new Uri(dataprovider.APIURL + id + "?api_key=" + dataprovider.APIKEY));
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                err.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void downloadCompleted(object sender, DownloadStringCompletedEventArgs ex)
        {
            try
            {
                model.Events data = JsonConvert.DeserializeObject<model.Events>(ex.Result);
                model.Event e = data.data.First();
                if (e == null)
                    err.Visibility = System.Windows.Visibility.Visible;
                else
                {
                    title.Text = e.title;
                    body.Text = e.body;
                    if (e.url != null && e.url != "")
                    {
                        url.Visibility = System.Windows.Visibility.Visible;
                        uri = e.url; ;
                    }

                    DateTime dateTime = new DateTime(1970,1,1,0,0,0,0);
                    double timestamp = 0;
                    string tmp = e.start_time.ToString();
                    if (double.TryParse(e.start_time.ToString(), out timestamp))
                        startTime.Text = dateTime.AddSeconds(timestamp).ToString("HH:mm d.M.yyyy");

                    GeoCoordinate geoCoordinate = e.toGeoCoordinate();

                    smallMap.Center = geoCoordinate;
                    smallMap.Children.Add(e.toPushpin());

                    map.Children.Add(e.toPushpin());
                }
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.ToString());
                err.Visibility = System.Windows.Visibility.Visible;
            }
            finally
            {
                performanceProgressBar.IsIndeterminate = false;
            }
        }

        private void url_Click(object sender, RoutedEventArgs e)
        {
            if (uri != "")
            {
                WebBrowserTask webBrowserTask = new WebBrowserTask();
                webBrowserTask.Uri = new Uri(uri, UriKind.Absolute);
                webBrowserTask.Show();
            }
        }

        private void onPositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (first)
            {
                pushpin = new Pushpin();
                pushpin.Content = "Minä";
                map.Children.Add(pushpin);
                first = false;
            }
            map.Center = e.Position.Location;
            pushpin.Location = e.Position.Location;
        }
    }
}