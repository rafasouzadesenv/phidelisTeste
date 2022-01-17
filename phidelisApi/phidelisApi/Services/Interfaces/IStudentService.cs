using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using phidelisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace phidelisApi.Services.Interfaces
{
    public interface IStudentService
    {
        Student AddStudent(string newStudent);
        Student GetStudentByName(string name);
    }
}
