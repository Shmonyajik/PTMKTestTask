using Microsoft.EntityFrameworkCore;
using PTMKTestTask.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMKTestTask.Repository
{
    public class EmployeeRepository : IBaseRepository<Employee>
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context)
        {
                _context = context;
        }
        public void Create(Employee model)
        {
            _context.Employees.Add(model);
            _context.SaveChanges();
        }

        public void CreateMultiple(IEnumerable<Employee> model)
        {
            _context.AddRange(model);
             _context.SaveChanges();
        }

        public IQueryable<Employee> GetAll()
        {
            return _context.Employees;
        }

    }
}
