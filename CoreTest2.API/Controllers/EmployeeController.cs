using CoreTest2.BLL.Interfaces;
using CoreTest2.Common;
using CoreTest2.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CoreTest2.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class EmployeeController : Controller
    {
        private readonly ICoreTest2Service _service;

        public EmployeeController(ICoreTest2Service service)
        {
            _service = service;
        }

        [HttpGet("{page}")]
        public IActionResult GetPage(int page, [FromBody] SortingCondition condition)
        {
            try
            {                
                var result = _service.GetEmployeesAsSortedArray(page, condition ?? SortingCondition.Default);
                return Ok(result);
            }
            catch (ArgumentException)
            {
                return NoContent();
                
            }
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _service.GetEmployeeByID(id);
                return Ok(result);
            }
            catch (ArgumentException)
            {
                return NoContent();
            }
        }

        [HttpPost]
        public void Post([FromBody]Employee value)
        {
            _service.AddEmployee(value);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Employee value)
        {
            try
            {
                _service.ChangeEmployee(id, value);
                return Ok();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.RemoveEmployee(id);
                return Ok();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }
    }
}