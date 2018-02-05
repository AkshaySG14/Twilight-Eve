using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyCSharp
{
	public class ShopScreen : GameScreen
	{
		// The current item. 
		private int currentItem = 0;

		// The list of items arranged by pages in an array. 
		private List<Item>[] itemList;

		// The current page. 
		private int currentPage = 0;

		// Max pages for items and quest items respectively. 
		private int maxItemPages = 0;

		// The shop event responsible for the launch of this screen. 
		private ShopEvent sEvent;

		public ShopScreen ()
		{

		}

		void Start() {
			// Sets the indicator and its sprite renderer.
			indicator = GameObject.Find ("Shop List Indicator");
			iSpriteRenderer = (SpriteRenderer)indicator.GetComponent (typeof(SpriteRenderer));
		}

		public void setVariables(ShopEvent sEvent, List<Item> givenList) {
			// Cools down, so as to prevent input spillage.
			cooldown = true;
			StartCoroutine (coolDown ());
			// Sets screen to 5.
			screen = 5;
			// Sets the shop event that lauunched this screen. 
			this.sEvent = sEvent;

			// Get max pages. 
			maxItemPages = Math.Abs(givenList.Count - 1) / 9;
			// Gets the item list per page (note there are 9 items per page). 
			itemList = new List<Item>[maxItemPages + 1];
			// Initialize list of each array element. 
			for (int i = 0; i < maxItemPages + 1; i++)
				itemList [i] = new List<Item> ();
			// Sets the items of the item list.
			for (int i = 0; i < itemList.Length; i++)
				// Adds the corresponding item to the corresponding list.
				for (int o = 0; o < 9; o++) {
					// If the item is beyond the amount givenList has, breaks the loop. 
					if (givenList.Count <= i * 9 + o)
						break;
					else 
						itemList [i].Add (givenList[i * 9 + o]);
				}
			// Sets the item list text. 
			setItemListText();
			// Moves the indicator accordingly. 
			moveIndicator();
		}
			
		// If on a different screen, don't update.
		void Update() {
			// If not the shop screen, return.
			if (screen != 5)
				return;
			// Else process. 
			checkInput ();
			// Adds delta time and checks for blink.
			deltaTime += Time.deltaTime;
			// Checks for blink of indicator.
			checkBlink ();
		}

		private void checkInput() {
			if (cooldown)
				return;
			// Returns to the game if the Z button is pressed and off cooldown. 
			if (Input.GetKeyDown (KeyCode.Z) && !cooldown) {
				reset ();
				sEvent.proceed ();
				screen = 0;
			}
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
			// Tries to buy the current item. 
			if (Input.GetKeyDown (KeyCode.F))
				attemptBuyItem ();
		}

		// Attempts to the buy the item, checking if Lark has the money available and then launching the confirmation 
		// screen.
		private void attemptBuyItem() {
			// If the current item is null, or the cost exceeds Lark's bank, returns. 
			if (currentItem > itemList[currentPage].Count - 1 || itemList [currentPage][currentItem].getCost () > storage.getGold ())
				return;

			// Launches a confirmation screen.
			((ConfirmationScreen)GameObject.Find ("Confirmation Window").GetComponent (typeof(ConfirmationScreen))).
			activate (this, "Buy", storage.getCellX () * 1.92f + 1.48f, storage.getCellY () * 1.92f - 0.6225f);
			screen = 6;

		}

		// Buys the item. 
		private void buyItem() {
			// Reduces gold and adds the item to inventory. 
			storage.setGold (storage.getGold () - itemList [currentPage] [currentItem].getCost ());
			storage.addItem (itemList [currentPage] [currentItem]);
			// Updates amount of gold in the UI.
			setItemListText ();

		}
			
		// Checks for movement in accordance with the direction given (i.e. S is down, W is up, etc.)
		private void checkMovement(int direction) {
			switch (direction) {
			// Right, if another page exists, turn to it. Else, do nothing.
			case 1:
				if (maxItemPages > currentPage)
					turnPage (1);
				return;
				// Left, if a page before exists, turn to it. Else, do nothing.
			case -1:
				if (currentPage > 0)
					turnPage (-1);
				else
					return;
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
			// If current item is less than zero or greater than seven (the max number of items), set currentItem to the
			// min/max respectively. 
			if (currentItem < 0)
				currentItem = 0;
			if (currentItem > 8)
				currentItem = 8;
			// Move the indicator accordingly. 
			moveIndicator ();
		}

		// Moves the indicator according to the position. 
		private void moveIndicator() {
			// Sets the indicator's position in accordance to the current item selected. 
			indicator.transform.localPosition = new Vector3 (-0.415f, 0.38f - currentItem * 0.09f, 0f);
			// Set text description accordingly. 
			setItemDescriptionText ();
		}

		// Turns the page (left or right). 
		private void turnPage(int dir) {
			// Adds the direction to the current page, effectively turning it. 
			currentPage += dir;
			// Makes sure min/max is not exceeded.
			if (currentPage < 0)
				currentPage = 0;
			if (currentPage > maxItemPages)
				currentPage = maxItemPages;
			// Sets item list text. 
			setItemListText ();
			// Updates the item description text. 
			setItemDescriptionText ();
		}

		// Sets the text item list.
		private void setItemListText() {
			string itemListText = "";
			string costText = "";
			// Does not do anything if the current page has no items.
			if (itemList [currentPage].Count != 0)
				// Iterates through the list of items in the current page and sets the text correspondingly. 
				foreach (Item item in itemList[currentPage])
					itemListText = itemListText + item.getShortName () + "\n";
			// Does not do anything if the current page has no items.
			if (itemList [currentPage].Count != 0)
				// Iterates through the list of items in the current page and sets the text correspondingly. 
				foreach (Item item in itemList[currentPage])
					costText = costText + item.getCost ().ToString () + "G" + "\n";
			
			// Sets the item list text object text to item text. 
			((Text)GameObject.Find ("Shop List Text").GetComponent (typeof(Text))).text = itemListText;
			// Sets the item cost list text object text to cost text. 
			((Text)GameObject.Find ("Shop List Cost Text").GetComponent (typeof(Text))).text = costText;

			// Sets the gold amount to reflect Lark's current gold. 
			string gold;
			// If the gold is in the 1000's digit (e.g. 5000), leave it as is. 
			if (storage.getGold () >= 1000)
				gold = storage.getGold ().ToString ();
			// If the gold is in the 100's digit (e.g. 500), add one 0 to the beginning.
			else if (storage.getGold () >= 100)
				gold = "0" + storage.getGold ().ToString ();
			// If the gold is in the 10's digit (e.g. 50), add two 0's to the beginning.
			else if (storage.getGold () >= 10)
				gold = "00" + storage.getGold ().ToString ();
			// If the gold is in the 0's digit (e.g. 5), add three 0's to the beginning.
			else
				gold = "000" + storage.getGold ().ToString ();
			// Sets the text object to the gold string.
			((Text)GameObject.Find ("Money Shop").GetComponent (typeof(Text))).text = gold;

			// Sets page text.
			((Text) GameObject.Find ("Shop Page Number Text").GetComponent (typeof(Text))).text = (currentPage + 1) + "/" + (maxItemPages + 1);
		}

		// Set item text description. 
		private void setItemDescriptionText() {
			string itemDesc = "";
			// Sets item description to item description of the current item position in item list of the current page.
			if (currentItem < itemList [currentPage].Count)
				itemDesc = itemList [currentPage] [currentItem].getDescription ();		
			// Sets the game object text accordingly. 
			((Text)GameObject.Find ("Shop Item Explanation Text").GetComponent (typeof(Text))).text = itemDesc;
		}

		// Resets the position and current item.
		private void reset() {
			currentItem = 0;
			position = 0;
			moveIndicator ();
		}

		// Acts based on input from confirmation window.
		public override void getInput(bool input, string key) {
			// Immediately sets the screen to this one (5). 
			screen = 5;
			// Sets cooldown to true, then cools down. 
			cooldown = true;
			StartCoroutine (coolDown ());

			// This is part of a buy item chain.
			if (key.Equals("Buy")) {
				// User has pressed OK; buy item.
				if (input)
					buyItem();
				// Else, do nothing.
				else 
					return;
			}
		}
	}
}

