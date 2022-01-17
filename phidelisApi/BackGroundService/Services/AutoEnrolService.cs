using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace BackGroundService.Services
{
    public class AutoEnrolService : MonitorByTime
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private System.Timers.Timer _timerWatcher;
        HttpClient clientGerador;
        HttpClient clientApi;
        private IConfigurationSection _getUpdateConfig;
        private int _interval;
        private string _nameRandom = "";


        public AutoEnrolService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;

            clientApi = _httpClientFactory.CreateClient("Enrol");
            _getUpdateConfig = configuration.GetSection("updateIntervalMilliseconds");
            _interval = Convert.ToInt32(_getUpdateConfig.Value);

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timerWatcher = new System.Timers.Timer();
            _timerWatcher.Elapsed += new ElapsedEventHandler(EnrollmentConstructor);
            //_timerWatcher.Interval = 30000;
            _timerWatcher.Interval = _interval;

            _timerWatcher.Enabled = true;

            return Task.CompletedTask;

        }

        private void EnrollmentConstructor(object sender, ElapsedEventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                GenerateNewRegistration();
            }

        }

        private void SendNewStudentToApi(string newStudentName)
        {
            try
            {
                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("newStudentName", newStudentName)
                });
                var response = clientApi.PostAsync("https://localhost:44390/AddNewEnrollement", content);
            }
            catch (TaskCanceledException ex)
            {

                return;
            }
        }

        private async void GenerateNewRegistration()
        {

            try
            {
                HttpResponseMessage response = await clientApi.GetAsync("http://gerador-nomes.herokuapp.com/nome/aleatorio");
                if (response.IsSuccessStatusCode)
                {
                    string nameRandom = await response.Content.ReadAsStringAsync();
                    nameRandom = FormatName(nameRandom);
                    SendNewStudentToApi(nameRandom);


                }

            }
            catch (TaskCanceledException ex)
            {

            }
        }

        private string FormatName(string name)
        {
            name = name.Replace("\"", "");
            name = name.Replace("[", "");
            name = name.Replace("]", "");
            name = name.Replace(",", " ");
            return name;
        }
    }
}
