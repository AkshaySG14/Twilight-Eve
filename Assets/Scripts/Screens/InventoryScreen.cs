using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyCSharp
{
	public class InventoryScreen : GameScreen
	{
		// The current item. 
		private int currentItem = 0;

		// The current page. 
		private int currentPage = 0;

		// Max pages for items and quest items respectively. 
		private int maxItemPages = 0;

		private int maxQuestItemPages = 0;

		// Whether the cursor is on items or quest items. 
		private bool items = true;

		// The list of items arranged by pages in an array. 
		private List<Item>[] itemList;

		// The list of quest items arranged by pages in an array. 
		private List<QuestItem>[] questitemList;

		public InventoryScreen ()
		{
			
		}

		void Start() {

		}

		public void setVariables() {
			// Sets the indicator and its sprite renderer.
			indicator = GameObject.Find ("Item List Indicator");
			iSpriteRenderer = (SpriteRenderer)indicator.GetComponent (typeof(SpriteRenderer));
			// Get max pages. 
			maxItemPages = Math.Abs(storage.getItems().Count - 1) / 7;
			maxQuestItemPages = Math.Abs(storage.getQuestItems().Count - 1) / 7;
			// Sets the item list. 
			setItemList();
			// Sets the item list text. 
			setItemListText();
			// Moves the indicator accordingly. 
			moveIndicator();
		}

		// Sets the items
		private void setItemList() {
			// Gets the item list per page (note there are 7 items per page). 
			itemList = new List<Item>[maxItemPages + 1];
			// Initialize list of each array element. 
			for (int i = 0; i < maxItemPages + 1; i++)
				itemList [i] = new List<Item> ();
			// Sets the items of the item list.
			for (int i = 0; i < itemList.Length; i++)
			// Adds the corresponding item to the corresponding list.
				for (int o = 0; o < 7; o++) {
					// If the item is beyond the amount storage.items has, breaks the loop. 
					if (storage.getItems().Count <= i * 7 + o)
						break;
					else 
						itemList [i].Add (storage.getItem (i * 7 + o));
				}

			// Gets the quest item list per page (note there are 7 items per page). 
			questitemList = new List<QuestItem>[maxQuestItemPages + 1];
			// Initialize list of each array element. 
			for (int i = 0; i < maxQuestItemPages + 1; i++)
				questitemList [i] = new List<QuestItem> ();
			// Sets the items of the item list.
			for (int i = 0; i < questitemList.Length; i++)
			// Adds the corresponding item to the corresponding list.
				for (int o = 0; o < 7; o++) {
					if (storage.getQuestItems ().Count <= i * 7 + o)
						break;
					else
						questitemList [i].Add (storage.getQuestItem (i * 7 + o));
				}
			
		}

		// If on a different screen, don't update.
		void Update() {
			// If not the inventory screen, return.
			if (screen != 2)
				return;
			// Else process. 
			checkInput();
			// Adds delta time and checks for blink.
			deltaTime += Time.deltaTime;
			// Checks for blink of indicator.
			checkBlink ();
			// Checks if a UI element should be highlighted (f the user has the cursor on the left side).
			checkHighlight();
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
			switch (position) {
			// At "Items", set items to be true.
			case 0:
				items = true;
				setItemListText ();
				break;
			// At "Quest Items", set quest items to be true. 
			case 1:
				items = false;
				setItemListText ();
				break;
			// At the item list, use the item. 
			case 2:
				if ((items && itemList[currentPage][currentItem] != null) ||
					(!items && questitemList[currentPage][currentItem] != null))
					useItem ();
				break;
			}
		}

		// Uses the item.
		private void useItem() {
			
		}

		private void checkHighlight() {
			// The cursor is technically over "Items".
			if (position == 0) {
				// Sets items to gray or gray, as it is being hovered over. 
				// If it has been pressed, set to light gray. 
				if (items)
					((SpriteRenderer)GameObject.Find ("Items Indicator").GetComponent (typeof(SpriteRenderer))).color = new Color (0.8f, 0.8f, 0.8f, 1);
				// Else set to gray.
				else
					((SpriteRenderer)GameObject.Find ("Items Indicator").GetComponent (typeof(SpriteRenderer))).color = new Color (0.5f, 0.5f, 0.5f, 1);
				// Sets quest items to white or dark gray, depending if it has been pressed or not. 
				// Has not been pressed, set to dark gray. 
				if (items)
					((SpriteRenderer) GameObject.Find("Quest Items Indicator").GetComponent(typeof(SpriteRenderer))).color = new Color (0.25f, 0.25f, 0.25f, 1);
				// Else set it to white.
				else 
					((SpriteRenderer) GameObject.Find("Quest Items Indicator").GetComponent(typeof(SpriteRenderer))).color = Color.white;
			}
			// The cursor is technically over "Quest Items". 
			if (position == 1) {
				// Sets quest items to gray, as it is being hovered over. 
				// If it has been pressed, set to light gray. 
				if (!items)
					((SpriteRenderer)GameObject.Find ("Quest Items Indicator").GetComponent (typeof(SpriteRenderer))).color = new Color (0.8f, 0.8f, 0.8f, 1);
				// Else set to gray.
				else
					((SpriteRenderer)GameObject.Find ("Quest Items Indicator").GetComponent (typeof(SpriteRenderer))).color = new Color (0.5f, 0.5f, 0.5f, 1);
				// Sets items to white or dark gray, depending if it has been pressed or not. 
				// Has been pressed, set to white. 
				if (items)
					((SpriteRenderer) GameObject.Find("Items Indicator").GetComponent(typeof(SpriteRenderer))).color = Color.white;
				// Else set it to dark gray.
				else 
					((SpriteRenderer) GameObject.Find("Items Indicator").GetComponent(typeof(SpriteRenderer))).color = new Color (0.25f, 0.25f, 0.25f, 1);
			}
			// The cursor is over the item list. Highlight the pressed item white and the other dark gray. 
			if (position == 2) {
				if (items) {
					((SpriteRenderer)GameObject.Find ("Items Indicator").GetComponent (typeof(SpriteRenderer))).color = Color.white;
					((SpriteRenderer)GameObject.Find ("Quest Items Indicator").GetComponent (typeof(SpriteRenderer))).color = new Color (0.25f, 0.25f, 0.25f, 1);
				} 
				else {
					((SpriteRenderer) GameObject.Find("Quest Items Indicator").GetComponent(typeof(SpriteRenderer))).color = Color.white;
					((SpriteRenderer) GameObject.Find("Items Indicator").GetComponent(typeof(SpriteRenderer))).color = new Color (0.25f, 0.25f, 0.25f, 1);
				}
			}
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
			// Moves according to the current position.
			switch (position) {
			// Currently on "Items".
			case 0: 
				switch (direction) {
				// Right, go to item list.
				case 1:
					position = 2;
					break;
				// Left or up, do nothing.
				case -1:
				case 2:
					return;
				// Down, go to quest items.
				case -2:
					position = 1;
					break;
				}
				break;
			// Currently on "Quest Items".
			case 1:
				switch (direction) {
				// Right, go to item list.
				case 1:
					position = 2;
					break;
				// Left or down, do nothing.
				case -1:
				case -2:
					return;
				// Up, go to items.
				case 2:
					position = 0;
					break;
				}
				break;
			// Currently on the item list
			case 2:
				switch (direction) {
				// Right, if another page exists, turn to it. Else, do nothing.
				case 1:
					if (items) {
						if (maxItemPages > currentPage)
							turnPage (1);
					} 
					else 
						if (maxQuestItemPages > currentPage)
							turnPage (1);
					return;
					// Left, if a page before exists, turn to it. Else, go to "items" or "quest items" depending on which 
						// is selected.
				case -1:
					if (currentPage > 0)
						turnPage (-1);
					else {
						if (items)
							position = 0;
						else
							position = 1;
					}
					break;
				// Up, go up item list.
				case 2:
					currentItem --;
					break;
				// Down, go down the item list. 
				case -2:
					currentItem++;
					break;
				}
				break;
			}
			// If current item is less than zero or greater than seven (the max number of items), set currentItem to the
			// min/max respectively. 
			if (currentItem < 0)
				currentItem = 0;
			if (currentItem > 6)
				currentItem = 6;
			// Move the indicator accordingly. 
			moveIndicator ();
		}

		// Moves the indicator according to the position. 
		private void moveIndicator() {
			switch (position) {
			// On items, so no need for indicator.
			case 0:
				indicator.transform.position = new Vector3 (-50f, -50f, 0);
				break;
			// On quest items, same as above.
			case 1:
				indicator.transform.position = new Vector3 (-50f, -50f, 0);
				break;
			// On the item list. Shift indicator depending on current item.
			case 2:
				indicator.transform.localPosition = new Vector3 (-0.4f, 0.38f - currentItem * 0.1075f, 0f);
				// Set text description accordingly. 
				setItemDescriptionText();
				break;
			}
		}

		// Sets the text item list.
		private void setItemListText() {
			string itemListText = "";
			// If on items. 
			if (items) {
				// Does not do anything if the current page has no items.
				if (itemList[currentPage].Count != 0)
				// Iterates through the list of items in the current page and sets the text correspondingly. 
					foreach (Item item in itemList[currentPage])
						itemListText = itemListText + item.getName () + "\n";
			} 
			else {
				// Does not do anything if the current page has no quest items.
				if (questitemList [currentPage].Count != 0)
				// Iterates through the list of quest items in the current page and sets the text correspondingly. 
					foreach (QuestItem item in questitemList[currentPage])
						itemListText = itemListText + item.getName () + "\n";
			}
			// Sets the item list text object text to text. 
			((Text) GameObject.Find ("Item List Text").GetComponent (typeof(Text))).text = itemListText;
			// Sets the page number text to the current page / max pages. 
			if (items)
				((Text) GameObject.Find ("Page Number Text").GetComponent (typeof(Text))).text = (currentPage + 1) + "/" + (maxItemPages + 1);
			else 
				((Text) GameObject.Find ("Page Number Text").GetComponent (typeof(Text))).text = (currentPage + 1) + "/" + (maxQuestItemPages + 1);
		}

		// Set item text description. 
		private void setItemDescriptionText() {
			string itemDesc = "";
			// If on items.
			if (items) {
				// Sets item description to item description of the current item position in item list of the current page.
				if (currentItem < itemList [currentPage].Count)
					itemDesc = itemList [currentPage] [currentItem].getDescription ();
			}
			// Else if on quest items.
			else 
				if (currentItem < questitemList [currentPage].Count)
					itemDesc = questitemList [currentPage] [currentItem].getDescription ();
			// Sets the game object text accordingly. 
			((Text) GameObject.Find ("Explanation Text").GetComponent (typeof(Text))).text = itemDesc;
		}

		// Turns the page (left or right). 
		private void turnPage(int dir) {
			// Adds the direction to the current page, effectively turning it. 
			currentPage += dir;
			// Makes sure min/max is not exceeded.
			if (currentPage < 0)
				currentPage = 0;
			if (items) {
				if (currentPage > maxItemPages)
					currentPage = maxItemPages;
			} 
			else 
				if (currentPage > maxQuestItemPages)
					currentPage = maxQuestItemPages;
			// Sets item list text. 
			setItemListText();
			// Updates the item description text. 
			setItemDescriptionText ();
		}

		// Resets the position and current item when going back to the main menu.
		private void reset() {
			currentItem = 0;
			position = 0;
			moveIndicator ();
		}
	}
}

