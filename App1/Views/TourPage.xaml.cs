using App1.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Net.Security;
using ModernHttpClient;
using App1.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TourPage : ContentPage
    {
        public TourPage()
        {
            InitializeComponent();
        }



        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await TourDraw();
              
        }

        public async Task<bool> TourDraw()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            using (HttpClient client = new HttpClient(handler))
            {
                string url = $"https://192.168.100.53:7268/api/Data/TourDraw";



                var response = await client.GetStringAsync(url);

                // Десериализуем JSON-ответ в коллекцию туров
                var tourList = JsonConvert.DeserializeObject<List<Tour>>(response);

                // Привязываем коллекцию к tourListView
                tourListView.ItemsSource = tourList;

                return true;
            }
        }



        public async Task<bool> RemoveTour(int id)
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            using (HttpClient client = new HttpClient(handler))
            {
                string url = $"https://192.168.100.53:7268/api/Data/RemoveTour/{id}";



                HttpResponseMessage response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    
                    Console.WriteLine($"Error: {response.StatusCode}");
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error Content: {content}");

                    return false;
                }
            }
        }



        //private async void OnRemoveButtonClickedAsync(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        await RemoveTour(19);

        //        await DisplayAlert("Function", "END", "Okay");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message.ToString());
        //        await DisplayAlert("Error", ex.Message, "Okay");
        //    }
        //}

        private void OnTourSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                // Обработка выбора элемента
                var selectedTour = (Tour)e.SelectedItem;
                DisplayAlert("Selected Tour", $"TourName: {selectedTour.TourName}, Destination: {selectedTour.Destination}", "OK");

                // Сброс выделения
                ((ListView)sender).SelectedItem = null;
            }
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            // Обработка нажатия кнопки
            Button button = (Button)sender;
            object tourIdObject = button.CommandParameter;

            // Преобразование объекта в int
            if (tourIdObject != null)
            {
                try
                {
                    int tourId = Convert.ToInt32(tourIdObject);

                    // Вызов функции с использованием tourId
                    await RemoveTour(tourId);
                }
                catch (FormatException ex)
                {
                    // Обработка ошибки при невозможности преобразования в int
                    Console.WriteLine($"Ошибка преобразования: {ex.Message}");
                }
            }
        }


    }
}