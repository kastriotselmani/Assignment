using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Models.DTO
{
    public class EmployeeSearchModel
    {
        public string Name { get; set; }
        public string Manager { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
    }
}
