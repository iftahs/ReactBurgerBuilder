using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactBurgerBuilder.Models;
using System.Text.Json;
using ReactBurgerBuilder.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Newtonsoft.Json;

namespace ReactBurgerBuilder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase

    {
        public DataContext db = new DataContext();

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            try
            {
                db.Orders.Add(order);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<Order[]>> GetOrders()
        {
            Order[] orders = null;

            try
            {
                await Task.Run(() =>
                {
                    orders = db.Orders.ToArray<Order>();

                    foreach (Order o in orders)
                    {
                        Customer c = db.Clients.Find(o.customerId);
                        c.address = db.Addresses.Find(c.addressId);
                        o.customer = c;
                        
                        o.ingredients = db.Ingreidents.Where(ing => ing.orderId == o.id).ToList<Ingreident>();
                    }
                });

                return orders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}