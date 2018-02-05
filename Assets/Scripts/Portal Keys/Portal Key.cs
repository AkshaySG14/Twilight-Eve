using UnityEngine;
using System;

namespace AssemblyCSharp
{
	// The base class for all portal keys. 
	public class Portal_Key
	{
		// The text description of the portal key. 
		protected string desc = "";
		// The message received when obtaining the portal key.
		protected string msg = "";
		// The name of the portal key. 
		protected string name = "";
		// The game object of the portal key, used to display it and otherwise transfer it. 
		protected GameObject portal_key;

		public Portal_Key ()
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
			
		// Returns the game object that the item corresponds to. 
		public GameObject getPortalKey() {
			return portal_key;
		}

	}
}

