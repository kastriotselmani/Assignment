using Assignment.Models;
using Assignment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly EmployeeContext _context;
        public DepartmentService(EmployeeContext context)
        {
            _context = context;
        }
        public async Task InsertOrEditDepartment(string name, string leader, string phoneNumber)
        {
            var department = await _context.Department.SingleOrDefaultAsync(_ => _.Name == name);
            if (department == null)
            {
                _context.Department.Add(new Department() { Leader = leader, Name = name, Phone = phoneNumber });
            }
            else
            {
                department.Leader = leader;
                department.Phone = phoneNumber;
                _context.ChangeTracker.Clear();
                _context.Entry(department).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }
    }
}
