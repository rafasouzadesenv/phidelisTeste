using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using phidelisApi.Context;
using phidelisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace phidelisApi.Repository
{
    public class EnrolRepository : IEnrolRepository
    {
        private readonly PhidelisDbContext _context;


        //string _connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public IConfiguration Configuration { get; }


        public EnrolRepository(IConfiguration configuration)
        {
            _context = new PhidelisDbContext(configuration);
            Configuration = configuration;

        }
        public void AddStudent(Student item)
        {
            _context.Set<Student>().Add(item);
            Save();
        }
        public void Update(Enrollment enrollment)
        {
            var local = _context.Set<Enrollment>()
                .Local
                .FirstOrDefault(entry => entry.IdEnrollment.Equals(enrollment.IdEnrollment));

            // check if local is not null 
            if (local != null)
            {
                // detach
                _context.Entry(local).State = EntityState.Detached;
            }
            // set Modified flag in your entry
            
            _context.Entry(enrollment).State = EntityState.Modified;
            Save();
        }
        private void Save()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public void AddInContext(Object item)
        {
            _context.Set<Object>().Add(item);
        }
        public void Delete(Enrollment item)
        {

            _context.Set<Enrollment>().Remove(item);
            Save();


        }

        public void AddEnrollment(Enrollment enrollment)
        {
            _context.Set<Enrollment>().Add(enrollment);
            Save();
        }

        public Enrollment FindEnrollmentByStudentId(Guid studentId)
        {
            var item = _context.Enrols.Where(e => e.IdStudent == studentId).FirstOrDefault();
            return item;
        }

        public List<Enrollment> GetAllEnrollments()
        {
            return _context.Enrols.ToList();
        }

        public Enrollment Find(Guid id)
        {
            return _context.Set<Enrollment>().Find(id);
        }

        public void DeleteAll()
        {
            foreach (var item in _context.Enrols.ToList())
            {
                _context.Set<Enrollment>().Remove(item);
            }
            Save();
        }

        public string GetStudentNameById(Guid idStudent)
        {
            return _context.Students.Where(e => e.IdStudent == idStudent).FirstOrDefault().Name;
        }
    }
}
