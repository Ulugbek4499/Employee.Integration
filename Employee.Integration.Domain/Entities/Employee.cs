﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Employee.Integration.Domain.Entities
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Payroll_Number { get; set; }
        public string Forenames { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Address_2 { get; set; }
        public string Postcode { get; set; }
        public string EMail_Home { get; set; }
        public DateTime StartDate { get; set; }
    }
}
