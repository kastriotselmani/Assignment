using Assignment.Models;
using Assignment.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployeesBySearch(EmployeeSearchModel model);
        Task<Employee> IsEmployeeInserted(string email);
        Task InsertEmployee(Employee emp);
        Task UpdateEmployee(Employee emp);
    }
}
