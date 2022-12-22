using Assignment.Models;
using Assignment.Models.DTO;
using Assignment.Services;
using Assignment.Services.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class TestEmployeeService
    {
        private EmployeeContext context;

        List<Employee> employeeList = new List<Employee>()
            {
                new Employee
                {
                    Id = new Guid(),
                    Name = "John Doe",
                    Address = "Prishtine, L.Dardania",
                    Department = "Dev",
                    Email = "john.doe@gmail.com",
                    StartDate = DateTime.Parse("2015-03-02T00:00:00"),
                    EndDate = DateTime.Parse("2022-05-10T00:00:00"),
                    IsActive = false,
                    Manager = "liz.erd",
                    PhoneNumber = "(566) 576-7803",
                    Username = "john.doe"
                },
                new Employee()
                {
                    Id = new Guid(),
                    Name = "Sarah Jones",
                    Address = "Prishtine, L.Dardania",
                    Department = "Dev",
                    Email = "sarah.jones@gmail.com",
                    StartDate = DateTime.Parse("2020-02-02T00:00:00"),
                    EndDate = DateTime.Parse("2022-05-10T00:00:00"),
                    IsActive = false,
                    Manager = "john.doe",
                    PhoneNumber = "(566) 576-7805",
                    Username = "john.doe"
                }
            };

        public TestEmployeeService()
        {
            var dbOptions = new DbContextOptionsBuilder<EmployeeContext>()
                         .UseInMemoryDatabase(databaseName: "EmployeeDb")
                         .Options;

                            context = new EmployeeContext(dbOptions);
                            context.Employee.AddRange(employeeList);
                            context.SaveChanges();
        }

        [TestMethod]
        public async Task GetEmployees_ShouldReturnFilteredEmployees()
        {

            var employeeService = new EmployeeService(context);

            var expected = new List<Employee>
            {
                new Employee()
                {
                    Id = Guid.Parse("a1eced25-7a09-4f36-42d0-08dae3464411"),
                    Name = "John Doe",
                    Address = "Prishtine, L.Dardania",
                    Department = "Dev",
                    Email = "john.doe@gmail.com",
                    StartDate = DateTime.Parse("2015-03-02T00:00:00"),
                    EndDate = DateTime.Parse("2022-05-10T00:00:00"),
                    IsActive = false,
                    Manager = "liz.erd",
                    PhoneNumber = "(566) 576-7803",
                    Username = "john.doe"
                }
            };

            var result = await employeeService.GetEmployeesBySearch(
                new EmployeeSearchModel { Name = "", Manager = "liz", Username = "john", Email = "john.doe@gmail.com", Department = "", PhoneNumber = "" });

            expected[0].Email.Should().Be(result[0].Email);
        }

        [TestMethod]
        public async Task GetEmployees_IsEmployeeInserted()
        {

            var employeeService = new EmployeeService(context);

            var employee = new Employee 
            { 

                    Id = Guid.Parse("a1eced25-7a09-4f36-42d0-08dae3464411"),
                    Name = "John Doe",
                    Address = "Prishtine, L.Dardania",
                    Department = "Dev",
                    Email = "john.doe@gmail.com",
                    StartDate = DateTime.Parse("2015-03-02T00:00:00"),
                    EndDate = DateTime.Parse("2022-05-10T00:00:00"),
                    IsActive = false,
                    Manager = "liz.erd",
                    PhoneNumber = "(566) 576-7803",
                    Username = "john.doe"
            };

            var res = await employeeService.IsEmployeeInserted(employee.Email);
            res.Should().NotBeNull();
        }



    }
}
