﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task InsertOrEditDepartment(string name, string leader, string phoneNumber);
    }
}
