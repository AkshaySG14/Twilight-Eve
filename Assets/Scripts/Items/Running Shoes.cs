using UnityEngine;
using System;

namespace AssemblyCSharp
{
	// Increases the evasion of a creature. 
	public class Running_Shoes : Item
	{
		public Running_Shoes ()
		{
			// Sets the description.
			desc = "When equipped, increases the evasion of a creature.";
			// Sets the reception message. 
			msg = "You pick up some running shoes.";
			// Sets the name.
			name = "Running Shoes";
			// Sets the shortname. 
			shortName = "Rng. Shoes";
			// Sets the gold. 
			cost = 750;
			// Sets the item. 
			item = GameObject.Find("Running Shoes");
		}
	}
}

