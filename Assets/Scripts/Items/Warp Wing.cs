using UnityEngine;
using System;

namespace AssemblyCSharp
{
	// Transports Lark outside of the current dungeon. 
	public class Warp_Wing : Item
	{
		public Warp_Wing ()
		{
			// Sets the description.
			desc = "Transports Lark outside of any dungeon he's in.";
			// Sets the reception message. 
			msg = "You pick up a warp wing.";
			// Sets the name.
			name = "Warp Wing";
			// Sets the gold. 
			cost = 100;
			// Sets the item. 
			item = GameObject.Find("Warp Wing");
		}
	}
}

