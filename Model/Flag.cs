using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public class Flag : IBoardObject
	{
		public Position position { get; set; }
		public int number { get; set; }

		public Flag(int number)
		{
			this.number = number;
		}
	}
}
