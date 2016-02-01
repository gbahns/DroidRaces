namespace Model
{
	public class Conveyor : IBoardObject
	{
		public Position position { get; set; }
		public Direction direction { get; set; }
		public int speed;
	}
}
