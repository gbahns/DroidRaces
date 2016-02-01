using System.Collections.Generic;

namespace Model
{
	public class Board
	{
		public int Width = 15;
		public int Height = 15;
		public List<IBoardObject> boardElements = new List<IBoardObject>();

		public Board()
		{
			AddFlag(1, 2, 2);
			AddFlag(2, 13, 3);
			AddFlag(3, 4, 8);

			AddDroid(4, 14, Direction.Left, DroidColor.Red);
			AddDroid(6, 14, Direction.Up, DroidColor.Green);
			AddDroid(8, 14, Direction.Up, DroidColor.Pink);
			AddDroid(10, 14, Direction.Right, DroidColor.Purple);

			AddConveyorOval(2, 2, 12, 12, true, 1);
			AddConveyorOval(4, 4, 10, 10, false, 2);

			AddVerticalWall(0, 1);
			AddVerticalWall(1, 0);
			AddHorizontalWall(1, 1);

			AddWrench(0, 3);
		}

		void AddConveyorOval(int x1, int y1, int x2, int y2, bool clockwise, int speed)
		{
			var dir = new Direction[4];
			if (clockwise)
			{
				dir[0] = Direction.Right;
				dir[1] = Direction.Down;
				dir[2] = Direction.Left;
				dir[3] = Direction.Up;
			}
			else
			{
				dir[0] = Direction.Left;
				dir[1] = Direction.Down;
				dir[2] = Direction.Right;
				dir[3] = Direction.Up;
			}

			for (int x = x1 + 1; x < x2; x++)
			{
				AddConveyor(x, y1, dir[0], speed);
				AddConveyor(x, y2, dir[2], speed);
			}

			for (int y = y1 + 1; y < y2; y++)
			{
				AddConveyor(x1, y, dir[clockwise ? 3 : 1], speed);
				AddConveyor(x2, y, dir[clockwise ? 1 : 3], speed);
			}

			AddConveyorCorner(x1, y1, dir[3], dir[0], speed);
			AddConveyorCorner(x2, y1, dir[0], dir[1], speed);
			AddConveyorCorner(x2, y2, dir[1], dir[2], speed);
			AddConveyorCorner(x1, y2, dir[2], dir[3], speed);
		}

		void AddFlag(int i, int x, int y)
		{
			var flag = new Flag(i);
			flag.position = new Position(x, y); ;
			boardElements.Add(flag);
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
			conveyor.direction = direction;
			conveyor.speed = speed;
			boardElements.Add(conveyor);
		}

		void AddDroid(int x, int y, Direction direction, DroidColor color)
		{
			var droid = new Droid();
			droid.position = new Position(x, y);
			droid.direction = direction;
			droid.color = color;
			boardElements.Add(droid);
		}
	}
}
