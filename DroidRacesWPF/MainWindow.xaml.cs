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
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Line[] horizontalGridLines = new Line[11];
		Line[] verticalGridLines = new Line[11];
		Brush gridLineBrush = new SolidColorBrush(Colors.DarkGray);
		Brush flagFillBrush = new SolidColorBrush(Colors.Blue);
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
			GameCanvas.Children.Add(line);
			return line;
		}

		void CreateDisplayElement(Flag flag)
		{
			//var ellipse = new Ellipse();
			//ellipse.Fill = flagFillBrush;
			//Canvas.SetLeft(ellipse, 10 * flag.position.X);
			//Canvas.SetTop(ellipse, 10 * flag.position.Y);
			//GameCanvas.Children.Add(ellipse);

			var text = new TextBlock();
			text.Text = flag.number.ToString();
			text.Foreground = flagFillBrush;
			text.TextAlignment = TextAlignment.Center;
			text.HorizontalAlignment = HorizontalAlignment.Center;
			text.VerticalAlignment = VerticalAlignment.Center;
			GameCanvas.Children.Add(text);

			var element = new BoardDisplayElement();
			element.boardElement = flag;
			//element.displayElement = ellipse;
			element.displayElement = text;
			boardDispalyElements.Add(element);
		}

		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);

			for (int i = 0; i <= 10; i++)
			{
				horizontalGridLines[i] = CreateGridLine();
				verticalGridLines[i] = CreateGridLine();
			}

			foreach (var boardElement in board.boardElements)
			{
				var flag = boardElement as Flag;
				if (flag != null)
				{
					CreateDisplayElement(flag);
				}
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

			var boardWidth = sizeInfo.NewSize.Width;
			var boardHeight = sizeInfo.NewSize.Height;
			var cols = verticalGridLines.Length - 1;
			var rows = horizontalGridLines.Length - 1;
			var tileWidth = boardWidth / cols;
			var tileHeight = boardHeight / rows;
			var elementWidth = tileWidth * 0.75;
			var elementHeight = tileHeight * 0.75;
			var elementOffsetLeft = tileWidth * 0.25 / 2;
			var elementOffsetTop = tileHeight * 0.25 / 2;
			//elementOffsetLeft = 0;
			//elementOffsetTop = 0;

			foreach (var element in boardDispalyElements)
			{
				var col = element.boardElement.position.X;
				var row = element.boardElement.position.Y;

				Canvas.SetLeft(element.displayElement, col * tileWidth + elementOffsetLeft);
				Canvas.SetTop(element.displayElement, row * tileHeight + elementOffsetTop);

				var frameworkElement = element.displayElement as FrameworkElement;
				if (frameworkElement != null)
				{
					frameworkElement.Width = elementWidth;
					frameworkElement.Height = elementHeight;
				}

				var text = element.displayElement as TextBlock;
				if (text != null)
				{
					text.FontSize = Math.Min(elementWidth, elementHeight) / 1.2;
				}
			}
		}
	}
}
