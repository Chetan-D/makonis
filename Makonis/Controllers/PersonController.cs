using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;


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
               
                var filename = $@"person.json";
                string path = _host.ContentRootPath + "\\wwwroot\\" + filename;

                var jsonData = System.IO.File.ReadAllText(path);
                var personsList = JsonConvert.DeserializeObject<List<Person>>(jsonData)
                      ?? new List<Person>();

                personsList.Add(person);

                jsonData = JsonConvert.SerializeObject(personsList);
                System.IO.File.WriteAllText(path, jsonData);                      

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
