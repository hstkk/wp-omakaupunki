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
using System.Windows.Navigation;

namespace OmaKaupunki
{
    public partial class Search : PhoneApplicationPage
    {
        public Search()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string tmp;
            int selectedIndex;
            if (NavigationContext.QueryString.TryGetValue("pivot.SelectedIndex", out tmp) && int.TryParse(tmp, out selectedIndex))
                pivot.SelectedIndex = selectedIndex;
        }
    }
}