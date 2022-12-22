using Assignment.Models.DTO;
using Assignment.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("search")]
        public async Task<IActionResult> ReadExcel([FromBody] EmployeeSearchModel model)
        {
            var result = await _employeeService.GetEmployeesBySearch(model);
            return Ok(result);
        }
    }
}
