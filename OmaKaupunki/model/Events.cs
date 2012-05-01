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
using System.Collections.Generic;
using System.Linq;
using Microsoft.Phone.Controls.Maps;
using Newtonsoft.Json;

namespace OmaKaupunki.model
{
    public class Events
    {
        //public Pagination[] pagination;
        public Event[] data;

        public List<Pushpin> toList(Menu menu)
        {
            List<Pushpin> pushpins = new List<Pushpin>();
            if (data != null && menu != null)
            {
                data = (from e in data
                        where e.show()
                        select e).ToArray<Event>();
                foreach(Event e in data)
                    pushpins.Add(e.toPushpin(menu));
            }
            return pushpins;
        }

/*        public List<Event> toList()
        {
            return (from e in data
                    where e.id != null &&
                    e.lat != null &&
                    e.lon != null &&
                    e.start_time != null &&
                    e.body != null
                    select e).ToList<Event>();
        }*/

        public List<Event> toList()
        {
            if (data != null)
                return (from e in data
                        where e.show()
                        select e).ToList<Event>();
            else
                return null;
        }
    }
}
