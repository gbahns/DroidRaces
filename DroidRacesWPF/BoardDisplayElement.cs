using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DroidRacesWPF
{
	public class BoardDisplayElement
	{
		public IBoardObject boardElement { get; set; }
		public UIElement displayElement { get; set; }
	}
}
