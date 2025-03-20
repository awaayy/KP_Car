using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBDD.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;

        public int DriverLicenseId { get; set; }
        public DriverLicense DriverLicense { get; set; } = null!;
    }
}
