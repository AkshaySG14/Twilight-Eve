using UnityEngine;
using System;

namespace AssemblyCSharp
{
	// An repel used to evade creatures for a short time. 
	public class Repel : Item
	{
		public Repel ()
		{
			// Sets the description.
			desc = "Evades creature encounters for 500 steps.";
			// Sets the reception message. 
			msg = "You pick up a repel.";
			// Sets the name.
			name = "Repel";
			// Sets the gold. 
			cost = 250;
			// Sets the item. 
			item = GameObject.Find("Repel");
		}
	}
}

