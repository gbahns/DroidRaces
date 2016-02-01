using DroidRacesWPF.BoardShapes;
using Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DroidRacesWPF
{
	public partial class MainWindow : Window
	{
		Brush gridLineBrush = new SolidColorBrush(Colors.DarkGray);
		Brush conveyorFillBrush = new SolidColorBrush(Colors.Orange);
		Brush fastConveyorFillBrush = new SolidColorBrush(Colors.Blue);
		Board board = new Board();
		List<BoardDisplayElement> boardDispalyElements = new List<BoardDisplayElement>();

		double widthByHeight = 0;

		//IContainer container;

		Dictionary<string, Func<IBoardObject, UIElement>> displayElementFactoryMap = new Dictionary<string, Func<IBoardObject, UIElement>>()
		{
			{"Wrench", e => new WrenchControl((Wrench)e) },
			{"Conveyor", e => new ConveyorShape((Conveyor)e) },
			{"Flag", e => new FlagShape((Flag)e) },
			{"Droid", e => new DroidShape((Droid)e) },
			{"ConveyorCorner", e => new ConveyorCornerShape((ConveyorCorner)e) },
			{"VerticalWall", e => new VerticalWallShape() },
			{"HorizontalWall", e => new HorizontalWallShape() },
		};

		public MainWindow()
		{
			//var container = new UnityContainer();
			//var builder = new ContainerBuilder();
			//builder.RegisterType<WrenchControl>().As<UIElement>();
			//builder.RegisterType<ConveyorShape>().As<UIElement>();
			//builder.RegisterAdapter<Conveyor, ConveyorShape>(conveyor => new ConveyorShape());
			//container = builder.Build();

			InitializeComponent();
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			HwndSource source = HwndSource.FromVisual(this) as HwndSource;
			if (source != null)
			{
				source.AddHook(new HwndSourceHook(WinProc));
			}
		}

		public const Int32 WM_EXITSIZEMOVE = 0x0232;
		private IntPtr WinProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, ref Boolean handled)
		{
			IntPtr result = IntPtr.Zero;
			switch (msg)
			{
				case WM_EXITSIZEMOVE:
					{
						this.Height = this.Width / widthByHeight;
						break;
					}
			}

			return result;
		}

		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);

			widthByHeight = Width / Height;

			for (int i = 0; i < board.Width; i++)
				GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
			for (int i = 0; i < board.Height; i++)
				GameGrid.RowDefinitions.Add(new RowDefinition());

			foreach (var boardElement in board.boardElements)
			{
				AddDisplayElement(boardElement);
			}
		}

		UIElement CreateDisplayElement(IBoardObject boardElement)
		{
			var typeName = boardElement.GetType().Name;
			if (displayElementFactoryMap.ContainsKey(typeName))
				return displayElementFactoryMap[typeName](boardElement);

			throw new ApplicationException(string.Format("No display element defined for {0}", boardElement.GetType().ToString()));
		}

		void AddDisplayElement(IBoardObject boardElement)
		{
			var displayElement = CreateDisplayElement(boardElement);
			var element = new BoardDisplayElement();
			element.boardElement = boardElement;
			element.displayElement = displayElement;
			boardDispalyElements.Add(element);
			Grid.SetColumn(displayElement, boardElement.position.X);
			Grid.SetRow(displayElement, boardElement.position.Y);
			GameGrid.Children.Add(displayElement);
		}

		//Line CreateGridLine()
		//{
		//	var line = new Line();
		//	line.StrokeThickness = 1;
		//	line.Stroke = gridLineBrush;
		//	//GameCanvas.Children.Add(line);
		//	return line;
		//}
		//
		//class TileDrawingInfo
		//{
		//	public double boardWidth;
		//	public double boardHeight;
		//	public int cols;
		//	public int rows;
		//	public double tileWidth;
		//	public double tileHeight;
		//	public double elementWidth;
		//	public double elementHeight;
		//	public double elementOffsetLeft;
		//	public double elementOffsetTop;

		//	public TileDrawingInfo(SizeChangedEventArgs sizeInfo, int rows, int cols)
		//	{
		//		boardWidth = sizeInfo.NewSize.Width;
		//		boardHeight = sizeInfo.NewSize.Height;
		//		this.cols = cols;
		//		this.rows = rows;
		//		tileWidth = boardWidth / cols;
		//		tileHeight = boardHeight / rows;
		//		elementWidth = tileWidth * 0.75;
		//		elementHeight = tileHeight * 0.75;
		//		elementOffsetLeft = tileWidth * 0.25 / 2;
		//		elementOffsetTop = tileHeight * 0.25 / 2;
		//	}
		//}

		//private void GameCanvas_SizeChanged(object sender, SizeChangedEventArgs sizeInfo)
		//{
		//	for (int i = 0; i < verticalGridLines.Length; i++)
		//	{
		//		var line = verticalGridLines[i];
		//		line.X1 = line.X2 = i * sizeInfo.NewSize.Width / (verticalGridLines.Length - 1);
		//		line.Y2 = sizeInfo.NewSize.Height;
		//	}

		//	for (int i = 0; i < horizontalGridLines.Length; i++)
		//	{
		//		var line = horizontalGridLines[i];
		//		line.Y1 = line.Y2 = i * sizeInfo.NewSize.Height / (horizontalGridLines.Length - 1);
		//		line.X2 = sizeInfo.NewSize.Width;
		//	}

		//	var drawInfo = new TileDrawingInfo(sizeInfo, verticalGridLines.Length - 1, horizontalGridLines.Length - 1);

		//	foreach (var element in boardDispalyElements)
		//	{
		//		var col = element.boardElement.position.X;
		//		var row = element.boardElement.position.Y;

		//		var verticalWall = element.boardElement as VerticalWall;
		//		if (verticalWall != null)
		//		{
		//			var line = (Line)element.displayElement;
		//			line.X1 = line.X2 = (verticalWall.position.X + 1) * drawInfo.tileWidth;
		//			line.Y1 = verticalWall.position.Y * drawInfo.tileHeight;
		//			line.Y2 = (verticalWall.position.Y + 1) * drawInfo.tileHeight;
		//			continue;
		//		}

		//		var horizontalWall = element.boardElement as HorizontalWall;
		//		if (horizontalWall != null)
		//		{
		//			var line = (Line)element.displayElement;
		//			line.Y1 = line.Y2 = (horizontalWall.position.Y + 1) * drawInfo.tileHeight;
		//			line.X1 = horizontalWall.position.X * drawInfo.tileWidth;
		//			line.X2 = (horizontalWall.position.X + 1) * drawInfo.tileWidth;
		//			continue;
		//		}

		//		Canvas.SetLeft(element.displayElement, col * drawInfo.tileWidth + drawInfo.elementOffsetLeft);
		//		Canvas.SetTop(element.displayElement, row * drawInfo.tileHeight + drawInfo.elementOffsetTop);

		//		//var droid = element.boardElement as Droid;
		//		//if (droid != null)
		//		//{
		//		//	//draw(droid, (Polygon)element.displayElement, drawInfo);
		//		//}

		//		//var conveyor = element.boardElement as Conveyor;
		//		//if (conveyor != null)
		//		//{
		//		//	//draw(conveyor, element.displayElement, drawInfo);
		//		//}

		//		var frameworkElement = element.displayElement as FrameworkElement;
		//		if (frameworkElement != null)
		//		{
		//			frameworkElement.Width = drawInfo.elementWidth;
		//			frameworkElement.Height = drawInfo.elementHeight;
		//		}

		//		var text = element.displayElement as TextBlock;
		//		if (text != null)
		//		{
		//			text.FontSize = Math.Min(drawInfo.elementWidth, drawInfo.elementHeight) / 1.2;
		//		}
		//	}
		//}

		//void draw(Conveyor conveyor, UIElement displayElement, TileDrawingInfo drawInfo)
		//{

		//}

		//void draw(Droid droid, Polygon polygon, TileDrawingInfo drawInfo)
		//{
		//	var elementHeight = drawInfo.elementHeight;
		//	var elementWidth = drawInfo.elementWidth;
		//	switch (droid.direction)
		//	{
		//		case Direction.Up:
		//			polygon.Points[0] = new Point(0, elementHeight);
		//			polygon.Points[1] = new Point(elementWidth, elementHeight);
		//			polygon.Points[2] = new Point(elementWidth / 2, 0);
		//			break;
		//		case Direction.Down:
		//			polygon.Points[0] = new Point(0, 0);
		//			polygon.Points[1] = new Point(elementWidth, 0);
		//			polygon.Points[2] = new Point(elementWidth / 2, elementHeight);
		//			break;
		//		case Direction.Left:
		//			polygon.Points[0] = new Point(elementWidth, 0);
		//			polygon.Points[1] = new Point(elementWidth, elementHeight);
		//			polygon.Points[2] = new Point(0, elementHeight / 2);
		//			break;
		//		case Direction.Right:
		//			polygon.Points[0] = new Point(0, 0);
		//			polygon.Points[1] = new Point(0, elementHeight);
		//			polygon.Points[2] = new Point(elementWidth, elementHeight / 2);
		//			break;
		//	}
		//}
	}
}
