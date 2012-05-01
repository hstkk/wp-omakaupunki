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
using System.Runtime.Serialization;

namespace OmaKaupunki.model
{
    [DataContract]
    public class Pagination
    {
        [DataMember]
        public int total;
        [DataMember]
        public int page;
        [DataMember]
        public int pagesize;
    }
}
