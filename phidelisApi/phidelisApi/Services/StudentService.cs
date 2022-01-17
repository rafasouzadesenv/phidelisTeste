using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using phidelisApi.Context;
using phidelisApi.Models;
using phidelisApi.Repository;
using phidelisApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace phidelisApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly PhidelisDbContext _context;
        private readonly IEnrolRepository _enrolRepository;

        public StudentService(PhidelisDbContext context, IEnrolRepository enrolRepository)
        {
            _context = context;
            _enrolRepository = enrolRepository;
        }

        public Student AddStudent(string studentName)
        {
            var student = new Student();
            student.IdStudent = Guid.NewGuid();
            student.Name = studentName;
            _enrolRepository.AddStudent(student);            
            return student;
            
        }

        public Student GetStudentByName(string name)
        {
            var student = _context.Students.Where(s => s.Name.Equals(name)).FirstOrDefault();
            if (student != null)
            {
                return student;
            }

            return null;
                
        }
    }
}
