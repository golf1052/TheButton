using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheButton.Controllers
{
    [Route("api/[controller]")]
    public class StatsController : Controller
    {
        public StatsController()
        {

        }

        [HttpGet("all")]
        public JArray GetAllStats()
        {
            return Startup.ButtonPressesInstance;
        }
    }
}
