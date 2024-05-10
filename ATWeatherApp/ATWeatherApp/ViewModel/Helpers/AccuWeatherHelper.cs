using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ATWeatherApp.Model;
using Newtonsoft.Json;

namespace ATWeatherApp.ViewModel.Helpers
{
    internal class AccuWeatherHelper
    {
        public const string? BASE_URL = "http://dataservice.accuweather.com/";
        public const string? AUTOCOMPLETE_EP = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string? CURRENT_CONDITIONS_EP = "currentconditions/v1/{0}?apikey={1}";
        public const string? LOCATION_EP = "locations/v1/{0}?apikey={1}";
        public const string? API_KEY = "UZ9aA0SjmKZG6l7Afzhd2euI3G7GwoK4";
        public static string? DETAILED_IMAGE_LINK = "";

        public static async Task<List<City>> GetCities(string query)
        {
            List<City> cityList = new List<City>();

            string? url = BASE_URL + string.Format(AUTOCOMPLETE_EP, API_KEY, query);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                cityList = JsonConvert.DeserializeObject<List<City>>(json);
            }

            return cityList;
        }

        public static async Task<CurrentConditions> GetCurrentConditions(string cityKey)
        {
            CurrentConditions currentConditions = new CurrentConditions();

            string? url = BASE_URL + string.Format(CURRENT_CONDITIONS_EP, cityKey, API_KEY);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                currentConditions = (JsonConvert.DeserializeObject<List<CurrentConditions>>(json)).FirstOrDefault();
            }

            return currentConditions;
        }
        public static async Task<Location> GetCurrentLocation(string cityKey)
        {
            Location location = new Location();

            string? url = BASE_URL + string.Format(LOCATION_EP, cityKey, API_KEY);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                location = (JsonConvert.DeserializeObject<Location>(json));
            }
            SetDetailedImage(location);
            return location;
        }

        private static void SetDetailedImage(Location currentLocation)
        {
            if (currentLocation == null) return;
            DETAILED_IMAGE_LINK = string.Format("http://www.7timer.info/bin/astro.php?lon={0}&lat={1}&ac=0&lang=en&unit=metric&output=internal&tzshift=0", currentLocation.GeoPosition.Longitude, currentLocation.GeoPosition.Latitude);
        }
    }
}
