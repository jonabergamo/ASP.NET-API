namespace SegundaAPI.Model
{
    public interface IEmployeeRepository
    {
        void Add(Employee employee);

        IList<Employee> GetAll();

        Employee? Get(string id);

        void Delete(Employee employee);

    }
}
