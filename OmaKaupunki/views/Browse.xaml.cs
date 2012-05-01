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

namespace OmaKaupunki
{
    public partial class Browse : PhoneApplicationPage
    {
        public Browse()
        {
            InitializeComponent();
        }

        private void search(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/search.xaml?pivot.SelectedIndex=" + pivot.SelectedIndex, UriKind.Relative));
        }
    }
}