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
using System.Device.Location;
using OmaKaupunki.controller;
using OmaKaupunki.model;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Microsoft.Phone.Controls.Maps;

namespace OmaKaupunki
{
    public partial class MainPage : PhoneApplicationPage
    {
        GeoCoordinateWatcher watcher;
        bool first = true;
        Pushpin pushpin;
        public MainPage()
        {
            InitializeComponent();
            watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
            watcher.MovementThreshold = 20;
            watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(onStatusChanged);
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(onPositionChanged);
            watcher.Start();
            download();
        }

        private void onStatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            /*            switch (e.Status)
                        {
                            case GeoPositionStatus.Disabled:
                            if (watcher.Permission == GeoPositionPermission.Denied)
                            {
                                    // The user has disabled the Location Service on their device.
                                    statusTextBlock.Text = "you have this application access to location.";
                                }
                                else
                                {
                                    statusTextBlock.Text = "location is not functioning on this device";
                                }
                                break;

                            case GeoPositionStatus.Initializing:
                                // The Location Service is initializing.
                                // Disable the Start Location button.
                                startLocationButton.IsEnabled = false;
                                break;

                            case GeoPositionStatus.NoData:
                                // The Location Service is working, but it cannot get location data.
                                // Alert the user and enable the Stop Location button.
                                statusTextBlock.Text = "location data is not available.";
                                stopLocationButton.IsEnabled = true;
                                break;

                            case GeoPositionStatus.Ready:
                                // The Location Service is working and is receiving location data.
                                // Show the current position and enable the Stop Location button.
                                statusTextBlock.Text = "location data is available.";
                                stopLocationButton.IsEnabled = true;
                                break;
                        }*/
        }

        private void onPositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (first)
            {
                pushpin = new Pushpin();
                pushpin.Background = new SolidColorBrush(Colors.Red);
                map.Children.Add(pushpin);
                first = false;
            }
            map.Center = e.Position.Location;
            pushpin.Location = e.Position.Location;
            watcher.Stop();
        }

        private void nearMe(object sender, EventArgs e)
        {
            watcher.Start();
        }

        private void browse(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/views/Categories.xaml", UriKind.Relative));
        }

        private void search(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/views/Search.xaml", UriKind.Relative));
        }

        private void download()
        {
            try
            {
                Dataprovider dataprovider = new Dataprovider();
                foreach(Menu menu in App.menu){
                    WebClient webClient = new WebClient();
                    webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(downloadCompleted);
                    webClient.DownloadStringAsync(new Uri(dataprovider.APIURL + "search?api_key=" + dataprovider.APIKEY + "&category=" + menu.id + "&start_date=" + dataprovider.startTime), menu.url);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                MessageBox.Show("Can't download events");
            }
        }

        private void downloadCompleted(object sender, DownloadStringCompletedEventArgs ex)
        {
            try
            {
                Uri url = ex.UserState as Uri;
                if(url != null){
                    model.Events data = JsonConvert.DeserializeObject<model.Events>(ex.Result);
                    ObservableCollection<Pushpin> events = data.toList(url);
                    if (events == null)
                        MessageBox.Show("Can't download events");
                    else
                        foreach (Pushpin pushpin in events)
                        {
                            pushpin.MouseLeftButtonUp += new MouseButtonEventHandler(pushpin_MouseLeftButtonUp);
                            map.Children.Add(pushpin);
                        }
                }
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.ToString());
                MessageBox.Show("Can't download events");
            }
            finally
            {
                performanceProgressBar.IsIndeterminate = false;
            }
        }

        void pushpin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Pushpin pushpin = sender as Pushpin;
                int id = 0;
                if(int.TryParse(pushpin.Tag.ToString(), out id))
                    NavigationService.Navigate(new Uri("/views/Event.xaml?id=" + id, UriKind.Relative));
            }
            catch (Exception ex)
            {
            }
        }   
    }
}