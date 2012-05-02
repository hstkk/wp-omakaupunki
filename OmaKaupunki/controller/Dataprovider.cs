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
using System.IO;
using System.Text;
using OmaKaupunki.model;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Collections.Generic;

/**
 * http://api.omakaupunki.fi/v1/event?api_key=262d6328-82f7-11e1-a468-000c29f7271d
http://api.omakaupunki.fi/v1/event/categories?api_key=262d6328-82f7-11e1-a468-000c29f7271d
http://api.omakaupunki.fi/v1/event/categories/108?api_key=262d6328-82f7-11e1-a468-000c29f7271d
http://api.omakaupunki.fi/v1/event/search?start_date=2012-04-12&category=9&api_key=262d6328-82f7-11e1-a468-000c29f7271d

 * JSON
 * http://pietschsoft.com/post/2008/02/NET-35-JSON-Serialization-using-the-DataContractJsonSerializer.aspx
 * http://www.smallandmighty.net/blog/using-json-with-windows-phone
 * http://debugmode.net/2011/12/22/how-to-consume-wcf-rest-service-with-json-in-windows-phone-7/
 * http://msdn.microsoft.com/en-us/library/bb410770.aspx
 * http://msdn.microsoft.com/en-us/library/system.runtime.serialization.json.datacontractjsonserializer%28v=vs.95%29.aspx
 * http://msdn.microsoft.com/en-us/library/system.runtime.serialization.datacontractjsonserializer%28v=VS.90%29.aspx?appId=Dev10IDEF1&l=EN-US&k=k%28DATACONTRACTJSONSERIALIZER%29&rd=true
 * http://msdn.microsoft.com/en-us/library/system.runtime.serialization.json.datacontractjsonserializer%28v=vs.95%29.aspx
 * http://msdn.microsoft.com/en-us/library/bb412179.aspx
 */
namespace OmaKaupunki.controller
{
    public class Dataprovider
    {
        private const string APIKEY = "262d6328-82f7-11e1-a468-000c29f7271d";
        private const string APIURL = "http://api.omakaupunki.fi/v1/event/";
        private const string STARTTIMEFORMAT = "yyyy-MM-dd";
        private string startTime;

        public Dataprovider()
        {
            startTime = (DateTime.Now).ToString(STARTTIMEFORMAT);
        }

        public Events getEvents(int gategory)
        {
            return downloadEvents(gategory);
        }

        private Events downloadEvents(int gategory)
        {
            Events events = new Events();
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(downloadEventsCompleted);
                webClient.DownloadStringAsync(new Uri(APIURL + "search?api_key=" + APIKEY + "&category=" + gategory + "&start_date=" + startTime));
            }
            catch
            {
                MessageBox.Show("Can not download events");
            }
            return events;
        }

        private void downloadEventsCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                Events events = JsonConvert.DeserializeObject<Events>(e.Result);
                if (events.toList() == null)
                    MessageBox.Show("Null");
                MessageBox.Show(events.toList()[0].title + "\n" + events.toList().Count);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }




/*        public void test(){
            Categories category = downloadCategories(new Uri("http://api.omakaupunki.fi/v1/event/categories?api_key=262d6328-82f7-11e1-a468-000c29f7271d"));
            MessageBox.Show((category!=null)? "ok": "EPIC FAIL!!!");
        }


        private Categories downloadCategories(Uri uri)
        {
            Categories categories = new Categories();
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(downloadCategoriesCompleted);
                webClient.DownloadStringAsync(uri);
            }
            catch
            {
                MessageBox.Show("Can not download events");
            }
            return categories;
        }

        private void downloadCategoriesCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(e.Result));
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Categories));
                Categories categories = (Categories)serializer.ReadObject(memoryStream);
                //if(categories == null)
                    //throw exception;
            }
            catch(Exception ex) {
                //TODO error
            }
        }

        private void downloadCompleted_(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(e.Result));
                ObservableCollection<Category> categories = new ObservableCollection<Category>();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ObservableCollection<Category>));
                categories = (ObservableCollection<Category>)serializer.ReadObject(memoryStream);
                /*foreach (Beacon b in list)
                {
                    RideList.Add(new RideDataModel { Date = b.Timestamp.ToString(), Description = b.Description, Distance = b.DistanceFromCustomer.ToString(), Name = b.Id.ToString(), Location = string.Format("{0},{1}", b.Location.Latitude.ToString(), b.Location.Longitude.ToString()), Address = b.Location.Address });
                }*//*
                MessageBox.Show((categories.Count != 0) ? "ok" : "EPIC FAIL!!!");

                MessageBox.Show(e.Result);
            }
            catch
            {
                //TODO error
            }
        }*/
    }
}
