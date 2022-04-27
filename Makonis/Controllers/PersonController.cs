using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;


namespace Makonis.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IHostEnvironment _host;
        public PersonController(ILogger<PersonController> logger, IHostEnvironment host)
        {
            _logger = logger;
            _host = host;
        }

        [HttpPost]
        public ActionResult<Person> CreatePerson(Person person)
        {
            try
            {
                string json = JsonSerializer.Serialize(person);
                var filename = $@"person.json";
                string path = _host.ContentRootPath;
                System.IO.File.AppendAllText(path + "\\wwwroot\\" + filename, json);                

                return StatusCode(StatusCodes.Status200OK,"Inserted Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error while inserting new record");
            }
        }
    }

    public class Person
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }
    }
}
