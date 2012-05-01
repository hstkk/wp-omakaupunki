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

namespace OmaKaupunki.model
{
    [DataContract]
    public class Category
    {
        [DataMember]
        public int id;
        [DataMember]
        public int parent;
        [DataMember]
        public string name;
        [DataMember]
        public string plural;
        [DataMember]
        public string url;

        public Category()
        {
        }

        public Category(int id, int parent, string name, string plural, string url)
        {
            this.id = id;
            this.parent = parent;
            this.name = name;
            this.plural = plural;
            this.url = url;
        }
    }
}
