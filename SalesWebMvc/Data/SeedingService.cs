using SalesWebMvc.Models;
using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Data
{
    public class SeedingService
    {
        private SalesWebMvcContext _context;

        public SeedingService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Department.Any() 
                || _context.Seller.Any() 
                || _context.SalesRecord.Any())
            {
                return; //Banco de dados já foi populado
            }

            Department d1 = new Department(1,"Hardware");
            Department d2 = new Department(2, "Games");
            Department d3 = new Department(3, "Mouse and Keyboard");
            Department d4 = new Department(4, "Monitor");
            Department d5 = new Department(5, "MotherBoard");
            Department d6 = new Department(6, "DRAM");

            Seller s1 = new Seller(1,"Jean","jean@gmail.com",new DateTime(1999,11,30),3000,d2);
            Seller s2 = new Seller(2, "Luana", "luana@gmail.com", new DateTime(2000, 10, 26), 2500, d3);
            Seller s3 = new Seller(3, "Cairo", "cairo@gmail.com", new DateTime(2001, 08, 15), 2000, d4);
            Seller s4 = new Seller(4, "Michel", "Michel@gmail.com", new DateTime(1999, 07, 30), 2700, d1);
            Seller s5 = new Seller(5, "Lucas", "lucas@gmail.com", new DateTime(2001, 11, 08), 2000, d5);
            Seller s6 = new Seller(6, "Luan", "Luan@gmail.com", new DateTime(1998, 05, 10), 2350, d6);

            SalesRecord r1 = new SalesRecord(1,new DateTime(2023,03,15),10000.0,SaleStatus.Billed,s4);
            SalesRecord r2 = new SalesRecord(2, new DateTime(2023, 03, 15), 500.0, SaleStatus.Billed, s1);
            SalesRecord r3 = new SalesRecord(3, new DateTime(2023, 03, 15), 250.0, SaleStatus.Billed, s2);
            SalesRecord r4 = new SalesRecord(4, new DateTime(2023, 04, 14), 1799.9, SaleStatus.Billed, s5);
            SalesRecord r5 = new SalesRecord(5, new DateTime(2023, 04, 14), 899.9, SaleStatus.Billed, s5);
            SalesRecord r6 = new SalesRecord(6, new DateTime(2023, 04, 01), 709.9, SaleStatus.Billed, s6);
            SalesRecord r7 = new SalesRecord(7, new DateTime(2023, 04, 20), 659.9, SaleStatus.Pending, s6);
            SalesRecord r8 = new SalesRecord(8, new DateTime(2023, 04, 22), 1199.9, SaleStatus.Pending, s3);
            SalesRecord r9 = new SalesRecord(9, new DateTime(2023, 04, 22), 600.0, SaleStatus.Pending, s1);

            _context.Department.AddRange(d1, d2, d3, d4, d5, d6);
            _context.Seller.AddRange(s1, s2, s3, s4, s5, s6);
            _context.SalesRecord.AddRange(r1, r2, r3, r4, r5, r6, r7, r8, r9);

            _context.SaveChanges();
        }
    }
}
