using Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DroidRacesWPF.BoardShapes
{
	public class FlagShape : TextBlock
	{
		Brush flagFillBrush = new SolidColorBrush(Colors.Blue);

		public FlagShape(Flag flag) 
		{
			Text = flag.number.ToString();
			Foreground = flagFillBrush;
			TextAlignment = TextAlignment.Center;
			HorizontalAlignment = HorizontalAlignment.Center;
			VerticalAlignment = VerticalAlignment.Center;
		}
	}
}
