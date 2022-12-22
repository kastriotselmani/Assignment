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
    public class ReadController : ControllerBase
    {
        private readonly IReadExcelFileService _readExcelFileService;

        public ReadController(IReadExcelFileService readExcelFileService)
        {
            _readExcelFileService = readExcelFileService;
        }

        [HttpPost("excel")]
        public async Task<IActionResult> ReadExcel([FromForm(Name = "file")] IFormFile file)
        {
            try
            {
                await _readExcelFileService.ReadExcelAsync(file);
                return Ok(new { success = true, message = "Successfully imported" });

            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }
    }
}
