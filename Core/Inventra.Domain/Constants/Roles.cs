using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Domain.Constants
{
    public static class Roles
    {
        public const string Admin = "Admin";

        public const string Manager = "Manager";

        public const string Employee = "Employee";
        public static readonly HashSet<string> All =
    [
        Admin,
        Manager,
        Employee
    ];
    }
}
