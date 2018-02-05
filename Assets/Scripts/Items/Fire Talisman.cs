using UnityEngine;
using System;

namespace AssemblyCSharp
{
	// Fire-resistant item equipped by creatures. 
	public class Fire_Talisman : Item
	{
		public Fire_Talisman ()
		{
			// Sets the description.
			desc = "When equipped, increases the fire resistance of a creature.";
			// Sets the reception message. 
			msg = "You pick up a fire talisman.";
			// Sets the name.
			name = "Fire Tali.";
			// Sets the short name.
			shortName = "Fire Tali.";
			// Sets the gold. 
			cost = 500;
			// Sets the item. 
			item = GameObject.Find("Fire Talisman");
		}
	}
}

