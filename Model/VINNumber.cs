using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GIBDD.Model
{
    public class VINNumber
    {
        public int VINNumberID { get; set; }
        public string Number { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string YearOfIssue { get; set; } = null!;
        public string CarType { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string? RegistrationNumber { get; set; } = null!;
        public string? Photography { get; set; }

        [NotMapped]
        public string ImageUrl
        {
            get => $"{App.PhotographyDirectory}\\{Photography}";
        }

        public VINNumber()
        {
            Number = "W8AN2NDZJK0000014";
            Color = "Цвет";
            Brand = "Бренд";
            Model = "Модель";
            YearOfIssue = "2025";
            CarType = "Седан";
            Category = "Легковая";
            RegistrationNumber = "А 123 АА 123";
        }

        public int EngineNumberId { get; set; }
        public EngineNumber EngineNumber { get; set; } = null!;

        public int DriverId { get; set; }
        public Driver Driver { get; set; } = null!;

    }
}
