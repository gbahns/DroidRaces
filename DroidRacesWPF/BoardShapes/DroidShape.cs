using Microsoft.Expression.Shapes;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DroidRacesWPF.BoardShapes
{
	public class DroidShape : UserControl
	{
		public DroidShape(Droid droid)
		{
			var shape = new RegularPolygon();
			shape.Fill = new SolidColorBrush(GetDroidColor(droid.color));
			shape.InnerRadius = 1;
			shape.PointCount = 3;

			shape.Stretch = Stretch.Fill;
			shape.Stroke = new SolidColorBrush(Colors.Black);
			shape.Margin = new Thickness(3);
			shape.RenderTransformOrigin = new Point(0.5, 0.5);
			shape.RenderTransform = new RotateTransform(DirectionToAngle(droid.direction));
			
			Content = shape;
		}

		Color GetDroidColor(DroidColor color)
		{
			switch (color)
			{
				case DroidColor.Black: return Colors.Black;
				case DroidColor.Blue: return Colors.Blue;
				case DroidColor.Green: return Colors.Green;
				case DroidColor.Orange: return Colors.Orange;
				case DroidColor.Pink: return Colors.Pink;
				case DroidColor.Purple: return Colors.Purple;
				case DroidColor.Red: return Colors.Red;
				case DroidColor.Yellow: return Colors.Yellow;
				default: return Colors.White;
			}
		}

		double DirectionToAngle(Direction direction)
		{
			switch (direction)
			{
				case Direction.Up: return 0;
				case Direction.Right: return 90;
				case Direction.Down: return 180;
				case Direction.Left: return 270;
				default: return 0;
			}
		}

	}
}
