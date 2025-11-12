using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urlaubsplaner.Enum;

namespace Urlaubsplaner.Model
{
    internal class User
    {
        public int Id { get; set; }
        public string FirstName;
        public string LastName;
        public Role[] Roles;
    }
}
