using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Projekt2_tidrapportering.DTOS.CustomerDTOS;

namespace Customer_Project_Administration_Application.Services
{
    public interface ICustomerService
    {
        public List<CustomersDTO> GetCustomers();
        public void UpdateCustomers();
        public void DeleteCustomers();
        public string CreateCustomers();
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

        public void UpdateCustomers()
        {
            throw new NotImplementedException();
        }

        public void DeleteCustomers()
        {
            throw new NotImplementedException();
        }

        public string CreateCustomers()
        {
            var httpClient = new HttpClient();
            var data = httpClient.GetStringAsync(_settings.Value.Url).Result;
            return JsonConvert.DeserializeObject<string>(data);
        }
        
    }
    public class CustomerSettings
    { 
        public string Url { get; set; }
    }
}
