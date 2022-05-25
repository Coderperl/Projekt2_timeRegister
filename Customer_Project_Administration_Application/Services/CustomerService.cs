using System.Net.Http.Headers;
using System.Text;
using Customer_Project_Administration_Application.ViewModels.CustomerFolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Projekt2_tidrapportering.DTOS.CustomerDTOS;

namespace Customer_Project_Administration_Application.Services
{
    public class CustomerSettings
    { 
        public string? Url { get; set; }
    }

    public class CustomerService : ICustomerService
    {
        private readonly IOptions<CustomerSettings> _settings;

        public CustomerService(IOptions<CustomerSettings> settings)
        {
            _settings = settings;
        }

        public List<CustomersDTO> GetCustomers()
        {
            var httpClient = new HttpClient();
            var data = httpClient.GetStringAsync(_settings.Value.Url).Result;
            return JsonConvert.DeserializeObject<List<CustomersDTO>>(data);
        }

        public Status UpdateCustomers(UpdateCustomerDTO customer, int Id)
        {
            var payload = JsonConvert.SerializeObject(customer);
            var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();
            var data = httpClient.PutAsync($"{_settings.Value.Url}/{Id}", httpContent).Result;
            if (data.IsSuccessStatusCode)
            {
                return Status.ok;
            }

            return Status.Error;
        }

        public Status DeleteCustomers(int Id)
        {
            var httpClient = new HttpClient();
            var data = httpClient.DeleteAsync($"{_settings.Value.Url}/{Id}").Result;
            if (data.IsSuccessStatusCode)
            {
                return Status.ok;
            }

            return Status.Error;
        }

        public Status CreateCustomers(CreateCustomerDTO customer)
        {
            var payload = JsonConvert.SerializeObject(customer);
            var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();
            var data = httpClient.PostAsync(_settings.Value.Url, httpContent).Result;
            if (data.IsSuccessStatusCode)
            {
                return Status.ok;
            }

            return Status.Error;
        }
    }
}