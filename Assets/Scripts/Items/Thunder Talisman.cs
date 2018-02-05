using UnityEngine;
using System;

namespace AssemblyCSharp
{
	// Lightning-resistant item equipped by creatures. 
	public class Thunder_Talisman : Item
	{
		public Thunder_Talisman ()
		{
			// Sets the description.
			desc = "When equipped, increases the th. resistance of a creature.";
			// Sets the reception message. 
			msg = "You pick up a thunder talisman.";
			// Sets the name.
			name = "Thunder Talisman";
			// Sets the short name.
			shortName = "Thunder Tali.";
			// Sets the gold. 
			cost = 500;
			// Sets the item. 
			item = GameObject.Find("Thunder Talisman");
		}
	}
}

