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
                switch (id)
                {
                    case 9:
                        PageTitle.Text = "elokuvat";
                        break;
                    case 2:
                        PageTitle.Text = "keikat";
                        break;
                    case 10:
                        PageTitle.Text = "klubit";
                        break;
                    case 8:
                        PageTitle.Text = "näyttelyt";
                        break;
                    case 7:
                        PageTitle.Text = "teatterit";
                        break;
                }
                events = dataprovider.getEvents(id);
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