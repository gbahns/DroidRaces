using Microsoft.Expression.Shapes;
using Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DroidRacesWPF.BoardShapes
{
	public class DroidShape : UserControl
	{
		//Droid droid;
		//public Droid Droid2
		//{
		//	get { return droid; }
		//	set
		//	{
		//		droid = value;
		//		var shape = (RegularPolygon)Content;
		//		shape.Fill = new SolidColorBrush(GetDroidColor(Droid.color));
		//		shape.RenderTransform = new RotateTransform(DirectionToAngle(Droid.direction));
		//	}
		//}

		public static readonly DependencyProperty DroidProperty =
			DependencyProperty.Register("Droid", typeof(Droid), typeof(DroidShape));

		public Droid Droid
		{
			get
			{
				return (Droid)GetValue(DroidProperty);
			}
			set
			{
				SetValue(DroidProperty, value);
				var shape = (RegularPolygon)Content;
				shape.Fill = new SolidColorBrush(GetDroidColor(Droid.color));
				shape.RenderTransform = new RotateTransform(DirectionToAngle(Droid.direction));
				Droid.PropertyChanged += (s, e) =>
				{
					if (e.PropertyName == "direction")
					{
						shape.RenderTransform = new RotateTransform(DirectionToAngle(Droid.direction));
					}
				};
			}
		}

		//public static readonly DependencyProperty DirectionProperty =
		//	DependencyProperty.Register("Direction", typeof(Direction), typeof(Direction));

		//public Direction Direction
		//{
		//	get { return (Direction)GetValue(DirectionProperty); }
		//	set { SetValue(DirectionProperty, Direction); }
		//}

		//public static readonly DependencyProperty AngleProperty =
		//	DependencyProperty.Register("Angle", typeof(double), typeof(double));

		//public double Angle
		//{
		//	get { return (double)GetValue(AngleProperty); }
		//	set { SetValue(AngleProperty, value); }
		//}

		public DroidShape()
		{
			var shape = new RegularPolygon();
			shape.InnerRadius = 1;
			shape.PointCount = 3;
			shape.Stretch = Stretch.Fill;
			shape.Stroke = new SolidColorBrush(Colors.Black);
			shape.Margin = new Thickness(3);
			shape.RenderTransformOrigin = new Point(0.5, 0.5);
			Content = shape;
		}

		public DroidShape(Droid droid) : this()
		{
			Droid = droid;
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
