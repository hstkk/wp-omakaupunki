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
using System.Windows.Media.Imaging;

namespace OmaKaupunki.model
{
    public class Menu
    {
        public string title { get; private set; }
        public int id { get; private set; }
        public Uri url { get; private set; }

        public Menu(string title, int id)
        {
            this.title = title;
            this.id = id;
            url = new Uri("/OmaKaupunki;component/icons/" + this.id + ".png", UriKind.Relative);
        }
    }
}
