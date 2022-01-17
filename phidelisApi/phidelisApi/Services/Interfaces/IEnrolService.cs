using phidelisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace phidelisApi.Services.Interfaces
{
    public interface IEnrolService
    {
        Enrollment AddStudentAndEnrolAsync(string name);
        void DeleteEnrollmentById(Guid enrollmentId);
        Enrollment UpdateEnrollment(Enrollment enrollmentId);
        List<EnrolledStudent> GetAllEnrollments();
        Enrollment FindEnrollmentStudentByName(string name);
        void DeleteAllEnrollment();

    }
}
