using Assignment.Models;
using Assignment.Models.DTO;
using Assignment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeContext _context;
        public EmployeeService(EmployeeContext context)
        {
            _context = context;
        }
        public async Task<List<Employee>> GetEmployeesBySearch(EmployeeSearchModel model)
        {
            var employees = await _context.Employee.Where(_ =>
                _.Name.Contains(model.Name) &&
                _.Manager.ToLower().Contains(model.Manager.ToLower()) &&
                _.Username.ToLower().Contains(model.Username.ToLower()) &&
                _.Email.ToLower().Contains(model.Email.ToLower()) &&
                _.Department.ToLower().Contains(model.Department.ToLower()) &&
                _.PhoneNumber.ToLower().Contains(model.PhoneNumber.ToLower())
                ).ToListAsync();
            return employees;
        }

        public async Task<Employee> IsEmployeeInserted(string email)
        {
            var user = await _context.Employee.SingleOrDefaultAsync(_ => _.Email == email);
            if (user == null)
                return null;
            return user;
        }

        public async Task InsertEmployee(Employee emp)
        {
            await _context.Employee.AddAsync(new Employee
            {
                Name = emp.Name,
                Manager = emp.Manager,
                Username = emp.Username,
                Address = emp.Address,
                Email = emp.Email,
                PhoneNumber = emp.PhoneNumber,
                StartDate = emp.StartDate,
                EndDate = emp.EndDate,
                Department = emp.Department
            });
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployee(Employee emp)
        {
            var empFound = await _context.Employee.SingleOrDefaultAsync(_ => _.Email == emp.Email);

            //UpdateData
            empFound.Name = emp.Name;
            empFound.Manager = emp.Manager;
            empFound.Username = emp.Username;
            empFound.Address = emp.Address;
            empFound.Email = emp.Email;
            empFound.PhoneNumber = emp.PhoneNumber;
            empFound.StartDate = emp.StartDate;
            empFound.EndDate = emp.EndDate;
            empFound.Department = emp.Department;
            empFound.IsActive = emp.IsActive;

            _context.ChangeTracker.Clear();
            _context.Entry(empFound).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
