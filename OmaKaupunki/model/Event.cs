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
using Microsoft.Phone.Controls.Maps;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using System.ComponentModel;

namespace OmaKaupunki.model
{
    public class Event
    {
        [DefaultValue(-1)]
        public int? id;
        [DefaultValue("")]
        public string title;
        [DefaultValue("")]
        public string body;
        [DefaultValue("")]
        public string url;
        [DefaultValue(-1)]
        public long? start_time;
        [DefaultValue(-1.0)]
        public double? lat;
        [DefaultValue(-1.0)]
        public double? lon;

        public Event()
        {
        }

        public override string ToString()
        {
            return title;
        }

        public bool show()
        {
            bool boolean = false;
            try
            {
                if (id > 0 && start_time > 0 && lat > 0 && lon > 0 && !title.Equals(""))
                    boolean = true;
            }catch(Exception e)
            {
            }
            return boolean;
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
//            pushpin.Location = new GeoCoordinate(lat, lon);
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
