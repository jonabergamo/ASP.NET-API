using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SegundaAPI.Model;

namespace SegundaAPI.Infra
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public void Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }



        public IList<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public void Delete(Employee employee)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();


        }


        public Employee? Get(string id)
        {
            return _context.Employees.SingleOrDefault(x => x.Id.Equals(id));
        }
    }
}
