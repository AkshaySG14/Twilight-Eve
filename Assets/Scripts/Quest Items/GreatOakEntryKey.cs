using UnityEngine;
using System;

namespace AssemblyCSharp
{
	// The key used to gain entrance into the Great Oak proper (from the summit). 
	public class GreatOakEntryKey : QuestItem
	{
		public GreatOakEntryKey ()
		{
			// Sets the description of the Great Oak Entry Key.
			desc = "Key that unlocks the entrance to the lower levels of Great Oak.";
			// Sets the reception message. 
			msg = "You have received the key to the lower levels of Great Oak.";
			// Sets the name.
			name = "Great Oak Key";
			// Sets the item. 
			item = GameObject.Find("GreatOakKey");
		}
	}
}

