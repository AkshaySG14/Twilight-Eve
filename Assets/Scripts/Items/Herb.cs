using UnityEngine;
using System;

namespace AssemblyCSharp
{
	// An herb used to restore health by a small amount. 
	public class Herb : Item
	{
		public Herb ()
		{
			// Sets the description.
			desc = "Restores a creature's health by 30 HP.";
			// Sets the reception message. 
			msg = "You pick up an herb.";
			// Sets the name.
			name = "Herb";
			// Sets the gold. 
			cost = 20;
			// Sets the item. 
			item = GameObject.Find("Herb");
		}
	}
}

