using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Board
    {
        //Tile[,] tiles;
		Dictionary<Position, IBoardObject> tiles = new Dictionary<Position, IBoardObject>();
		public List<IBoardObject> boardElements = new List<IBoardObject>();

        public Board()
        {
            for (int i=0; i<10; i++)
			{
				var position = new Position(i, i);
				var flag = new Flag(i);
				flag.position = position;
				tiles[position] = new Flag(i);
				boardElements.Add(flag);
			}
        }
    }
}
