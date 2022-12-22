using Assignment.Models;
using Assignment.Services.Interfaces;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Services
{
    public class ReadExcelFileService : IReadExcelFileService
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public ReadExcelFileService(EmployeeContext context, IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }
        public async Task ReadExcelAsync(IFormFile file)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    bool firstRowPassed = false;

                    while (reader.Read())
                    {
                        if (firstRowPassed == false)
                        {
                            if (reader.GetString(0).Equals("name"))
                                firstRowPassed = true;
                        }
                        else
                        {
                            //Employee Data
                            var name = Convert.ToString(reader.GetValue(0));
                            var manager = Convert.ToString(reader.GetValue(1));
                            var username = Convert.ToString(reader.GetValue(2));
                            var email = Convert.ToString(reader.GetValue(3));
                            var department = Convert.ToString(reader.GetValue(4));
                            var phoneNumber = Convert.ToString(reader.GetValue(5));
                            var address = Convert.ToString(reader.GetValue(6));
                            var startDate = Convert.ToString(reader.GetValue(7));
                            var endDate = Convert.ToString(reader.GetValue(8));

                            //Department Data
                            var departmentName = Convert.ToString(reader.GetValue(11));
                            var departmentLeader = Convert.ToString(reader.GetValue(12));
                            var departmentPhone = Convert.ToString(reader.GetValue(13));

                            if (!string.IsNullOrEmpty(departmentName) && !string.IsNullOrEmpty(departmentLeader) && !string.IsNullOrEmpty(departmentPhone))
                                await _departmentService.InsertOrEditDepartment(departmentName, departmentLeader, departmentPhone);



                            //check if user exist
                            if (await _employeeService.IsEmployeeInserted(email) == null)
                            {
                                await _employeeService.InsertEmployee(new Employee
                                {
                                    Name = name,
                                    Email = email,
                                    Manager = manager,
                                    Username = username,
                                    Department = department,
                                    Address = address,
                                    StartDate = FormatDate(startDate),
                                    EndDate = FormatDate(endDate),
                                    PhoneNumber = phoneNumber,
                                    IsActive = FormatDate(endDate) > DateTime.Now ? true : false
                                });
                            }
                            else
                            {
                                await _employeeService.UpdateEmployee(new Employee
                                {
                                    Name = name,
                                    Email = email,
                                    Manager = manager,
                                    Username = username,
                                    Department = department,
                                    Address = address,
                                    StartDate = FormatDate(startDate),
                                    EndDate = FormatDate(endDate),
                                    PhoneNumber = phoneNumber,
                                    IsActive = FormatDate(endDate) > DateTime.Now ? true : false
                                });
                            }

                        }
                    }
                };
                stream.Dispose();
            }
        }


        private DateTime FormatDate(string date)
        {
            return DateTime.Parse($"{date.Substring(0, 4)}-{date.Substring(4, 2)}-{date.Substring(6, 2)}");
        }
    }
}
