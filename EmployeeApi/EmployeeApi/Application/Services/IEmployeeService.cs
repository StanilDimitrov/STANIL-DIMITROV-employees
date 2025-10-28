using EmployeeApi.Application.DTOs;
using EmployeeApi.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeApi.Application.Services
{
    public interface IEmployeeService
    {
        List<EmployeeProject> GetEmployeesFromFile(IFormFile file);

        List<EmployeePairResult> GetPairsPerProject(List<EmployeeProject> projects);
    }
}
