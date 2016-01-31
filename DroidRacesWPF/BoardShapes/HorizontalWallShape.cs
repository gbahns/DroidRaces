using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DroidRacesWPF.BoardShapes
{
	public class HorizontalWallShape : UserControl
	{
		public HorizontalWallShape()
		{
			var line = new Rectangle();
			line.StrokeThickness = 5;
			line.Stroke = new SolidColorBrush(Colors.Black);
			line.Fill = new SolidColorBrush(Colors.Black);
			line.HorizontalAlignment = HorizontalAlignment.Stretch;
			line.VerticalAlignment = VerticalAlignment.Bottom;
			line.Margin = new Thickness(0, 0, 0, -3);
			Content = line;
		}
	}
}
