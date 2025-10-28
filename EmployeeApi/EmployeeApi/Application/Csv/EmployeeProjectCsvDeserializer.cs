using CsvHelper;
using CsvHelper.Configuration;
using EmployeeApi.Application.Csv.Options;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace EmployeeApi.Application.Csv
{
    public class EmployeeProjectCsvDeserializer : IEmployeeProjectCsvDeserializer
    {
        private readonly IOptions<CsvConfig> _options;

        public EmployeeProjectCsvDeserializer(IOptions<CsvConfig> options)
        {
            _options = options;
        }

        public ICollection<EmployeeProjectCsvRecord> Deserialize(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                TrimOptions = TrimOptions.Trim,
                IgnoreBlankLines = true,  
                BadDataFound = null       
            };

            using var csv = new CsvReader(reader, config);

            var map = new EmployeeProjectCsvMap(_options);
            csv.Context.RegisterClassMap(map);

            var records = csv.GetRecords<EmployeeProjectCsvRecord>().ToList();

            return records;
        }
    }
}
