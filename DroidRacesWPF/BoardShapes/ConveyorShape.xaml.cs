using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DroidRacesWPF.BoardShapes
{
	public partial class ConveyorShape : UserControl
	{
		public ConveyorShape(Conveyor element)
		{
			InitializeComponent();
			this.RenderTransformOrigin = new Point(0.5, 0.5);
			this.RenderTransform = new RotateTransform(DirectionToAngle(element.direction));
			if (element.speed == 2)
				this.Arrow.Fill = new SolidColorBrush(Colors.LightBlue);
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
