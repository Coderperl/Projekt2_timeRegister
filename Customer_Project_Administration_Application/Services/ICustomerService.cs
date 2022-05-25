using Projekt2_tidrapportering.DTOS.CustomerDTOS;

namespace Customer_Project_Administration_Application.Services;

public interface ICustomerService
{
    public List<CustomersDTO> GetCustomers();
    public Status UpdateCustomers(UpdateCustomerDTO customer, int Id);
    public Status DeleteCustomers(int Id);
    public Status CreateCustomers(CreateCustomerDTO customer);
}