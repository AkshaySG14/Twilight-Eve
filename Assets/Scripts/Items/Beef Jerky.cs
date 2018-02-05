using UnityEngine;
using System;

namespace AssemblyCSharp
{
	// Food used to lure creatures into your party. 
	public class Beef_Jerky : Item
	{
		public Beef_Jerky ()
		{
			// Sets the description.
			desc = "Marginally motivates creatures to join your party.";
			// Sets the reception message. 
			msg = "You pick up some beef jerky.";
			// Sets the name.
			name = "Beef Jerky";
			// Sets the gold. 
			cost = 75;
			// Sets the item. 
			item = GameObject.Find("Beef Jerky");
		}
	}
}

