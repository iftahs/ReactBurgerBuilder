using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReactBurgerBuilder.Models
{
    public class Order
    {
        public ICollection<Ingreident> ingredients { get; set; }
        public double totalPrice { get; set; }
        public Customer customer { get; set; }
        public string deliveryMethod { get; set; }
        [Key]
        public int id { get; set; }
    }
}
