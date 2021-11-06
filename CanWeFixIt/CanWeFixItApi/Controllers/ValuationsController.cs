using CanWeFixItService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CanWeFixItApi.Controllers
{
    [ApiController]
    [Route("v1/valuations")]
    public class ValuationsController : Controller
    {
        private readonly IDatabaseService _database;

        public ValuationsController(IDatabaseService database)
        {
            _database = database;
        }
        // GET
        public async Task<ActionResult<IEnumerable<MarketValuation>>> Get()
        {
            // TODO:
            return Ok(await _database.MarketValuation());
        }
    }
}
