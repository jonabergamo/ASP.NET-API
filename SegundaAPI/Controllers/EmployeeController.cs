using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SegundaAPI.Model;
using SegundaAPI.ViewModel;

namespace SegundaAPI.Controllers
{
    [ApiController]
    [Route("api/v1/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException();
        }

        [HttpPost]
        public IActionResult Add([FromForm] EmployeeViewModel employeeView)
        {
            // Gerar um nome de arquivo único
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(employeeView.photo.FileName);
            string filePath = Path.Combine("Storage", uniqueFileName);

            // Salvar o arquivo no sistema de arquivos
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                employeeView.photo.CopyTo(fileStream);
            }

            // Criar a entidade Employee
            Employee employee = new Employee(employeeView.Name, employeeView.Age, filePath);

            // Adicionar ao repositório
            _employeeRepository.Add(employee);

            // Retornar a resposta Created
            return CreatedAtAction(nameof(Add), new { id = employee.Id }, employee);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = _employeeRepository.GetAll();

            return Ok(employees);
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            Employee? employee = _employeeRepository.Get(id);

            if (employee == null) return NotFound();

            _employeeRepository.Delete(employee);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var employee = _employeeRepository.Get(id);
            if (employee == null)
            {
                return NotFound(); // Retorna 404 se o funcionário não for encontrado
            }
            return Ok(employee); // Retorna o funcionário com status 200
        }


        [HttpPost]
        [Route("{id}/download")]
        public IActionResult DownloadPhoto(string id)
        {
            Employee? employee = _employeeRepository.Get(id);
            if (employee == null)
            {
                return NotFound(); // Retorna 404 se o funcionário não for encontrado
            }

            if (employee.Photo == null)
            {
                return NotFound("Usuário não possui foto de perfil.");
            }

            var dataBytes = System.IO.File.ReadAllBytes(employee.Photo);

            return File(dataBytes, "image/png");

        }
    }
}
