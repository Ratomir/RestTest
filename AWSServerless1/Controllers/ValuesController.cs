using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestTest
{
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ILogger _logger { get; set; }
        public IConfiguration _configuration { get; set; }
        public ValuesController(ILogger<ValuesController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("version")]
        public string GetVersionOfCode()
        {
            return _configuration.GetValue<string>("Version");
        }

        [HttpGet("exception")]
        public string GetException()
        {
            throw new Exception("Look at me man. I am exception");
        }

        [HttpGet("loginfo")]
        public ObjectResult GetInfo()
        {
            _logger.LogInformation("Find the log!!!!");
            return Ok(new { message = "Go and find it" });
        }
    }
}
