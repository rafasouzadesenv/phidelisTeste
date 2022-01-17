using Microsoft.AspNetCore.Mvc;
using phidelisApi.Models;
using phidelisApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace phidelisApi.Controllers
{
    public class EnrolController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IEnrolService _enrolService;

        public EnrolController(IStudentService studentService, IEnrolService enrolService)
        {
            _studentService = studentService;
            _enrolService = enrolService;

        }

        [HttpPost("AddNewEnrollement")]
        public async Task<IActionResult> AddNewEnrollement(string newStudentName)
        {
            if (newStudentName != "")
            {
                _enrolService.AddStudentAndEnrolAsync(newStudentName);
                return Ok();
            }

            return Json(new { message = "Dados Inválidos." });



        }
        
        public IActionResult DeleteEnrollement(string idEnrollment)
        {

            if (idEnrollment != "")
            {

                _enrolService.DeleteEnrollmentById(Guid.Parse(idEnrollment));
                return Ok();
            }

            return Json(new { message = "Dados Inválidos." });



        }

        [HttpPost("UpdateEnrollement")]
        public IActionResult UpdateEnrollement(Enrollment enrollment)
        {

            var updateEnrollment = _enrolService.UpdateEnrollment(enrollment);
            if (updateEnrollment == null)
                return Json(new { message = "Matricula não encontrada." });
            else
                return Ok();



        }
        [HttpGet("GetAllEnrollements")]
        public JsonResult GetAllEnrollements()
        {
            return Json(_enrolService.GetAllEnrollments());
        }
        [HttpGet("ClearEnrollements")]
        public JsonResult ClearEnrollements()
        {
            _enrolService.DeleteAllEnrollment();
            return Json(new { message = "Dados Apagados." });
        }

        [HttpGet("GetEnrollmentByStudent")]
        public JsonResult GetEnrollmentByStudent(string nome)
        {
            return Json(_enrolService.FindEnrollmentStudentByName(nome));
        }
    }
}
