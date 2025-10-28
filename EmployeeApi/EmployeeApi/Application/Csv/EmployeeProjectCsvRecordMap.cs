using CsvHelper.Configuration;
using EmployeeApi.Application.Csv.Options;
using Microsoft.Extensions.Options;

namespace EmployeeApi.Application.Csv
{
    public sealed class EmployeeProjectCsvMap : ClassMap<EmployeeProjectCsvRecord>
    {
        public EmployeeProjectCsvMap(IOptions<CsvConfig> options)
        {
            var dateConverter = new MultiFormatDateTimeConverter(options);

            Map(m => m.EmployeeId).Name("EmpID");

            Map(m => m.ProjectId).Name("ProjectID");

            Map(m => m.DateFrom).Name("DateFrom")
                .TypeConverter(dateConverter);

            Map(m => m.DateTo).Name("DateTo")
                .TypeConverterOption.NullValues("NULL")
                .TypeConverter(dateConverter);
        }
    }
}