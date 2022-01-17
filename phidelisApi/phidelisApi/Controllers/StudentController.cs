using Microsoft.AspNetCore.Mvc;
using phidelisApi.Models;
using phidelisApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace phidelisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]        
        public IActionResult AddStudent(string newStudentName)
        {
            if (newStudentName != "")
            {
                _studentService.AddStudent(newStudentName);
                return Ok();
            }

            return Json(new { message = "Dados Inválidos." });

            

        }
    }
}
