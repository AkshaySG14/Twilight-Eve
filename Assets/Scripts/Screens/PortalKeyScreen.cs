using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyCSharp
{
	public class PortalKeyScreen : GameScreen
	{
		// The current item. 
		private int currentKey = 0;

		// The current page. 
		private int currentPage = 0;

		// Max pages. 
		private int maxPages = 0;

		// The list of portal keys arranged by pages in an array. 
		private List<Portal_Key>[] portalKeys;

		public PortalKeyScreen ()
		{

		}

		void Start() {

		}

		public void setVariables() {
			// Sets the indicator and its sprite renderer.
			indicator = GameObject.Find ("Portal Key Indicator");
			iSpriteRenderer = (SpriteRenderer)indicator.GetComponent (typeof(SpriteRenderer));
			// Get max pages. 
			maxPages = Math.Abs(storage.getPortalKeys().Count - 1) / 7;
			// Sets the portal keys. 
			setPortalKeyList();
			// Sets the item list text. 
			setPortalKeyListText();
			// Moves the indicator accordingly. 
			moveIndicator();
		}

		// Sets the list of portal keys
		private void setPortalKeyList() {
			// Gets the portal key list per page (note there are 7 items per page). 
			portalKeys = new List<Portal_Key>[maxPages + 1];
			// Initialize list of each array element. 
			for (int i = 0; i < maxPages + 1; i++)
				portalKeys [i] = new List<Portal_Key> ();
			// Sets the keys of the portal key list.
			for (int i = 0; i < portalKeys.Length; i++)
				// Adds the portal key to the corresponding list.
				for (int o = 0; o < 7; o++) {
					// If the portal key is beyond the amount storage.portalKeys has, breaks the loop. 
					if (storage.getPortalKeys().Count <= i * 7 + o)
						break;
					else 
						portalKeys [i].Add (storage.getPortalKey (i * 7 + o));
				}
		}

		// If on a different screen, don't update.
		void Update() {
			// If not the inventory screen, return.
			if (screen != 4)
				return;
			// Else process. 
			checkInput();
			// Adds delta time and checks for blink.
			deltaTime += Time.deltaTime;
			// Checks for blink of indicator.
			checkBlink ();
		}

		private void checkInput() {
			// If recently switched from the confirmation screen, returns. 
			if (cooldown)
				return;
			// Goes back to main screen. 
			if (Input.GetKeyDown (KeyCode.Z))
				goToMainScreen ();
			// Goes up/down.
			if (Input.GetKeyDown (KeyCode.W))
				checkMovement (2);
			if (Input.GetKeyDown (KeyCode.S))
				checkMovement (-2);
			// Goes right/left.
			if (Input.GetKeyDown (KeyCode.D))
				checkMovement (1);
			if (Input.GetKeyDown (KeyCode.A))
				checkMovement (-1);
			// Makes a decision based on the position of the cursor. 
			if (Input.GetKeyDown (KeyCode.F))
				makeDecision ();
		}

		// Does an action in accordance with the position.
		private void makeDecision() {
			if (portalKeys [currentPage] [currentKey] != null)
				usePortalKey ();
		}

		// Uses the portal key.
		private void usePortalKey() {
		}
			
		// Reverse of the checkDecision() case. 
		private void goToMainScreen() {
			screen = 0;
			// Moves inventory screen to the pause screen's current position, then moves pause screen out of the way.
			GameObject.Find ("Pause Screen").transform.position = new Vector3 (this.gameObject.transform.position.x, 
				this.gameObject.transform.position.y, 0);
			this.gameObject.transform.position = new Vector3 (-50f, -50f, 0);
			// Moves the indicator in accordance with the general pause screen position. 
			((PauseScreen) GameObject.Find("Pause Screen").GetComponent(typeof(PauseScreen))).moveIndicator();
			// Resets the inventory screen. 
			reset();
			// Sets cooldown to true and then launches the coroutine to refresh it. This is to prevent bleeding of input.
			cooldown = true;
			StartCoroutine (coolDown ());
		}

		// Checks for movement in accordance with the direction given (i.e. S is down, W is up, etc.)
		private void checkMovement(int direction) {
			// Currently on the item list
			switch (direction) {
			// Right, if another page exists, turn to it. Else, do nothing.
			case 1:
				if (maxPages > currentPage)
					turnPage (1);
				return;
			// Left, if a page before exists, turn to it. Else, do nothing.
			case -1:
				if (currentPage > 0)
					turnPage (-1);
				break;
			// Up, go up item list.
			case 2:
				currentKey--;
				break;
			// Down, go down the item list. 
			case -2:
				currentKey++;
				break;
			}
			// If current item is less than zero or greater than seven (the max number of items), set currentItem to the
			// min/max respectively. 
			if (currentKey < 0)
				currentKey = 0;
			if (currentKey > 6)
				currentKey = 6;
			// Move the indicator accordingly. 
			moveIndicator ();
		}

		// Moves the indicator according to the position. 
		private void moveIndicator() {
			indicator.transform.localPosition = new Vector3 (-0.4f, 0.38f - currentKey * 0.1075f, 0f);
			// Set text description accordingly. 
			setPortalKeyDescriptionText ();
		}

		// Sets the text of the portal key list.
		private void setPortalKeyListText() {
			string portalKeyText = "";
			// Does not do anything if the current page has no items.
			if (portalKeys [currentPage].Count != 0)
				// Iterates through the list of items in the current page and sets the text correspondingly. 
				foreach (Portal_Key pKey in portalKeys[currentPage])
					portalKeyText = portalKeyText + pKey.getName () + "\n";
			
			// Sets the portal key list text object text to text. 
			((Text)GameObject.Find ("Portal Key Text").GetComponent (typeof(Text))).text = portalKeyText;
			// Sets the page number text to the current page / max pages. 
			((Text)GameObject.Find ("Portal Key Page Number Text").GetComponent (typeof(Text))).text = (currentPage + 1) + "/" + (maxPages + 1);
		}

		// Set portal key text description. 
		private void setPortalKeyDescriptionText() {
			string portalKeyDesc = "";
				// Sets item description to item description of the current item position in item list of the current page.
				if (currentKey < portalKeys [currentPage].Count)
				portalKeyDesc = portalKeys [currentPage] [currentKey].getDescription ();
		}

		// Turns the page (left or right). 
		private void turnPage(int dir) {
			// Adds the direction to the current page, effectively turning it. 
			currentPage += dir;
			// Makes sure min/max is not exceeded.
			if (currentPage < 0)
				currentPage = 0;
				if (currentPage > maxPages)
					currentPage = maxPages;
			
			// Sets item list text. 
			setPortalKeyListText();
			// Updates the item description text. 
			setPortalKeyDescriptionText ();
		}

		// Resets the position and current item when going back to the main menu.
		private void reset() {
			currentKey = 0;
			position = 0;
			moveIndicator ();
		}
	}
}

