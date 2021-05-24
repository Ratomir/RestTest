using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestTest
{
    [Route("api/[controller]")]
    [ApiController]
    public class LongTaskController : ControllerBase
    {
        public ILogger _logger { get; set; }
        public LongTaskController(ILogger<LongTaskController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogTrace("Long running action");

            Thread.Sleep(10000);

            return new string[] { "Action is done, check the file." };
        }
    }
}
