using EmployeeApi.Application.Csv;
using EmployeeApi.Application.DTOs;
using EmployeeApi.Domain.Entities;

namespace EmployeeApi.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeProjectCsvDeserializer _projectCsvDeserializer;

        public EmployeeService(IEmployeeProjectCsvDeserializer projectCsvDeserializer)
        {
            _projectCsvDeserializer = projectCsvDeserializer;
        }

        public List<EmployeeProject> GetEmployeesFromFile(IFormFile file)
        {
            var records = _projectCsvDeserializer.Deserialize(file);

            var employeeProjects = records.Select(r => new EmployeeProject
            {
                EmployeeId = r.EmployeeId,
                ProjectId = r.ProjectId,
                DateFrom = r.DateFrom,
                DateTo = r.DateTo
            }).ToList();

            return employeeProjects;
        }

        public List<EmployeePairResult> GetPairsPerProject(List<EmployeeProject> projects)
        {
            var results = new Dictionary<(int, int, int), int>();

            var projectGroups = projects.GroupBy(p => p.ProjectId);

            foreach (var project in projectGroups)
            {
                var employees = project.ToList();

                for (int i = 0; i < employees.Count; i++)
                {
                    for (int j = i + 1; j < employees.Count; j++)
                    {
                        if (employees[i].EmployeeId == employees[j].EmployeeId)
                        {
                            continue;
                        }

                        int days = CalculateOverlapDays(employees[i], employees[j]);

                        if (days <= 0)
                        {
                            continue;
                        }

                        var key = (
                            Math.Min(employees[i].EmployeeId, employees[j].EmployeeId),
                            Math.Max(employees[i].EmployeeId, employees[j].EmployeeId),
                            project.Key
                        );

                        if (results.ContainsKey(key))
                        {
                            results[key] += days;
                        }

                        else
                        {
                            results[key] = days;
                        }
                    }
                }
            }

            return results.Select(r => new EmployeePairResult
            {
                EmployeeId1 = r.Key.Item1,
                EmployeeId2 = r.Key.Item2,
                ProjectId = r.Key.Item3,
                TotalDaysWorked = r.Value
            })
            .OrderByDescending(r => r.TotalDaysWorked)
            .ToList();
        }

        private static int CalculateOverlapDays(EmployeeProject first, EmployeeProject second)
        {
            DateTime firstEnd = first.DateTo ?? DateTime.Today;
            DateTime secondEnd = second.DateTo ?? DateTime.Today;

            DateTime overlapStart = first.DateFrom > second.DateFrom
                ? first.DateFrom
                : second.DateFrom;

            DateTime overlapEnd = firstEnd < secondEnd
                ? firstEnd
                : secondEnd;

            if (overlapEnd < overlapStart)
            {
                return 0;
            }

            TimeSpan overlapDuration = overlapEnd - overlapStart;
            int totalDays = overlapDuration.Days + 1;

            return totalDays;
        }
    }
}
