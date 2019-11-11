using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReactBurgerBuilder.Models
{
    public class Customer
    {
        [Key]
        public int customerId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int addressId { get; set; }
        public Address address { get; set; }
    }
}
