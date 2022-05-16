namespace Projekt2_tidrapportering.Data
{
    public class TimeRegister
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int AmountMinutes { get; set; }
        public string Description { get; set; }
        public Project Project { get; set; } = new Project();
        public Customer Customer { get; set; } = new Customer();
    }
}
