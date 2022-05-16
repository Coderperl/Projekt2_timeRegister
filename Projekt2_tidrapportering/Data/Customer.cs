namespace Projekt2_tidrapportering.Data
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Project> Projects { get; set; } = new List<Project>();
        public List<TimeRegister> TimeRegisters { get; set; } = new List<TimeRegister>();
    }
}
