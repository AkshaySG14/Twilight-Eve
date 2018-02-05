using UnityEngine;
using System;

namespace AssemblyCSharp
{
	// The base class for all items. 
	public class Item
	{
		// The text description of the item. 
		protected string desc = "";
		// The message received when obtaining the item.
		protected string msg = "";
		// The name of the item. 
		protected string name = "";
		// The shortname of the item, defaulted to the original name. Used in shops. 
		protected string shortName = "";
		// The cost of the item, if applicable. 
		protected int cost = 0;
		// The game object of the item, used to display it and otherwise transfer it. 
		protected GameObject item;

		public Item ()
		{
			
		}

		// Returns the description of the item. 
		public string getDescription() {
			return desc;
		}

		// Returns the reception message of the item. 
		public string getMessage() {
			return msg;
		}

		// Returns the name of the item. 
		public string getName() {
			return name;
		}

		// Returns the name of the item if empty. Otherwise, returns the short name.
		public string getShortName() {
			if (shortName.Equals(""))
				return name;
			else 
				return shortName;
		}

		// Returns the game object that the item corresponds to. 
		public GameObject getItem() {
			return item;
		}

		// Returns the cost of the item. 
		public int getCost() {
			return cost;
		}
	}
}

