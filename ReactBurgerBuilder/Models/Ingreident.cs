using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReactBurgerBuilder.Models
{
    public class Ingreident
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int amount { get; set; }

        [ForeignKey("Order")]
        public int orderId { get; set; }
        public Order order { get; set; }
    }
}
