﻿using System;
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


        public Pushpin toPushpin(Uri url)
        {
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(url);
            
            Pushpin pushpin = new Pushpin();
            pushpin.Location = toGeoCoordinate();
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

        public Pushpin toPushpin()
        {
            Pushpin pushpin = new Pushpin();
            pushpin.Location = toGeoCoordinate();
            pushpin.Content = title;
            return pushpin;
        }

        public GeoCoordinate toGeoCoordinate()
        {
            double tmpLat = 0, tmpLon = 0;
            double.TryParse(lat.ToString(), out tmpLat);
            double.TryParse(lon.ToString(), out tmpLon);
            return new GeoCoordinate(tmpLat, tmpLon);
        }
    }
}
