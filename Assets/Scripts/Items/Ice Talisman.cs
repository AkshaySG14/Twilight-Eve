using UnityEngine;
using System;

namespace AssemblyCSharp
{
	// Frost-resistant item equipped by creatures. 
	public class Ice_Talisman : Item
	{
		public Ice_Talisman ()
		{
			// Sets the description.
			desc = "When equipped, increases the frost resistance of a creature.";
			// Sets the reception message. 
			msg = "You pick up an ice talisman.";
			// Sets the name.
			name = "Ice Tali.";
			// Sets the short name.
			shortName = "Ice Tali.";
			// Sets the gold. 
			cost = 500;
			// Sets the item. 
			item = GameObject.Find("Ice Talisman");
		}
	}
}

