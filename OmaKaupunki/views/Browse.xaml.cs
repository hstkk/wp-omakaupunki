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
using System.Linq;


namespace OmaKaupunki.views
{
    public partial class Browse : PhoneApplicationPage
    {
        public Browse()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string tmp;
            int id;
            Events events;
            Dataprovider dataprovider = new Dataprovider();

            if (NavigationContext.QueryString.TryGetValue("id", out tmp) && int.TryParse(tmp, out id))
            {
                Menu menu = (from m in App.menu
                            where m.id == id
                            select m).First();
                if (menu != null)
                {
                    PageTitle.Text = menu.title.ToLower();
                    events = dataprovider.getEvents(id);
                    List<model.Event> data = events.toList();
                    listBox.DataContext = data;
                    if(data == null)
                        err.Visibility = System.Windows.Visibility.Visible;
                }
            }
            else
            {
                PageTitle.Text = "hakutulokset";                
            }
            /*if (events != null)
                longListSelector.DataContext = events.toList();
            else
                err.Visibility = System.Windows.Visibility.Visible;*/
        }
    }
}