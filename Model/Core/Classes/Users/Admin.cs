using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Model.Core.Classes
{
    public class Admin : User
    {
        public Admin(int adminID, string fname, string lname, string email, string username, string password)
            : base(adminID, fname, lname, email, username, password)
        {
        }
    }
}