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
    public partial class Categories : PhoneApplicationPage
    {
        public Categories()
        {
            InitializeComponent();
            DataContext = App.menu;
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (listBox.SelectedIndex == -1 || listBox.SelectedItem == null)
                return;

            Menu menu = listBox.SelectedItem as Menu;
            NavigationService.Navigate(new Uri("/Views/Browse.xaml?id=" + menu.id, UriKind.Relative));

            // Reset selected index to -1 (no selection)
            listBox.SelectedIndex = -1;
        }
    }
}