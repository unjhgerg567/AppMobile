using App1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    public partial class NewsPage : ContentPage
    {
        public NewsPage()
        {
            InitializeComponent();


        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await NewsDraw();

        }

        public async Task<bool> NewsDraw()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            using (HttpClient client = new HttpClient(handler))
            {
                string url = $"https://192.168.100.53:7268/api/Data/NewsDraw";



                var response = await client.GetStringAsync(url);

                // Десериализуем JSON-ответ в коллекцию туров
                var newsList = JsonConvert.DeserializeObject<List<News>>(response);

                // Привязываем коллекцию к tourListView
                newsListView.ItemsSource = newsList;

                return true;
            }
        }

        private void OnNewsSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {

                var selected = (News)e.SelectedItem;
                DisplayAlert("Selected News", $"Title: {selected.Title},\n Content: {selected.Content},\n Date: {selected.PublishedAt}", "OK");


                ((ListView)sender).SelectedItem = null;
            }
        }

    }
}