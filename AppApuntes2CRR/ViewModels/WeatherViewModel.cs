using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppApuntes2CRR.Models;
using AppApuntes2CRR.Services;

namespace AppApuntes2CRR.ViewModels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private WeatherData _weatherData = new WeatherData();
        public WeatherData WeatherDataInfo
        {
            get => _weatherData;
            set
            {
                if (_weatherData != value)
                {
                    _weatherData = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand RecalculateWeather { get; set; }
        public WeatherViewModel()
        {
            GetCurrentWeatherFromLocation();
            RecalculateWeather = new Command(async () => GetWeatherFromLocation());
        }
        public async void GetWeatherFromLocation()
        {
            WeatherServices weatherServices = new WeatherServices();
            WeatherDataInfo = await weatherServices.GetWeatherDataFromLocationAsync(_weatherData.latitude, _weatherData.longitude);
        }
        public async void GetCurrentWeatherFromLocation()
        {
            WeatherServices weatherServices = new WeatherServices();
            WeatherDataInfo = await weatherServices.GetCurrentLocationWeatherAsync();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

