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
using System.Collections.Generic;
using System.Linq;
using Microsoft.Phone.Controls.Maps;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace OmaKaupunki.model
{
    public class Events
    {
        //public Pagination[] pagination;
        public Event[] data;

        public ObservableCollection<Pushpin> toList(Uri url)
        {
            ObservableCollection<Pushpin> pushpins = new ObservableCollection<Pushpin>();
            if (data != null && url != null)
            {
                data = (from e in data
                        where e.show()
                        orderby e.start_time
                        select e).ToArray<Event>();
                foreach(Event e in data)
                    pushpins.Add(e.toPushpin(url));
            }
            return pushpins;
        }

        public ObservableCollection<Event> toList()
        {
            if (data != null)
            {
                List<Event> tmp = (from e in data
                                   where e.show()
                                   orderby e.start_time
                                   select e).ToList<Event>();
                return new ObservableCollection<Event>(tmp);
            }
            else
                return null;
        }
    }
}
