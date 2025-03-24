using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBDD.Model
{
    public class Driver
    {
        public int DriverId { get; set; }
        public string FirstName { get; set; } = null!;
        public string SurName { get; set; } = null!;
        public string? Patronymic { get; set; }
        public DateTime BirthDay { get; set; }
        public string SeriesPassport { get; set; } = null!;
        public string NumberPassport { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string Home { get; set; } = null!;

        public Driver()
        {
            FirstName = "Иван";
            SurName = "Иванов";
            Patronymic = "Иванович";
            BirthDay = DateTime.Parse("01.01.1980"); 
            SeriesPassport = "1234";
            NumberPassport = "123456";
            City = "Москва";
            Street = "Центральная";
            Home = "12-3";
        }

        public int DriverLicenseId { get; set; }
        public DriverLicense? DriverLicense { get; set; }

        public ObservableCollection<VINNumber> VINNumbers { get; set; }

		public override string ToString() => $"[{DriverId}] " + SurName + " " + FirstName;
	}
}
