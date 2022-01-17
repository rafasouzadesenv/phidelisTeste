using phidelisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace phidelisApi.Repository
{
    public interface IEnrolRepository
    {
        void AddStudent(Student item);
        void AddEnrollment(Enrollment enrollment);
        Enrollment FindEnrollmentByStudentId(Guid studentId);
        List<Enrollment> GetAllEnrollments();
        Enrollment Find(Guid id);
        void Delete(Enrollment item);
        string GetStudentNameById(Guid idStudent);
        void Update(Enrollment enrollment);
        void DeleteAll();

    }
}
