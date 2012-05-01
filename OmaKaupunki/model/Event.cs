using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Device.Location;
using System.Runtime.Serialization;
using Microsoft.Phone.Controls.Maps;
using System.Windows.Media.Imaging;

namespace OmaKaupunki.model
{
    [DataContract]
    public class Event
    {
        [DataMember]
        public int id;
        [DataMember]
        public string title;
        [DataMember]
        public string body;
        [DataMember]
        public string url;
        [DataMember]
        public long start_time;
        [DataMember]
        public long end_time;
        [DataMember]
        public long created_at;
        [DataMember]
        public double lat;
        [DataMember]
        public double lon;
        [DataMember]
        public int venue;

        public Event()
        {
        }

        /*public Event(int id, string title, string body, string url, DateTime start_time, DateTime end_time, DateTime created_at, double lat, double lon, int venue)
        {
            this.id = id;
            this.title = title;
            this.body = body;
            this.url = url;
            this.startTime = start_time;
            this.endTime = end_time;
            this.createdAt = created_at;
            coordinate = new GeoCoordinate(lat, lon);
            this.venue = venue;
        }

        public Event(int id, string title, string url, DateTime start_time, DateTime end_time, DateTime created_at, double lat, double lon)
        {
            this.id = id;
            this.title = title;
            this.url = url;
            this.startTime = start_time;
            this.endTime = end_time;
            this.createdAt = created_at;
            coordinate = new GeoCoordinate(lat, lon);
        }*/

        public Pushpin toPushpin(Menu menu){
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(menu.url);

            Pushpin pushpin = new Pushpin();
            pushpin.Location = new GeoCoordinate(lat, lon);
            pushpin.Tag = id;
            pushpin.Content = new Rectangle()
            {
                Fill = imageBrush,
                StrokeThickness = 0,
                Height = 48,
                Width = 48
            };
            return pushpin;
        }
    }
}
