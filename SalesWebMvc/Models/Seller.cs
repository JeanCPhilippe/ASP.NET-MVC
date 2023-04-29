using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} required")]
        [StringLength(60,MinimumLength = 3,ErrorMessage = "{0} size should be between {2} and {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Enter a valid email")]
        public string Email { get; set; }

        [Display(Name = "Birth Date")]
        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0 : dd/MM/yyyy} ")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Base Salary")]
        [Range(100.00,50000.00,ErrorMessage ="{0} must be from {1} to {2}")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double BaseSalary { get; set; }

        public Department department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() 
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            this.department = department;
        }


        public void AddSales(SalesRecord record)
        {
            Sales.Add(record);
        }
        public void RemoveSales(SalesRecord record)
        {
            Sales.Remove(record);
        }
        public double TotalSales(DateTime dateInitial, DateTime dateFinal)
        {
            return Sales.Where(sr => sr.Date >= dateInitial && sr.Date <= dateFinal).Sum(sr => sr.Ammount);
        }

    }
}
