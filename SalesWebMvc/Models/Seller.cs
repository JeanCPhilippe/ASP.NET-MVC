﻿using System.Linq;
namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }

        public Department department { get; set; }
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