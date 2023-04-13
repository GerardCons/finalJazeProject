using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTHardware_EmployeeManagement
{
    internal class DBConnection
    {
        public string MyConnection()
        {
            string con = @"Data Source=DESKTOP-1CBE6JQ;Initial Catalog=hardwareHRSystem;Integrated Security=True";
            return con;
        }
    }
}
