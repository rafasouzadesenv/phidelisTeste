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
    public class EnrolService : IEnrolService
    {
        
        private readonly IStudentService _studentService;
        private readonly IEnrolRepository _enrolRepository;

        public EnrolService(IStudentService studentService, IEnrolRepository enrolRepository)
        {
            _studentService = studentService;
            _enrolRepository = enrolRepository;
        }
        public Enrollment AddStudentAndEnrolAsync(string name)
        {
            var student = FindOrCreateStudent(name);
            return EnrolAsync(student);
            
        }

        private Enrollment EnrolAsync(Student student)
        {
            var enrollment = new Enrollment();
            enrollment.IdEnrollment = Guid.NewGuid();
            enrollment.IdStudent = student.IdStudent;
            enrollment.Active = true;
            enrollment.LastUpdate = DateTime.Now;
            _enrolRepository.AddEnrollment(enrollment);
            return enrollment;
        }

        private Student FindOrCreateStudent(string name)
        {
            var student = _studentService.GetStudentByName(name);
            if (student == null)
            {
                student = _studentService.AddStudent(name);
            }
            return student;
        }

       

        public Enrollment FindEnrollmentStudentByName(string name)
        {
            var student = _studentService.GetStudentByName(name);
            if (student != null)
            {
                var enrollments = _enrolRepository.FindEnrollmentByStudentId(student.IdStudent);
                if (enrollments != null)
                {
                    return enrollments;
                }

            }
            return null;
        }

        public List<EnrolledStudent> GetAllEnrollments()
        {
            var enrolledStudents = new List<EnrolledStudent>();
            foreach(var enrollment in _enrolRepository.GetAllEnrollments())
            {
                var enrolledStudent = new EnrolledStudent();
                enrolledStudent.IdEnrollment = enrollment.IdEnrollment;
                enrolledStudent.LastUpdate = enrollment.LastUpdate;
                enrolledStudent.StudentName = _enrolRepository.GetStudentNameById(enrollment.IdStudent);

                enrolledStudents.Add(enrolledStudent);

            }
            return enrolledStudents;
        }

        public Enrollment UpdateEnrollment(Enrollment enrollmentChange)
        {
            var enrollment = GetEnrollmentsById(enrollmentChange.IdEnrollment);
            if (enrollment != null)
            {
                enrollmentChange.LastUpdate = DateTime.Now;
                _enrolRepository.Update(enrollmentChange);
                return enrollment;
            }
            else
                return null;

            
        }

        private Enrollment GetEnrollmentsById(Guid enrollmentId)
        {
            return _enrolRepository.Find(enrollmentId);
        }

        

        public void DeleteEnrollmentById(Guid enrollmentId)
        {
            var enrollment = GetEnrollmentsById(enrollmentId);
            if (enrollment != null)
            {
                _enrolRepository.Delete(enrollment);
            }
            
        }

        public void DeleteAllEnrollment()
        {
            _enrolRepository.DeleteAll();

        }


    }
}
