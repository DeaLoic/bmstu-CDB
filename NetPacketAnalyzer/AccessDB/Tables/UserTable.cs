using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDB.Tables
{
    public static class UserTable
    {
        public readonly static string Name = "UserTable";
        public readonly static List<string> Columns = new List<string> {"Id", "Login", "Hash", "Permission" };
            }
}
