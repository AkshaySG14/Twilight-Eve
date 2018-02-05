using UnityEngine;
using System;

namespace AssemblyCSharp
{
	// Negates some physical damage. 
	public class Buckler : Item
	{
		public Buckler ()
		{
			// Sets the description.
			desc = "When equipped, marginally bolsters the def of a creature.";
			// Sets the reception message. 
			msg = "You pick up a buckler.";
			// Sets the name.
			name = "Buckler";
			// Sets the gold. 
			cost = 275;
			// Sets the item. 
			item = GameObject.Find("Buckler");
		}
	}
}

