using UnityEngine;
using System;

namespace AssemblyCSharp
{
	// A potion used to cure poison. 
	public class Antidote : Item
	{
		public Antidote ()
		{
			// Sets the description.
			desc = "Cures a creature of any poisonous effects.";
			// Sets the reception message. 
			msg = "You pick up an antidote.";
			// Sets the name.
			name = "Antidote";
			// Sets the gold. 
			cost = 125;
			// Sets the item. 
			item = GameObject.Find("Antidote");
		}
	}
}

