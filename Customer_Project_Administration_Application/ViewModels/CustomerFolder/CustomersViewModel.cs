namespace Customer_Project_Administration_Application.ViewModels.CustomerFolder
{
    
    public class CustomersViewModel
    {
        public List<CustomerItemViewModel> Customers { get; set; }

        public class CustomerItemViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        
    }
}
