using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBDD.Model
{
    public class DriverLicense
    {
        public int DriverLicenseId { get; set; }
        public DateOnly DateOfIssue { get; set; }
        public string IssuedBy { get; set; } = null!;
        public string Number { get; set; } = null!;
        public string VehicleCategories { get; set; } = null!;

        public int? UserId { get; set; }
        public User? User { get; set; }

        public int DriverId { get; set; }
        public Driver Driver { get; set; } = null!;


        public override string ToString() => $"{Number} - {VehicleCategories}";
	}
}
