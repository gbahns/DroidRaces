using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Model
{
	[ImplementPropertyChanged]
	public class Droid : IBoardObject, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public Position position { get; set; }
		public Direction direction { get; set; }
		public DroidColor color { get; set; }
		public int lives { get; set; }
		public int damage { get; set; }
		public bool poweredDown { get; set; }
		public ObservableCollection<Instruction> Instructions { get; }

		public Droid()
		{
			lives = 3;
			Instructions = new ObservableCollection<Instruction>();
		}


		Position GetPositionLeft()
		{
			return new Position(position.X - 1, position.Y);
		}

		Position GetPositionRight()
		{
			return new Position(position.X + 1, position.Y);
		}

		Position GetPositionUp()
		{
			return new Position(position.X, position.Y - 1);
		}

		Position GetPositionDown()
		{
			return new Position(position.X, position.Y + 1);
		}

		Position GetPositionForward()
		{
			switch (direction)
			{
				case Direction.Left: return GetPositionLeft();
				case Direction.Right: return GetPositionRight();
				case Direction.Up: return GetPositionUp();
				case Direction.Down: return GetPositionDown();
				default: return position;
			}
		}

		Position GetPositionBackward()
		{
			switch (direction)
			{
				case Direction.Left: return GetPositionRight();
				case Direction.Right: return GetPositionLeft();
				case Direction.Up: return GetPositionDown();
				case Direction.Down: return GetPositionUp();
				default: return position;
			}
		}

		void MoveForward(int moves)
		{
			for (int i = 0; i < moves; i++)
			{
				position = GetPositionForward();
			}
		}

		void MoveBackward()
		{
			position = GetPositionBackward();
		}

		void TurnRight()
		{
			if ((int)direction == 3)
				direction = (Direction)0;
			else
				direction += 1;
		}

		void TurnLeft()
		{
			if ((int)direction == 0)
				direction = (Direction)3;
			else
				direction -= 1;
		}

		void TurnAround()
		{
			if ((int)direction <= 1)
				direction += 2;
			else
				direction -= 2;
		}

		public void ExecuteInstruction()
		{
			if (Instructions.Count > 0)
			{
				var instruction = Instructions[0];
				Instructions.RemoveAt(0);
				switch (instruction)
				{
					case Instruction.Move1:
						MoveForward(1);
						break;
					case Instruction.Move2:
						MoveForward(2);
						break;
					case Instruction.Move3:
						MoveForward(3);
						break;
					case Instruction.Backup:
						MoveBackward();
						break;
					case Instruction.TurnLeft:
						TurnLeft();
						break;
					case Instruction.TurnRight:
						TurnRight();
						break;
					case Instruction.UTurn:
						TurnAround();
						break;
				}
			}
		}
	}
}
