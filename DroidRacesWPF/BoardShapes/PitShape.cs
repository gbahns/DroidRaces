using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DroidRacesWPF.BoardShapes
{
	public class PitShape : UserControl
	{
		Brush flagFillBrush = new SolidColorBrush(Colors.Blue);

		public PitShape() 
		{
			var shape = new Rectangle();
			shape.Fill = new SolidColorBrush(Colors.Black);
			shape.Stretch = Stretch.Fill;
			shape.Margin = new Thickness(2);
			Content = shape;
		}
	}
}
