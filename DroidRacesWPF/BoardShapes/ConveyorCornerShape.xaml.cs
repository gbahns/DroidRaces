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
	public partial class ConveyorCornerShape : UserControl
	{
		SolidColorBrush FastBrush = new SolidColorBrush(Colors.LightBlue);

		public ConveyorCornerShape(ConveyorCorner element)
		{
			InitializeComponent();

			if (element.speed == 2)
				this.Arrow.Fill = FastBrush;

			this.RenderTransformOrigin = new Point(0.5, 0.5);

			var transform = new TransformGroup();
			transform.Children.Add(new RotateTransform(DirectionToAngle(element)));
			if ((element.direcdtionIn == Direction.Right && element.direcdtionOut == Direction.Up) ||
				(element.direcdtionIn == Direction.Left && element.direcdtionOut == Direction.Down))
			{
				transform.Children.Add(new ScaleTransform(-1, -1));
				var arrowRotate = Arrow.RenderTransform;
				var groupTransform = new TransformGroup();
				groupTransform.Children.Add(arrowRotate);
				groupTransform.Children.Add(new ScaleTransform(-1, -1));
				Arrow.RenderTransform = groupTransform;
				this.Arrow.Fill = new SolidColorBrush(Colors.LightGreen);
			}
			if (element.direcdtionOut != Direction.Up && element.direcdtionIn > element.direcdtionOut)
			{
				//transform.Children.Add(new ScaleTransform(-1, -1));
				this.Arrow.Fill = new SolidColorBrush(Colors.Purple);
			}

			

			this.RenderTransform = transform;
		}

		double DirectionToAngle(ConveyorCorner element)
		{
			switch (element.direcdtionIn)
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
