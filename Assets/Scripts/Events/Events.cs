using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyCSharp
{
	// This is the class that is responsible for the starting event.
	public class StartingEvent : GameEvent {

		public StartingEvent(GameHandler gHandler) : base(gHandler) {
			
		}

			protected override void beginEvent() {
			// Sets the starting cell to the the first location of the game. 
			storage.setCells(6, -1);
			// Sets the camera accordingly. 
			gHandler.updateCamera();

			// Fades into the scene by repositioning the mask first, then making it fade in.
			mask.gameObject.transform.position = new Vector3((float) storage.getCellX() * 1.92f + 0.96f, 
				(float) storage.getCellY() * 1.92f - 0.96f, 0f);
			mask.fade(false, 2);

			// Gets the first spawn point of the game.
			GameObject spawnPoint = storage.getSpawnPoint();
			// Phases Lark, making him look like he is in bed. 
			larkScript.phase();
			// Moves Lark to his spawn point. Takes into account his width and height.
			lark.transform.position = spawnPoint.transform.position;
			// Note the addition of half his width and the subtraction of half his 
			// height. 
			lark.transform.Translate(new Vector3(0.08f, -0.08f, 0));
			// Stuns Daur to prevent movement. 
			larkScript.stun();
			// Plays the up animation and freezes Lark to simulate a meeting. 
			larkScript.setAnimation("Sleeping", 0);
			larkScript.freeze ();

			// Moves Irune, the sagely old man, to the point above Lark. This is to simulate a meeting.
			GameObject irune = GameObject.Find("Irune");
			irune.transform.position = GameObject.Find ("Irune Spawn 1").transform.position;
			// Adjusts to the center of the cell. 
			irune.transform.Translate (new Vector3 (0.07f, -0.14f, 0));

			// Creates the dialogue box and creates the dialogue. 
			message();
		}
			
		protected override void end() {
			// Awakens Lark, unstunning, unfreezing, and translating him.
			larkScript.wake ();
		}

		protected override void message() {
			switch (stage) {
			case 0:
				// Creates the initial dialogue for the game.
				DialogueHandler dHandler = storage.getDialogueHandler ();
				dHandler.startText (gHandler, storage, this, "You find yourself asleep in a strange land. " +
					"Although it seems that you are still in the embrace of a dream, you have been whisked " +
					"far, far away from your homeland. For what purpose or by whom remains unknown, " +
					"but now is not the time for speculation. Now is the time for awakening...", 2f);
				break;
			case 1: 
				// Breaks out of the game. 
				end ();
				break;
			}
		}
	}

	// This is a generic dialogue event. 
	public class GenericDialogueEvent : GameEvent {

		private string text; 
		// A potentially second event. 
		GameEvent sEvent;

		public GenericDialogueEvent(GameHandler gHandler, string text, GameEvent sEvent = null) : base(gHandler) {
			this.text = text;
			this.sEvent = sEvent;
		}

		protected override void beginEvent() {
			// Sets Lark's animation to disabled. 
			Animator anim = lark.GetComponent<Animator>();
			anim.enabled = false;
			// Stuns and freezes Lark. 
			larkScript.stun();
			larkScript.freeze ();
			// Starts dialogue.
			message ();
		}

		protected override void end() {
			// Sets current event to null. 
			gHandler.setCurrentEvent (null);
			// Unstuns and unfreezes Lark. 
			larkScript.unStun ();
			larkScript.unFreeze ();
			// Sets Lark's animation to disabled. 
			Animator anim = lark.GetComponent<Animator>();
			anim.enabled = false;
		}

		protected override void message() {
			switch (stage) {
			case 0:
				// Creates the initial dialogue for the game.
				DialogueHandler dHandler = storage.getDialogueHandler ();
				dHandler.startText (gHandler, storage, this, text, 0f);
				break;
			case 1: 
				// Breaks out of the game. 
				end ();
				// If there is a second event, launches it. 
				if (sEvent != null) {
					gHandler.setCurrentEvent (sEvent);
					sEvent.begin ();
				}
				break;
			}
		}
	}

	// This is an item event. 
	public class ItemEvent : GameEvent {
	
		// The item being given. 
		private Item item;
		// A potentially second event. 
		GameEvent sEvent;

		public ItemEvent(GameHandler gHandler, Item item, GameEvent sEvent = null) : base(gHandler) {
			this.item = item;
			this.sEvent = sEvent;
		}

		protected override void beginEvent() {
			// Sets Lark's animation to disabled. 
			Animator anim = lark.GetComponent<Animator>();
			anim.enabled = false;
			// Stuns and freezes Lark. 
			larkScript.stun();
			larkScript.freeze ();
			// Gets Lark's sprite renderer to get his position more effectively. 
			SpriteRenderer lSpriteRenderer = (SpriteRenderer) lark.GetComponent(typeof(SpriteRenderer));
			// Moves the item to above Lark, to display it being acquired by him.
			item.getItem ().transform.position = new Vector3 (lSpriteRenderer.bounds.center.x, 
				lSpriteRenderer.bounds.max.y + 0.1f, 0);
			// Starts item reception.
			message ();
		}

		protected override void end() {
			// Sets current event to null. 
			gHandler.setCurrentEvent (null);
			// Unstuns and unfreezes Lark. 
			larkScript.unStun ();
			larkScript.unFreeze ();
			// Sets Lark's animation to disabled. 
			Animator anim = lark.GetComponent<Animator>();
			anim.enabled = false;
			// Moves the item's position back to zero, so as to avoid being seen. 
			item.getItem().transform.position = new Vector3(0, 0, 0);
		}

		protected override void message() {
			switch (stage) {
			case 0:
				// Creates the dialogue alerting the player that he has received an item.
				DialogueHandler dHandler = storage.getDialogueHandler ();
				dHandler.startText (gHandler, storage, this, item.getMessage(), 0f);
				break;
			case 1: 
				// Adds the item to the quest item list or the item list. 
				if (item is QuestItem)
					storage.addQuestItem ((QuestItem)item);
				else
					storage.addItem (item);
				
				// If there is a second event, launches it. 
				if (sEvent != null) {
					gHandler.setCurrentEvent (sEvent);
					sEvent.begin ();
				}
				// Else, Breaks out of the game. 
				else
					end ();
				break;
			case 2:
				end ();
				break;

			}
		}
	}

	// This is a shop event. 
	public class ShopEvent : GameEvent {
		// The list of items for the shop.
		List<Item> itemList;

		public ShopEvent(GameHandler gHandler, List<Item> itemList) : base(gHandler) {
			this.itemList = itemList;
		}

		protected override void beginEvent() {
			// Sets Lark's animation to disabled. 
			Animator anim = lark.GetComponent<Animator>();
			anim.enabled = false;
			// Stuns and freezes Lark. 
			larkScript.stun();
			larkScript.freeze ();
			// Starts shop event.
			message ();
		}

		protected override void end() {
			// Sets current event to null. 
			gHandler.setCurrentEvent (null);
			// Unstuns and unfreezes Lark. 
			larkScript.unStun ();
			larkScript.unFreeze ();
			// Sets Lark's animation to disabled. 
			Animator anim = lark.GetComponent<Animator>();
			anim.enabled = false;
		}

		protected override void message() {
			switch (stage) {
			case 0:
				// Switches the screen to the shop screen.
				GameObject.Find ("Shop Screen").transform.position = new Vector3 (storage.getCellX () * 1.92f + 0.723f, 
					storage.getCellY () * 1.92f - 0.98f, 0);
				((ShopScreen)GameObject.Find ("Shop Screen").GetComponent (typeof(ShopScreen))).setVariables(this, itemList);
				larkScript.pause ();
				gHandler.paused = true;
				break;
			case 1: 
				// Breaks out of the screen.
				gHandler.unpauseGame();
				GameObject.Find ("Shop Screen").transform.position = new Vector3 (-50, -50, 0);
				larkScript.unpause ();
				end ();
				break;
			}
		}
	}

	// This is the event that opens the Great Oak Door. 
	public class GreatOakOpeningEvent : GameEvent {
		
		public GreatOakOpeningEvent(GameHandler gHandler) : base(gHandler) {
		}

		protected override void beginEvent() {
			// Sets Lark's animation to disabled. 
			Animator anim = lark.GetComponent<Animator>();
			anim.enabled = false;
			// Stuns and freezes Lark. 
			larkScript.stun();
			larkScript.freeze ();
			// Starts event.
			message ();
		}

		protected override void end() {
			// Sets current event to null. 
			gHandler.setCurrentEvent (null);
			// Unstuns and unfreezes Lark. 
			larkScript.unStun ();
			larkScript.unFreeze ();
			// Sets Lark's animation to disabled. 
			Animator anim = lark.GetComponent<Animator>();
			anim.enabled = false;
			// Increment main quest. 
			storage.advMainQuest();
		}

		protected override void message() {
			switch (stage) {
			case 0:
				gHandler.shakeScreen (this);
				break;
			case 1: 
				openDoor ();
				break;
			case 2:
				end ();
				break;
			}
		}

		// Opens the door to the rest of Great Oak. 
		private void openDoor() {
			// Displaces the left and right door to make the door seem as though it has opened. 
			GameObject.Find("Door Left").transform.position = new Vector3(-0.16f, 0, 0);
			GameObject.Find ("Door Right").transform.position = new Vector3 (0.16f, 0, 0);
			proceed ();
		}
	}
}