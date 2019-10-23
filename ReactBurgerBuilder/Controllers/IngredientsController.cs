using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactBurgerBuilder.Models;

namespace ReactBurgerBuilder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        [HttpGet]
        public Dictionary<string, int> Get()
        {
            Dictionary<string, int> ingreidents = new Dictionary<string, int>();

            ingreidents["salad"] = 0;
            ingreidents["bacon"] = 0;
            ingreidents["cheese"] = 0;
            ingreidents["meat"] = 0;

            return ingreidents;
        }
    }
}