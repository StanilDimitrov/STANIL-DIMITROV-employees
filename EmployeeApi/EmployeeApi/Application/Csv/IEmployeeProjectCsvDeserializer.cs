namespace EmployeeApi.Application.Csv
{
    public interface IEmployeeProjectCsvDeserializer
    {
        ICollection<EmployeeProjectCsvRecord> Deserialize(IFormFile file);
    }
}
