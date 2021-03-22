using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AWSMarketAPI.Controllers.Base
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StockBaseController : ControllerBase
    {
        public readonly ILogger _logger;
        public StockBaseController(ILogger<StockBaseController> logger)
        {
            _logger = logger;
        }
    }
}
