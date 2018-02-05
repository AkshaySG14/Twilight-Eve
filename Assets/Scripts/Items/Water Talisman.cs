using UnityEngine;
using System;

namespace AssemblyCSharp
{
	// Water-resistant item equipped by creatures. 
	public class Water_Talisman : Item
	{
		public Water_Talisman ()
		{
			// Sets the description.
			desc = "When equipped, increases the water resistance of a creature.";
			// Sets the reception message. 
			msg = "You pick up a water talisman.";
			// Sets the name.
			name = "Water Tali.";
			// Sets the short name.
			shortName = "Water Tali.";
			// Sets the gold. 
			cost = 500;
			// Sets the item. 
			item = GameObject.Find("Water Talisman");
		}
	}
}

