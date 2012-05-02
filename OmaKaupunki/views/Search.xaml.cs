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
using OmaKaupunki.model;

namespace OmaKaupunki.views
{
    public partial class Search : PhoneApplicationPage
    {
        public Search()
        {
            InitializeComponent();
            DataContext = App.menu;
        }

        private void search_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/Browse.xaml?area=" + (area.Items[area.SelectedIndex] as ListPickerItem).Tag.ToString() + "&start_date=" + when.Value.Value.ToString("yyyy-MM-dd") + "&category=" + (category.Items[category.SelectedIndex] as Menu).id + "&text=" + keyword.Text, UriKind.Relative));
        }
    }
}