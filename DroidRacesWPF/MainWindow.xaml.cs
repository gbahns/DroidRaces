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

namespace DroidRacesWPF
{
	public partial class MainWindow : Window
	{
		Line[] horizontalGridLines = new Line[11];
		Line[] verticalGridLines = new Line[11];
		Brush gridLineBrush = new SolidColorBrush(Colors.DarkGray);
		Brush flagFillBrush = new SolidColorBrush(Colors.Blue);
		Brush conveyorFillBrush = new SolidColorBrush(Colors.Orange);
		Brush fastConveyorFillBrush = new SolidColorBrush(Colors.Blue);
		Board board = new Board();
		List<BoardDisplayElement> boardDispalyElements = new List<BoardDisplayElement>();

		public MainWindow()
		{
			InitializeComponent();
		}

		Line CreateGridLine()
		{
			var line = new Line();
			line.StrokeThickness = 1;
			line.Stroke = gridLineBrush;
			//GameCanvas.Children.Add(line);
			return line;
		}

		UIElement CreateDisplayElement(Conveyor conveyor)
		{
			var shape = new ConveyorShape();
			return shape;
			//var ellipse = new Ellipse();
			//ellipse.Fill = conveyor.speed == 2 ? fastConveyorFillBrush : conveyorFillBrush;
			//return ellipse;
		}

		UIElement CreateDisplayElement(ConveyorCorner corner)
		{
			var square = new Rectangle();
			square.Fill = corner.speed == 2 ? fastConveyorFillBrush : conveyorFillBrush;
			return square;
		}

		UIElement CreateDisplayElement(Flag flag)
		{
			var text = new TextBlock();
			text.Text = flag.number.ToString();
			text.Foreground = flagFillBrush;
			text.TextAlignment = TextAlignment.Center;
			text.HorizontalAlignment = HorizontalAlignment.Center;
			text.VerticalAlignment = VerticalAlignment.Center;
			return text;
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

		UIElement CreateDisplayElement(Droid droid)
		{
			var polygon = new Polygon();
			polygon.Fill = new SolidColorBrush(GetDroidColor(droid.color));
			polygon.Points = new PointCollection();
			polygon.Points.Add(new Point(0, 0));
			polygon.Points.Add(new Point(0, 20));
			polygon.Points.Add(new Point(20, 20));
			return polygon;
		}

		UIElement CreateDisplayElement(VerticalWall wall)
		{
			var line = new Line();
			line.Stroke = new SolidColorBrush(Colors.Black);
			line.StrokeThickness = 5;
			return line;
		}

		UIElement CreateDisplayElement(HorizontalWall wall)
		{
			var line = new Line();
			line.Stroke = new SolidColorBrush(Colors.Black);
			line.StrokeThickness = 5;
			return line;
		}

		UIElement CreateDisplayElement(Wrench wall)
		{
			var shape = new WrenchControl();
			//shape.Arrow.Orientation = Microsoft.Expression.Media.ArrowOrientation.Down;
			//shape.RenderTransformOrigin = new Point(0.5, 0.5);
			//shape.RenderTransform = new RotateTransform(90);
			return shape;
		}

		UIElement CreateDisplayElement(IBoardObject boardElement)
		{
			var flag = boardElement as Flag;
			if (flag != null)
				return CreateDisplayElement(flag);

			var droid = boardElement as Droid;
			if (droid != null)
				return CreateDisplayElement(droid);

			var conveyor = boardElement as Conveyor;
			if (conveyor != null)
				return CreateDisplayElement(conveyor);

			var conveyorCorner = boardElement as ConveyorCorner;
			if (conveyorCorner != null)
				return CreateDisplayElement(conveyorCorner);

			var wall = boardElement as VerticalWall;
			if (wall != null)
				return CreateDisplayElement(wall);

			var horizontalWall = boardElement as HorizontalWall;
			if (horizontalWall != null)
				return CreateDisplayElement(horizontalWall);

			var wrench = boardElement as Wrench;
			if (wrench != null)
				return CreateDisplayElement(wrench);

			throw new ApplicationException(string.Format("No display element defined for {0}", boardElement.GetType().ToString()));
		}

		void AddDisplayElement(IBoardObject boardElement)
		{
			var displayElement = CreateDisplayElement(boardElement);
			var element = new BoardDisplayElement();
			element.boardElement = boardElement;
			element.displayElement = displayElement;
			boardDispalyElements.Add(element);
			//GameCanvas.Children.Add(displayElement);
			Grid.SetColumn(displayElement, boardElement.position.X);
			Grid.SetRow(displayElement, boardElement.position.Y);
			GameGrid.Children.Add(displayElement);
		}

		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);

			for (int i = 0; i <= 10; i++)
			{
				horizontalGridLines[i] = CreateGridLine();
				verticalGridLines[i] = CreateGridLine();

				GameGrid.RowDefinitions.Add(new RowDefinition());
				GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
			}

			foreach (var boardElement in board.boardElements)
			{
				AddDisplayElement(boardElement);
			}
		}

		private PointCollection GetDroidPoints(Direction direction)
		{
			var points = new PointCollection();
			switch (direction)
			{
				case Direction.Up:
					points.Add(new Point(0, 1));
					points.Add(new Point(1, 1));
					points.Add(new Point(0.5, 0));
					break;
				case Direction.Down:
					points.Add(new Point(0, 0));
					points.Add(new Point(1, 0));
					points.Add(new Point(0.5, 1));
					break;
				case Direction.Left:
					points.Add(new Point(1, 0));
					points.Add(new Point(1, 1));
					points.Add(new Point(0, 0.5));
					break;
				case Direction.Right:
					points.Add(new Point(0, 0));
					points.Add(new Point(0, 1));
					points.Add(new Point(1, 0.5));
					break;
			}
			return points;
		}

