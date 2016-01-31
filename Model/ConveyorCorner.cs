namespace Model
{
	public class ConveyorCorner : IBoardObject
	{
		public Position position { get; set; }
		public Direction direcdtionIn { get; set; }
		public Direction direcdtionOut { get; set; }
		public int speed;
	}
}
