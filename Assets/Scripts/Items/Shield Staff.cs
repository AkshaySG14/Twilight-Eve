using UnityEngine;
using System;

namespace AssemblyCSharp
{
	// Staff that provides some shielding when used. 
	public class Shield_Staff : Item
	{
		public Shield_Staff ()
		{
			// Sets the description.
			desc = "When used, shields all creatures in the party.";
			// Sets the reception message. 
			msg = "You pick up a shield staff.";
			// Sets the name.
			name = "Shield Staff";
			// Sets the short name.
			shortName = "Shield St.";
			// Sets the gold. 
			cost = 1250;
			// Sets the item. 
			item = GameObject.Find("Shield Staff");
		}
	}
}

