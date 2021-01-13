using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_React.Models.Data;
using Api_React.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api_React.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly ILogger<StudentController> _logger;
        private readonly IConfiguration _configuration;

        StudentData studentData;

        public StudentController(ILogger<StudentController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

        }

        // GET: api/<StudentController>
        [HttpGet]
        public List<Student> Get()
        {
            studentData = new StudentData(_configuration);
            return studentData.GetStudent().ToList<Student>();
            //return new string[] { "value1", "value2" };
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public Student Get(int id)
        {
            studentData = new StudentData(_configuration);
            return studentData.GetStudentById(id);
           
        }

        // POST api/<StudentController>
        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            studentData = new StudentData(_configuration);
            int resultToReturn = studentData.Insert(student);
            //llamada al modelo para insertar el estudiante
           return  Ok(resultToReturn);
                   }

        // PUT api/<StudentController>/5
        [HttpPut("{studentId}")]
        public IActionResult Put(int studentId, [FromBody] Student student)
        {
            studentData = new StudentData(_configuration);
            int resultToReturn = studentData.Update(studentId, student);
            return  Ok(resultToReturn);
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{studentId}")]
        public IActionResult Delete(int studentId)
        {
            studentData = new StudentData(_configuration);
            int resultToReturn = studentData.Delete(studentId);
            return Ok(resultToReturn);
        }
    }
}
