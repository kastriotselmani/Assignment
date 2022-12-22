using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Leader { get; set; }
        public string Phone { get; set; }
    }
}
