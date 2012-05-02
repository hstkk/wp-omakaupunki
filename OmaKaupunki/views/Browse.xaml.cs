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
using OmaKaupunki.controller;
using OmaKaupunki.model;
using System.Windows.Navigation;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace OmaKaupunki.views
{
    public partial class Browse : PhoneApplicationPage
    {
        int id;
        public Browse()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string tmp;

            if (NavigationContext.QueryString.TryGetValue("id", out tmp) && int.TryParse(tmp, out id))
            {
                Menu menu = (from m in App.menu
                            where m.id == id
                            select m).First();
                if (menu != null)
                {
                    PageTitle.Text = menu.title;
                    download();
                }
            }
        }

        private void download()
        {
            try
            {
                Dataprovider dataprovider = new Dataprovider();
                WebClient webClient = new WebClient();
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(downloadCompleted);
                webClient.DownloadStringAsync(new Uri(dataprovider.APIURL + "search?api_key=" + dataprovider.APIKEY + "&category=" + id + "&start_date=" + dataprovider.startTime));
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                err.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void downloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                model.Events data = JsonConvert.DeserializeObject<model.Events>(e.Result);
                ObservableCollection<model.Event> events = data.toList();
                if (events == null)
                    err.Visibility = System.Windows.Visibility.Visible;
                else
                    listBox.DataContext = events;

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                err.Visibility = System.Windows.Visibility.Visible;
            }
            finally
            {
                performanceProgressBar.IsIndeterminate = false;
            }
        }
        
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs ex)
        {
            // If selected index is -1 (no selection) do nothing
            if (listBox.SelectedIndex == -1)
                return;

            model.Event e = listBox.SelectedItem as model.Event;
            NavigationService.Navigate(new Uri("/Views/Event.xaml?id=" + e.id + "&category=" + id, UriKind.Relative));

            // Reset selected index to -1 (no selection)
            listBox.SelectedIndex = -1;
        }
    }
}