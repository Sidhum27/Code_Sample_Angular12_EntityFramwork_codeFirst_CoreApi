using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmpController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public EmpController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Employee>> Get()
        
        {
            return Ok(_context.Employee.ToList().OrderByDescending(x=> x.Date));
        }
        [Route("{id}")]
        [HttpGet]
        public ActionResult<Employee> Get(int id)
        {
            var employee = _context.Employee.FirstOrDefault(a => a.id == id);
            return Ok(employee);
        }
        [HttpPost]
        public ActionResult<Employee> post(Employee employee)
        {
            _context.Employee.Add(employee);
            _context.SaveChanges();
            return Ok(employee);
        }

        [HttpPut]
        public ActionResult<Employee> put(Employee employee)
        {
            var employeeIndb = _context.Employee.FirstOrDefault(a => a.id == employee.id);
            employeeIndb.Date = employee.Date;
            employeeIndb.Open = employee.Open;
            
            employeeIndb.High = employee.High;
            employeeIndb.Low = employee.Low;
            employeeIndb.Close = employee.Close;
            _context.SaveChanges();
            return Ok(employee);
        }

        [Route("{id}")]
        [HttpDelete]
        public ActionResult<Employee> delete(int id)
        {
            var employeeIndb = _context.Employee.FirstOrDefault(a => a.id == id);
            _context.Remove(employeeIndb);
            _context.SaveChanges();
            return Ok(employeeIndb);
        }
    }
}
