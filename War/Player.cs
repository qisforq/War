using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace War
{
	public class Player
	{
		public List<Card> Hand = new List<Card>();
		public bool IsWinner { get; set; }
	}
}