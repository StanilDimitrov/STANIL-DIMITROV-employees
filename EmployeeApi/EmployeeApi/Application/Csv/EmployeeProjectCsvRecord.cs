using System;

namespace EmployeeApi.Application.Csv
{
    public class EmployeeProjectCsvRecord
    {
        public int EmployeeId { get; set; }

        public int ProjectId { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
