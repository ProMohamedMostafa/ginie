using System;
using System.Collections.Generic;

#nullable disable

namespace GenieMistro.Models
{
    public partial class BusinessAccount
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public int NumberOfEmployees { get; set; }
        public string BusinessDescription { get; set; }
        public double SubscriptionYearsBackage { get; set; }
        public string BPhone { get; set; }
        public string BaPassword { get; set; }
        public string BaWebSite { get; set; }
    }
}
