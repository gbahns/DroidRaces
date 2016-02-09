using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class Board
	{
		public int Width = 15;
		public int Height = 15;
		public List<IBoardObject> boardElements = new List<IBoardObject>();
		public List<Droid> Droids { get; private set; }

		public Board()
		{
			Droids = new List<Droid>();

			AddFlag(1, 2, 2);
			AddFlag(2, 13, 3);
			AddFlag(3, 4, 8);

			AddDroid(4, 14, Direction.Left, DroidColor.Red);
			AddDroid(6, 14, Direction.Up, DroidColor.Green);
			AddDroid(8, 14, Direction.Up, DroidColor.Pink);
			AddDroid(10, 14, Direction.Right, DroidColor.Purple);

			var droid = Droids[0];
			droid.Instructions.Add(Instruction.Move1);
			droid.Instructions.Add(Instruction.TurnRight);
			droid.Instructions.Add(Instruction.Move1);
			droid.Instructions.Add(Instruction.Move1);
			droid.Instructions.Add(Instruction.Move1);
			droid.Instructions.Add(Instruction.UTurn);
			droid.Instructions.Add(Instruction.Move1);
			droid.Instructions.Add(Instruction.Move1);
			droid.Instructions.Add(Instruction.TurnLeft);
			droid.Instructions.Add(Instruction.Move3);
			droid.Instructions.Add(Instruction.Backup);

			//AddConveyorOval(2, 2, 12, 12, true, 1);
			//AddConveyorOval(4, 4, 10, 10, false, 2);
			AddClockwiseConveyorOval(2, 2, 12, 12, 1);
			AddCounterclockwiseConveyorOval(4, 4, 10, 10, 2);

			AddVerticalWall(0, 1);
			AddVerticalWall(1, 0);
			AddHorizontalWall(1, 1);

			AddWrench(0, 3);
			AddHammer(0, 4);
			AddWrench(0, 5);
			AddHammer(0, 5);

			AddPit(5, 5);
		}

		void AddClockwiseConveyorOval(int x1, int y1, int x2, int y2, int speed)
		{
			for (int x = x1; x < x2; x++)
				AddConveyor(x, y1, Direction.Right, speed);
			for (int x = x1 + 1; x <= x2; x++)
				AddConveyor(x, y2, Direction.Left, speed);
			for (int y = y1 + 1; y <= y2; y++)
				AddConveyor(x1, y, Direction.Up, speed);
			for (int y = y1; y < y2; y++)
				AddConveyor(x2, y, Direction.Down, speed);
		}

		void AddCounterclockwiseConveyorOval(int x1, int y1, int x2, int y2, int speed)
		{
			for (int x = x1 + 1; x <= x2; x++)
				AddConveyor(x, y1, Direction.Left, speed);
			for (int x = x1; x < x2; x++)
				AddConveyor(x, y2, Direction.Right, speed);
			for (int y = y1; y < y2; y++)
				AddConveyor(x1, y, Direction.Down, speed);
			for (int y = y1 + 1; y <= y2; y++)
				AddConveyor(x2, y, Direction.Up, speed);
		}

		void AddPit(int x, int y)
		{
			var item = new Pit();
			item.position = new Position(x, y); ;
			boardElements.Add(item);
		}

		void AddFlag(int i, int x, int y)
		{
			var item = new Flag(i);
			item.position = new Position(x, y); ;
			boardElements.Add(item);
		}

		void AddHammer(int x, int y)
		{
			var item = new Hammer();
			item.position = new Position(x, y);
			boardElements.Add(item);
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
			Droids.Add(droid);
		}
	}
}
