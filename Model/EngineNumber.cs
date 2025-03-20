using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBDD.Model
{
    public class EngineNumber
    {
        public int EngineNumberId { get; set; }
        public int NumberEngine { get; set; }
        public string? Kind { get; set; } = null!;
        public double Capacity { get; set; }
        public double Power { get; set; }
        public double Torque { get; set; }

        public VINNumber VINNumber { get; set; } = null!;

		public override string ToString()
		{
            return $"{Kind}{Power}: {NumberEngine}";
		}
	}
}
