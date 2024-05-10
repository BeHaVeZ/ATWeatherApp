using ATWeatherApp.Model;
using ATWeatherApp.View;
using ATWeatherApp.ViewModel.Commands;
using ATWeatherApp.ViewModel.Helpers;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ATWeatherApp.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        private string query;

        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                OnPropertyChanged("Query");
            }
        }

        public ObservableCollection<City> Cities { get; set; }

        private CurrentConditions currentConditions;

        private Location currentLocation;

        public Location CurrentLocation
        {
            get => currentLocation;
            set => currentLocation = value;
        }

        public CurrentConditions CurrentConditions
        {
            get { return currentConditions; }
            set
            {
                currentConditions = value;
                OnPropertyChanged("CurrentConditions");
            }
        }

        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {
                if (value != selectedCity)
                {
                    selectedCity = value;
                    OnPropertyChanged("SelectedCity");

                    GetCurrentConditions();
                    GetCurrentLocation();
                }
            }
        }

        public SearchCommand SearchCommand { get; set; }



        public WeatherVM()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City
                {
                    LocalizedName = ""
                };
                CurrentConditions = new CurrentConditions
                {
                    WeatherText = "",
                    Temperature = new Temperature
                    {
                        Metric = new Units
                        {
                            Value = ""
                        }
                    }
                };
            }

            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
        }

        private void OpenDetailsWindow()
        {
            DetailsWindow detailsWindow = new DetailsWindow();
            detailsWindow.Show();
        }


        private async void GetCurrentLocation()
        {
            if (SelectedCity != null)
            {
                CurrentLocation = await AccuWeatherHelper.GetCurrentLocation(SelectedCity.Key);
            }
        }

        private async void GetCurrentConditions()
        {
            if (SelectedCity != null)
            {
                Cities.Clear();
                Query = string.Empty;
                CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key);
            }
        }


        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);

            Cities.Clear();
            foreach (var city in cities)
            {
                Cities.Add(city);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
