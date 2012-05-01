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
/*
 * usb stroage http://forum.xda-developers.com/showthread.php?t=1069568
 * 
 * http://msdn.microsoft.com/en-us/library/ff431782%28v=vs.92%29.aspx
 * http://create.msdn.com/en-US/education/quickstarts/Developing_with_the_Windows_Phone_GPS_%28Location_Services%29
 * http://msdn.microsoft.com/en-us/library/gg588383%28v=vs.92%29.aspx
 * http://msdn.microsoft.com/en-us/library/ff941093%28v=vs.92%29.aspx
 * http://msdn.microsoft.com/en-us/library/ee681883.aspx
 * http://msdn.microsoft.com/en-us/library/hh202984%28v=vs.92%29.aspx
 * http://slartoolkit.codeplex.com/wikipage?title=Beginner%27s%20Guide&referringTitle=Documentation
 * http://vimeo.com/27377090
 * http://vimeo.com/27378156
 * http://blog.markarteaga.com/AugmentedRealityAndWindowsPhone7.aspx
 * http://channel9.msdn.com/coding4fun/blog/GART-The-Geo-AR-Augmented-Reality-Toolkit-for-Windows-Phone-715
 * http://slartoolkit.codeplex.com/
 * http://msdn.microsoft.com/en-us/library/ff431803%28v=vs.92%29.aspx
 * 
 * http://mobiforge.com/designing/story/building-location-service-apps-windows-phone-7
 * 
 * reititys
 * http://blogs.msdn.com/b/ukmsdn/archive/2012/03/21/how-to-integrate-real-time-data-on-an-interactive-bing-map.aspx
 * http://msdn.microsoft.com/en-us/library/cc980922.aspx
 * http://blogs.msdn.com/b/mikebattista/archive/2011/01/18/geocoding-and-routing-with-bing-maps.aspx
 * 
 * 
 * json
 * http://msdn.microsoft.com/en-us/library/system.runtime.serialization.datacontractjsonserializer%28v=VS.90%29.aspx?appId=Dev10IDEF1&l=EN-US&k=k%28DATACONTRACTJSONSERIALIZER%29&rd=true
 * http://msdn.microsoft.com/en-us/library/bb412179.aspx
 * http://pietschsoft.com/post/2008/02/NET-35-JSON-Serialization-using-the-DataContractJsonSerializer.aspx
 * http://www.smallandmighty.net/blog/using-json-with-windows-phone
 * 
 * hubtile
 * http://www.windowsphonegeek.com/articles/Windows-Phone-HubTile-in-depth-Part1-key-concepts-and-API
 * http://www.windowsphonegeek.com/articles/How-to-Programmatically-switch-the-HubTile-Visual-States
 * http://blog.humann.info/post/2011/11/06/Silverlight-Toolkit-bug-on-HubTile-control-the-solution.aspx
 * http://www.windowsphonegeek.com/articles/Programmatically-changing-Visual-States-of-HubTile-controls-used-as-ListBox-Items
 * http://www.windowsphonegeek.com/articles/Windows-Phone-HubTile-in-depth-Part3-Freezing-and-Unfreezing-tiles
 * http://stackoverflow.com/questions/9149812/unable-to-put-images-into-hub-tile
 * http://igrali.wordpress.com/2011/08/19/how-to-use-the-hubtile-control/
 */
namespace OmaKaupunki
{
    public partial class MainPage : PhoneApplicationPage
    {
        GeoCoordinateWatcher watcher;
        public MainPage()
        {
            InitializeComponent();
            map.ZoomLevel = 15;
            watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
            watcher.MovementThreshold = 20;
            watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(onStatusChanged);
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(onPositionChanged);
            watcher.Start();
            controller.Dataprovider dataprovider = new controller.Dataprovider();
            dataprovider.test();
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
            map.Center = e.Position.Location;
        }

        private void nearMe(object sender, EventArgs e)
        {
            watcher.Start();
            //centerMap();
        }

        private void browse(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/browse.xaml", UriKind.Relative));
        }

        private void search(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/search.xaml", UriKind.Relative));
        }

        /*private void centerMap()
        {
            map.Center = userLocation;
        }*/
    }
}