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
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace OmaKaupunki.model
{
    [DataContract]
    public class Events
    {
        [DataMember]
        public List<Pagination> pagination;
        [DataMember]
        public List<Event> data;

        public List<Event> toList(Menu menu)
        {
            return data;
        }
    }
}
