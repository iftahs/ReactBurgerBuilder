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
        DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
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
                    orders = _context.Orders.ToArray<Order>();

                    foreach (Order o in orders)
                    {
                        Customer c = _context.Clients.Find(o.customerId);
                        c.address = _context.Addresses.Find(c.addressId);
                        o.customer = c;
                        
                        o.ingredients = _context.Ingreidents.Where(ing => ing.orderId == o.id).ToList<Ingreident>();
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