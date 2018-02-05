using UnityEngine;
using System;

namespace AssemblyCSharp
{
	// Food used to lure creatures into your party. 
	public class Pork_Chop : Item
	{
		public Pork_Chop ()
		{
			// Sets the description.
			desc = "Somewhat motivates creatures to join your party.";
			// Sets the reception message. 
			msg = "You pick up some pork chop.";
			// Sets the name.
			name = "Pork Chop";
			// Sets the gold. 
			cost = 325;
			// Sets the item. 
			item = GameObject.Find("Pork Chop");
		}
	}
}

