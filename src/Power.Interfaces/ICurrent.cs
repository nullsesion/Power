using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Power.Interfaces
{
    public interface ICurrent
    {
		public string Region { get; set; }
		public string Date { get; set; }
		public decimal TempC { get; set; }
		public decimal WindKph { get; set; }
		public int Humidity { get; set; }
		public string ConditionText { get; set; }
		public string Icon { get; set; }
	}
}
