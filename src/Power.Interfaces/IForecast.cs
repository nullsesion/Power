using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Power.Interfaces
{
	public interface IForecast
	{
		public ICurrent[] Days { get; set; }
	}
}
