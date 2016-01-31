using System.Collections.Generic;

namespace Model
{
	public class Board
	{
		//Tile[,] tiles;
		Dictionary<Position, IBoardObject> tiles = new Dictionary<Position, IBoardObject>();
		public List<IBoardObject> boardElements = new List<IBoardObject>();

		public Board()
		{
			for (int i = 0; i < 10; i++)
			{
				var flag = new Flag(i);
				flag.position = new Position(i, i); ;
				tiles[flag.position] = new Flag(i);
				boardElements.Add(flag);
			}

			AddDroid(new Position(1, 0), Direction.Up, DroidColor.Red);
			AddDroid(new Position(0, 1), Direction.Right, DroidColor.Green);
			AddDroid(new Position(0, 2), Direction.Down, DroidColor.Pink);
			AddDroid(new Position(3, 4), Direction.Left, DroidColor.Purple);

			AddConveyor(7, 0, Direction.Right, 1);
			AddConveyorCorner(8, 0, Direction.Right, Direction.Down, 1);
			AddConveyor(8, 1, Direction.Down, 1);
			AddConveyorCorner(8, 2, Direction.Down, Direction.Left, 1);
			AddConveyor(7, 2, Direction.Left, 1);
			AddConveyorCorner(6, 2, Direction.Left, Direction.Up, 1);
			AddConveyor(6, 1, Direction.Up, 2);
			AddConveyorCorner(6, 0, Direction.Up, Direction.Right, 1);

			AddVerticalWall(0, 1);
			AddVerticalWall(1, 0);
			AddHorizontalWall(1, 1);

			AddWrench(0, 3);
		}

		void AddWrench(int x, int y)
		{
			var item = new Wrench();
			item.position = new Position(x, y);
			boardElements.Add(item);
		}

		void AddHorizontalWall(int x, int y)
		{
			var wall = new HorizontalWall();
			wall.position = new Position(x, y);
			boardElements.Add(wall);
		}

		void AddVerticalWall(int x, int y)
		{
			var wall = new VerticalWall();
			wall.position = new Position(x, y);
			boardElements.Add(wall);
		}

		void AddConveyorCorner(int x, int y, Direction directionIn, Direction directionOut, int speed)
		{
			var conveyor = new ConveyorCorner();
			conveyor.position = new Position(x, y);
			conveyor.direcdtionIn = directionIn;
			conveyor.direcdtionOut = directionOut;
			conveyor.speed = speed;
			boardElements.Add(conveyor);
		}

		void AddConveyor(int x, int y, Direction direction, int speed)
		{
			var conveyor = new Conveyor();
			conveyor.position = new Position(x, y);
			conveyor.direcdtion = direction;
			conveyor.speed = speed;
			boardElements.Add(conveyor);
		}

		void AddDroid(Position position, Direction direction, DroidColor color)
		{
			var droid = new Droid();
			droid.position = position;
			droid.direction = direction;
			droid.color = color;
			boardElements.Add(droid);
		}
	}
}
