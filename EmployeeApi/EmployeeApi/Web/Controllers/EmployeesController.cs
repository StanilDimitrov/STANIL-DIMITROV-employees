using EmployeeApi.Application.DTOs;
using EmployeeApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("upload")]
        public ActionResult<ICollection<EmployeePairResult>> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "Invalid file upload",
                    Detail = "The uploaded file cannot be null or empty.",
                    Status = StatusCodes.Status400BadRequest,
                    Instance = HttpContext.Request.Path
                };

                return BadRequest(problemDetails);
            }

            var employeeProjects = _employeeService.GetEmployeesFromFile(file);
            var topPairs = _employeeService.GetPairsPerProject(employeeProjects);

            return topPairs;
        }
    }
}
