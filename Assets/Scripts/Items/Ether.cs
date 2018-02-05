using UnityEngine;
using System;

namespace AssemblyCSharp
{
	// An ether used to restore mana by a small amount. 
	public class Ether : Item
	{
		public Ether ()
		{
			// Sets the description.
			desc = "Restores a creature's mana by 20 MP.";
			// Sets the reception message. 
			msg = "You pick up an ether.";
			// Sets the name.
			name = "Ether";
			// Sets the gold. 
			cost = 75;
			// Sets the item. 
			item = GameObject.Find("Ether");
		}
	}
}

