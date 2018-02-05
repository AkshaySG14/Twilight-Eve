using UnityEngine;
using System;

namespace AssemblyCSharp
{
	// A special jug of water that restores health by a moderate amount. 
	public class RoseWater : Item
	{
		public RoseWater ()
		{
			// Sets the description.
			desc = "Restores a creature's health by 50 HP.";
			// Sets the reception message. 
			msg = "You pick up a jug of rosewater.";
			// Sets the name.
			name = "Rose Water";
			// Sets the gold. 
			cost = 200;
			// Sets the item. 
			item = GameObject.Find("Rose Water");
		}
	}
}

