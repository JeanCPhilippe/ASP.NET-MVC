using System.Linq;
namespace SalesWebMvc.Models
{
    public class Department
    {
        public int id { get; set; }
        public string name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department() 
        { 
        }

        public Department(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime dateInitial, DateTime dateFinal)
        {
            return Sellers.Sum(sr => sr.TotalSales(dateInitial, dateFinal));
        }
    }
}