		class TileDrawingInfo
		{
			public double boardWidth;
			public double boardHeight;
			public int cols;
			public int rows;
			public double tileWidth;
			public double tileHeight;
			public double elementWidth;
			public double elementHeight;
			public double elementOffsetLeft;
			public double elementOffsetTop;

			public TileDrawingInfo(SizeChangedEventArgs sizeInfo, int rows, int cols)
			{
				boardWidth = sizeInfo.NewSize.Width;
				boardHeight = sizeInfo.NewSize.Height;
				this.cols = cols;
				this.rows = rows;
				tileWidth = boardWidth / cols;
				tileHeight = boardHeight / rows;
				elementWidth = tileWidth * 0.75;
				elementHeight = tileHeight * 0.75;
				elementOffsetLeft = tileWidth * 0.25 / 2;
				elementOffsetTop = tileHeight * 0.25 / 2;
			}
		}

		private void GameCanvas_SizeChanged(object sender, SizeChangedEventArgs sizeInfo)
		{
			for (int i = 0; i < verticalGridLines.Length; i++)
			{
				var line = verticalGridLines[i];
				line.X1 = line.X2 = i * sizeInfo.NewSize.Width / (verticalGridLines.Length - 1);
				line.Y2 = sizeInfo.NewSize.Height;
			}

			for (int i = 0; i < horizontalGridLines.Length; i++)
			{
				var line = horizontalGridLines[i];
				line.Y1 = line.Y2 = i * sizeInfo.NewSize.Height / (horizontalGridLines.Length - 1);
				line.X2 = sizeInfo.NewSize.Width;
			}

			var drawInfo = new TileDrawingInfo(sizeInfo, verticalGridLines.Length - 1, horizontalGridLines.Length - 1);

			foreach (var element in boardDispalyElements)
			{
				var col = element.boardElement.position.X;
				var row = element.boardElement.position.Y;

				var verticalWall = element.boardElement as VerticalWall;
				if (verticalWall != null)
				{
					var line = (Line)element.displayElement;
					line.X1 = line.X2 = (verticalWall.position.X + 1) * drawInfo.tileWidth;
					line.Y1 = verticalWall.position.Y * drawInfo.tileHeight;
					line.Y2 = (verticalWall.position.Y + 1) * drawInfo.tileHeight;
					continue;
				}

				var horizontalWall = element.boardElement as HorizontalWall;
				if (horizontalWall != null)
				{
					var line = (Line)element.displayElement;
					line.Y1 = line.Y2 = (horizontalWall.position.Y + 1) * drawInfo.tileHeight;
					line.X1 = horizontalWall.position.X * drawInfo.tileWidth;
					line.X2 = (horizontalWall.position.X + 1) * drawInfo.tileWidth;
					continue;
				}

				Canvas.SetLeft(element.displayElement, col * drawInfo.tileWidth + drawInfo.elementOffsetLeft);
				Canvas.SetTop(element.displayElement, row * drawInfo.tileHeight + drawInfo.elementOffsetTop);

				var droid = element.boardElement as Droid;
				if (droid != null)
				{
					draw(droid, (Polygon)element.displayElement, drawInfo);
				}

				var conveyor = element.boardElement as Conveyor;
				if (conveyor != null)
				{
					draw(conveyor, element.displayElement, drawInfo);
				}

				var frameworkElement = element.displayElement as FrameworkElement;
				if (frameworkElement != null)
				{
					frameworkElement.Width = drawInfo.elementWidth;
					frameworkElement.Height = drawInfo.elementHeight;
				}

				var text = element.displayElement as TextBlock;
				if (text != null)
				{
					text.FontSize = Math.Min(drawInfo.elementWidth, drawInfo.elementHeight) / 1.2;
				}
			}
		}

		void draw(Conveyor conveyor, UIElement displayElement, TileDrawingInfo drawInfo)
		{

		}

		void draw(Droid droid, Polygon polygon, TileDrawingInfo drawInfo)
		{
			var elementHeight = drawInfo.elementHeight;
			var elementWidth = drawInfo.elementWidth;
			switch (droid.direction)
			{
				case Direction.Up:
					polygon.Points[0] = new Point(0, elementHeight);
					polygon.Points[1] = new Point(elementWidth, elementHeight);
					polygon.Points[2] = new Point(elementWidth / 2, 0);
					break;
				case Direction.Down:
					polygon.Points[0] = new Point(0, 0);
					polygon.Points[1] = new Point(elementWidth, 0);
					polygon.Points[2] = new Point(elementWidth / 2, elementHeight);
					break;
				case Direction.Left:
					polygon.Points[0] = new Point(elementWidth, 0);
					polygon.Points[1] = new Point(elementWidth, elementHeight);
					polygon.Points[2] = new Point(0, elementHeight / 2);
					break;
				case Direction.Right:
					polygon.Points[0] = new Point(0, 0);
					polygon.Points[1] = new Point(0, elementHeight);
					polygon.Points[2] = new Point(elementWidth, elementHeight / 2);
					break;
			}
		}
	}
}
