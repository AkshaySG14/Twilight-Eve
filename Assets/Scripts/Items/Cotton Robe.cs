using UnityEngine;
using System;

namespace AssemblyCSharp
{
	// Negates some magical damage. 
	public class Cotton_Robe : Item
	{
		public Cotton_Robe ()
		{
			// Sets the description.
			desc = "When equipped, marginally bolsters the mag def of a creature.";
			// Sets the reception message. 
			msg = "You pick up a cotton robe.";
			// Sets the name.
			name = "Cotton Robe";
			// Sets the short name.
			shortName = "Ctn. Shoes";
			// Sets the gold. 
			cost = 350;
			// Sets the item. 
			item = GameObject.Find("Cotton Robe");
		}
	}
}

