using UnityEngine;
using System;

namespace AssemblyCSharp
{
	// Healing staff that provides some recuperation when used. 
	public class Healing_Staff : Item
	{
		public Healing_Staff ()
		{
			// Sets the description.
			desc = "When used, heals all creatures in the party.";
			// Sets the reception message. 
			msg = "You pick up a healing staff.";
			// Sets the name.
			name = "Healing Staff";
			// Sets the short name.
			shortName = "Healing St.";
			// Sets the gold. 
			cost = 600;
			// Sets the item. 
			item = GameObject.Find("Healing Staff");
		}
	}
}

