namespace Model
{
	public class Droid : IBoardObject
	{
		public Position position { get; set; }
		public Direction direction { get; set; }
		public DroidColor color { get; set; }
	}
}
