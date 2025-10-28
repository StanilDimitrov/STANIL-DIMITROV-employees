using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;
using System.Globalization;
using Microsoft.Extensions.Options;
using EmployeeApi.Application.Csv.Options;

namespace EmployeeApi.Application.Csv
{
    public class MultiFormatDateTimeConverter : DefaultTypeConverter
    {
        private readonly string[] _formats;

        public MultiFormatDateTimeConverter(IOptions<CsvConfig> options)
        {
            _formats = options.Value.SupportedDateFormats;
        }

        public override object? ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text) || text.Equals("NULL", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            foreach (var format in _formats)
            {
                if (DateTime.TryParseExact(text, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    return date;
                }
            }

            throw new TypeConverterException(this, memberMapData, text, row.Context,
                $"Cannot parse '{text}' as DateTime. Supported formats: {string.Join(", ", _formats)}");
        }
    }
}
