using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyCSharp
{
	// The class responsible for handling the various characters of the game. 
	public class CharacterHandler : MonoBehaviour
	{
		private Storage storage;

		private GameHandler gHandler;

		public CharacterHandler ()
		{

		}

		void Start() {
		}

		public void setInitialVariables(GameHandler gHandler, Storage storage) {
			this.gHandler = gHandler;
			this.storage = storage;
			spawnCharacters ();
		}

		// Spawns all the different characters. 
		private void spawnCharacters() {
			// The variable game object. 
			GameObject character;
			// The variable sprite renderer. 
			SpriteRenderer charRenderer;
			// Princess.
			character = GameObject.Find("Princess");
			((Character)character.GetComponent (typeof(Character))).setInitialVariables (gHandler, storage);
			charRenderer = (SpriteRenderer) character.GetComponent (typeof(SpriteRenderer));
			character.transform.position = GameObject.Find ("Princess Spawn").transform.position;
			// Adjusts to the center of the cell. 
			character.transform.Translate (new Vector3 (charRenderer.bounds.size.x / 2, -charRenderer.bounds.size.y / 2, 0));
			// Prince
			character = GameObject.Find("Prince");
			((Character)character.GetComponent (typeof(Character))).setInitialVariables (gHandler, storage);
			charRenderer = (SpriteRenderer) character.GetComponent (typeof(SpriteRenderer));
			character.transform.position = GameObject.Find ("Prince Spawn").transform.position;
			character.transform.Translate (new Vector3 (charRenderer.bounds.size.x / 2, -charRenderer.bounds.size.y / 2, 0));

			// Chef Bordeaux.
			character = GameObject.Find("Chef Bordeaux");
			((Character)character.GetComponent (typeof(Character))).setInitialVariables (gHandler, storage);
			charRenderer = (SpriteRenderer) character.GetComponent (typeof(SpriteRenderer));
			character.transform.position = GameObject.Find ("Chef Bordeaux Spawn").transform.position;
			character.transform.Translate (new Vector3 (charRenderer.bounds.size.x / 2, -charRenderer.bounds.size.y / 2, 0));

			// The Prince's Aide. 
			character = GameObject.Find("Prince's Aide");
			((Character)character.GetComponent (typeof(Character))).setInitialVariables (gHandler, storage);
			charRenderer = (SpriteRenderer) character.GetComponent (typeof(SpriteRenderer));
			character.transform.position = GameObject.Find ("Prince Aide Spawn").transform.position;
			character.transform.Translate (new Vector3 (charRenderer.bounds.size.x / 2, -charRenderer.bounds.size.y / 2, 0));

			// Slib, the slime. 
			character = GameObject.Find("Slib");
			((Character)character.GetComponent (typeof(Character))).setInitialVariables (gHandler, storage);
			charRenderer = (SpriteRenderer) character.GetComponent (typeof(SpriteRenderer));
			character.transform.position = GameObject.Find ("Slib Spawn").transform.position;
			character.transform.Translate (new Vector3 (charRenderer.bounds.size.x / 2, -charRenderer.bounds.size.y / 2, 0));

			// Guards. 
			character = GameObject.Find("Royal Guard 1");
			((Character)character.GetComponent (typeof(Character))).setInitialVariables (gHandler, storage);
			charRenderer = (SpriteRenderer) character.GetComponent (typeof(SpriteRenderer));
			character.transform.position = GameObject.Find ("Royal Guard 1 Spawn").transform.position;
			character.transform.Translate (new Vector3 (charRenderer.bounds.size.x / 2, -charRenderer.bounds.size.y / 2, 0));
			character = GameObject.Find("Guard 1");
			((Character)character.GetComponent (typeof(Character))).setInitialVariables (gHandler, storage);
			charRenderer = (SpriteRenderer) character.GetComponent (typeof(SpriteRenderer));
			character.transform.position = GameObject.Find ("Guard 1 Spawn").transform.position;
			character.transform.Translate (new Vector3 (charRenderer.bounds.size.x / 2, -charRenderer.bounds.size.y / 2, 0));
			character = GameObject.Find("Guard 2");
			((Character)character.GetComponent (typeof(Character))).setInitialVariables (gHandler, storage);
			charRenderer = (SpriteRenderer) character.GetComponent (typeof(SpriteRenderer));
			character.transform.position = GameObject.Find ("Guard 2 Spawn").transform.position;
			character.transform.Translate (new Vector3 (charRenderer.bounds.size.x / 2, -charRenderer.bounds.size.y / 2, 0));
			character = GameObject.Find("Guard 3");
			((Character)character.GetComponent (typeof(Character))).setInitialVariables (gHandler, storage);
			charRenderer = (SpriteRenderer) character.GetComponent (typeof(SpriteRenderer));
			character.transform.position = GameObject.Find ("Guard 3 Spawn").transform.position;
			character.transform.Translate (new Vector3 (charRenderer.bounds.size.x / 2, -charRenderer.bounds.size.y / 2, 0));
			character = GameObject.Find("Guard 4");
			((Character)character.GetComponent (typeof(Character))).setInitialVariables (gHandler, storage);
			charRenderer = (SpriteRenderer) character.GetComponent (typeof(SpriteRenderer));
			character.transform.position = GameObject.Find ("Guard 4 Spawn").transform.position;
			character.transform.Translate (new Vector3 (charRenderer.bounds.size.x / 2, -charRenderer.bounds.size.y / 2, 0));

			// Remy, the Treasurer. 
			character = GameObject.Find("Remy");
			((Character)character.GetComponent (typeof(Character))).setInitialVariables (gHandler, storage);
			charRenderer = (SpriteRenderer) character.GetComponent (typeof(SpriteRenderer));
			character.transform.position = GameObject.Find ("Remy Spawn").transform.position;
			character.transform.Translate (new Vector3 (charRenderer.bounds.size.x / 2, -charRenderer.bounds.size.y / 2, 0));

			// The Head Servant. 
			character = GameObject.Find("Head Servant");
			((Character)character.GetComponent (typeof(Character))).setInitialVariables (gHandler, storage);
			charRenderer = (SpriteRenderer) character.GetComponent (typeof(SpriteRenderer));
			character.transform.position = GameObject.Find ("Head Servant Spawn").transform.position;
			character.transform.Translate (new Vector3 (charRenderer.bounds.size.x / 2, -charRenderer.bounds.size.y / 2, 0));

			// Creates all the citizens. 
			for (int i = 1; i <= 13; i++) {
				character = GameObject.Find("Citizen " + i);
				((Character)character.GetComponent (typeof(Character))).setInitialVariables (gHandler, storage);
				charRenderer = (SpriteRenderer) character.GetComponent (typeof(SpriteRenderer));
				character.transform.position = GameObject.Find ("Citizen " + i + " Spawn").transform.position;
				character.transform.Translate (new Vector3 (charRenderer.bounds.size.x / 2, -charRenderer.bounds.size.y / 2, 0));
			}

			// Creates all the merchants.
			for (int i = 1; i <= 2; i++) {
				character = GameObject.Find("Merchant " + i);
				((Character)character.GetComponent (typeof(Character))).setInitialVariables (gHandler, storage);
				charRenderer = (SpriteRenderer) character.GetComponent (typeof(SpriteRenderer));
				character.transform.position = GameObject.Find ("Merchant " + i + " Spawn").transform.position;
				character.transform.Translate (new Vector3 (charRenderer.bounds.size.x / 2, -charRenderer.bounds.size.y / 2, 0));
			}
			// Creates all the priests.
			for (int i = 1; i <= 1; i++) {
				character = GameObject.Find("Priest " + i);
				((Character)character.GetComponent (typeof(Character))).setInitialVariables (gHandler, storage);
				charRenderer = (SpriteRenderer) character.GetComponent (typeof(SpriteRenderer));
				character.transform.position = GameObject.Find ("Priest " + i + " Spawn").transform.position;
				character.transform.Translate (new Vector3 (charRenderer.bounds.size.x / 2, -charRenderer.bounds.size.y / 2, 0));
			}
		}
			
		// Finds the corresponding character and begins his dialogue. Also passes the value of the 
		// character as a parameter to select the proper stage of dialgoue. 
		public void checkDialogue(GameObject character, GameHandler gHandler) {
			// Special characters.
			if (character == GameObject.Find ("Irune"))
				startDialogue (0, gHandler);
			if (character == GameObject.Find ("Princess"))
				startDialogue (1, gHandler);
			if (character == GameObject.Find ("Chef Bordeaux"))
				startDialogue (2, gHandler);
			if (character == GameObject.Find ("Remy"))
				startDialogue (3, gHandler);
			if (character == GameObject.Find ("Head Servant"))
				startDialogue (4, gHandler);
			if (character == GameObject.Find ("Prince"))
				startDialogue (5, gHandler);
			if (character == GameObject.Find ("Prince's Aide"))
				startDialogue (6, gHandler);
			if (character == GameObject.Find ("Slib"))
				startDialogue (7, gHandler);
			// Guards. 
			if (character == GameObject.Find ("Royal Guard 1"))
				startDialogue (25, gHandler);
			if (character == GameObject.Find ("Guard 1"))
				startDialogue (26, gHandler);
			if (character == GameObject.Find ("Guard 2"))
				startDialogue (27, gHandler);
			if (character == GameObject.Find ("Guard 3"))
				startDialogue (28, gHandler);
			if (character == GameObject.Find ("Guard 4"))
				startDialogue (29, gHandler);
			// Citizens. 
			if (character == GameObject.Find ("Citizen 1"))
				startDialogue (50, gHandler);
			if (character == GameObject.Find ("Citizen 2"))
				startDialogue (51, gHandler);
			if (character == GameObject.Find ("Citizen 3"))
				startDialogue (52, gHandler);
			if (character == GameObject.Find ("Citizen 4"))
				startDialogue (53, gHandler);
			if (character == GameObject.Find ("Citizen 5"))
				startDialogue (55, gHandler);
			if (character == GameObject.Find ("Citizen 6"))
				startDialogue (56, gHandler);
			if (character == GameObject.Find ("Citizen 7"))
				startDialogue (57, gHandler);
			if (character == GameObject.Find ("Citizen 8"))
				startDialogue (58, gHandler);
			if (character == GameObject.Find ("Citizen 9"))
				startDialogue (59, gHandler);
			if (character == GameObject.Find ("Citizen 10"))
				startDialogue (60, gHandler);
			if (character == GameObject.Find ("Citizen 11"))
				startDialogue (61, gHandler);
			if (character == GameObject.Find ("Citizen 12"))
				startDialogue (62, gHandler);
			if (character == GameObject.Find ("Citizen 13"))
				startDialogue (63, gHandler);
			// Merchants. 
			if (character == GameObject.Find ("Merchant 1"))
				startDialogue (90, gHandler);
			if (character == GameObject.Find ("Merchant 2"))
				startDialogue (91, gHandler);
			// Priests. 
			if (character == GameObject.Find ("Merchant 1"))
				startDialogue (110, gHandler);
		}

		private void startDialogue(int character, GameHandler gHandler) {
			// Gets Lark's sprite renderer for positional purposes.
			SpriteRenderer lRenderer = (SpriteRenderer) storage.getLark ().GetComponent (typeof(SpriteRenderer));
			// Character game object. 
			GameObject characterObject = null;
			// Sprite renderer for the character. 
			SpriteRenderer cRenderer;
			// Character script. 
			Character charScript;
			// The generic dialogue event for any dialogue fired off. 
			GenericDialogueEvent dEvent;
			// The generic item event for any items given. 
			ItemEvent iEvent;

			switch (character) {
			// Irune is the character.
			case 0:
				// Gets the Irune character. 
				characterObject = GameObject.Find ("Irune");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for Irune. 
					dEvent = new GenericDialogueEvent (gHandler, "Ho there! Where " +
					"did a creature such as yourself come from? You couldn't possibly be from below; " +
					"I know each and every face in Great Oak. Hmm... Then I suppose you are a stranger, an " +
					"anomaly of some sorts who ended up here by some ways or means. Ah, nevermind. Live and let live " +
					"I suppose. Seeing as you are here, you might as well enjoy the view from the top.");
					// Increments character stage by one for Irune. 
					storage.setStage (character, storage.getStage (character) + 1);
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				case 1: 
					// Sets the second dialogue event for Irune (second time he is spoken to).
					dEvent = new GenericDialogueEvent (gHandler, "I suppose I haven't introduced " +
					"myself. The name's Irune. I serve as a keeper or farmer of sorts. For what you " +
					"ask. Well that's simple... creatures that are vicious, cruel, and wild. Monsters " +
					"that need to be kept under control. Of course, " +
					"such things do have their uses for those willing to work with them, and I can even " +
					"loan you one if you want, but you'd have to have a valid reason for doing so. " +
					"Maybe ask the prince or some other member of the royalty for permission first.");
					// Increments character stage by one for Irune. 
					storage.setStage (character, storage.getStage (character) + 1);
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				case 2:
					// Sets the third dialogue event for Irune (third time he is spoken to).
					// The item event that bestows upon Lark the Great Oak key. 
					iEvent = new ItemEvent (gHandler, new GreatOakEntryKey ());
					dEvent = new GenericDialogueEvent (gHandler, "Oh dear. I forgot that since you had not " +
					"gotten here by normal means, you must have no feasible way to go below. " +
					"Then I suppose I must entrust you this. Please handle it carefully.", iEvent);
					// Increments character stage by one for Irune. 
					storage.setStage (character, storage.getStage (character) + 1);
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				case 3:
					// This is the fourth dialogue event for Irune, that will repeat until the story is 
					// further progressed. 
					dEvent = new GenericDialogueEvent (gHandler, "Well? What are you waiting for?");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
				// The Princess is the character. 
			case 1:
				// Gets the Princess character.
				characterObject = GameObject.Find("Princess");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for the Princess.
					dEvent = new GenericDialogueEvent (gHandler, "Hmmmm...? Are you the new master of hounds father sent for?");
					// Increments character stage by one for Princess. 
					storage.setStage (character, storage.getStage (character) + 1);
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				case 1:
					// Sets the first dialogue event for the Princess.
					dEvent = new GenericDialogueEvent (gHandler, "Oh? You're not him? In that case, I shall carry on grooming with grace.");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			case 2:
				// Gets the Chef Bordeaux character.
				characterObject = GameObject.Find("Chef Bordeaux");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for the Chef Bordeaux.
					dEvent = new GenericDialogueEvent (gHandler, "The Princess' tastes are whimsical and obscure. " +
						"She once asked for a squid tart... I wasn't even sure where to start with that one.");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			// Remy is the character.
			case 3:
				// Gets the Remy character. 
				characterObject = GameObject.Find ("Remy");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for Remy. 
					dEvent = new GenericDialogueEvent (gHandler, "Hmmm? Where did you come from? And, more importantly, " +
						"what manner of beast are you? How utterly strange.");
					// Increments character stage by one for Remy. 
					storage.setStage (character, storage.getStage (character) + 1);
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				case 1:
					// Sets the second dialogue event for Remy.
					dEvent = new GenericDialogueEvent (gHandler, "Well, seeing as you are a cordial, albeit odd creature. " +
						"I suppose I could converse with you. I am Remy, the royal banker of Great Oak. Come to me for any pecuniary " +
						"needs. And, should you require anything else, please inquire at the general information counter below.");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
				// The Head Servant is the character. 
			case 4:
				// Gets the Head Servant character.
				characterObject = GameObject.Find ("Head Servant 2-1");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for the Head Servant. 
					dEvent = new GenericDialogueEvent (gHandler, "I'm terrible sorry. I simply don't have the time to speak with " +
						"you, as the princess requires her cosmetics to be in absolute perfect order.");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			case 5:
				// Gets the Prince character.
				characterObject = GameObject.Find("Prince");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for the Prince.
					dEvent = new GenericDialogueEvent (gHandler, "I hear you're looking for a monster... err... creature keeping " +
						"license. Seeing as I am the only royal authority present to bestow upon you such an item in Great Oak, I " +
						"assume you came to ME for the license. Well, you're in luck. Not only am I kind enough to give the license " +
						"to a stranger I've never met before, I am also willing to give you a free creature to go along with it. However, " +
						"there is a test... to see whether you're worthy to represent the Prince of Great Oak. Take this useless... ahem... " +
						"inexperienced slime next to me to the portal keeper downstairs. I shall inform him that I've given you " +
						"access to a portal to the wilderness. Defeat the Master Creature that resides in the portal, and I'll give you the " +
						"license. Fail, and I'll ensure you'll never see anything but a defeat screen for the rest of your life!");
					// Increments the stage of the character.
					storage.setStage (character, 1);
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				case 1:
					// Sets the second dialogue event for the Prince.
					dEvent = new GenericDialogueEvent (gHandler, "Well? I'm waiting m'boy.");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}			
				break;
			case 6:
				// Gets the Prince's Aide.
				characterObject = GameObject.Find("Prince's Aide");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for the Prince's Aide.
					dEvent = new GenericDialogueEvent (gHandler, "They say the only reason I became the Prince's Aide is because of " +
						"my sycophantic personality. But with the royalty of Great Oak being as they are, what other way " +
						"can one hope to advance through the ranks?");
					// Increments the stage of the character.
					storage.setStage (character, 1);
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				case 1:
					// Sets the second dialogue event for the Prince.
					dEvent = new GenericDialogueEvent (gHandler, "Please talk to the Prince for any further inquiries. " +
						"He'll scold me if I say something he doesn't like.");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}			
				break;
			case 7:
				// Gets Slib.
				characterObject = GameObject.Find("Slib");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for Slib.
					dEvent = new GenericDialogueEvent (gHandler, "Uhh... Hi?");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}			
				break;
				// Royal Guard 1 is the character. 
			case 25:
				// Gets the Guard 2 character.
				characterObject = GameObject.Find("Royal Guard 1");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for Guard 3. 
					dEvent = new GenericDialogueEvent (gHandler, "Uhhh... Hello to you sir.");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			// Guard 1 is the character. 
			case 26:
				// Gets the Guard 1 character.
				characterObject = GameObject.Find("Guard 1");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for Guard 1. 
					dEvent = new GenericDialogueEvent (gHandler, "What creature are you? I don't like the look of you...");
					// Increments character stage by one for Guard 1. 
					storage.setStage (character, storage.getStage (character) + 1);
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				case 1:
					// Sets the second dialogue event for Guard 1.
					dEvent = new GenericDialogueEvent (gHandler, "Whatever you do, behave.");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			// Guard 2 is the character. 
			case 27:
				// Gets the Guard 2 character.
				characterObject = GameObject.Find("Guard 2");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for Guard 2. 
					dEvent = new GenericDialogueEvent (gHandler, "Carry on.");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			// Guard 3 is the character. 
			case 28:
				// Gets the Guard 3 character.
				characterObject = GameObject.Find("Guard 3");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for Guard 3. 
					dEvent = new GenericDialogueEvent (gHandler, "The Prince's home is just beyond here. They say he has a winning " +
						"personality.");
					// Increments character stage by one for Guard 3. 
					storage.setStage (character, storage.getStage (character) + 1);
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				case 1:
					// Sets the second dialogue event for Guard 3.
					dEvent = new GenericDialogueEvent (gHandler, "Remember not to irritate him. They say the last one who " +
						"angered the Prince was thrown into a vat of pidgeon droppings. Nasty stuff.");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			// Guard 4 is the character. 
			case 29:
				// Gets the Guard 4 character.
				characterObject = GameObject.Find("Guard 4");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for Guard 4. 
					dEvent = new GenericDialogueEvent (gHandler, "The Prince sure is a bore to guard. Unlike other members " +
						"of the royal family, he rarely leaves his home.");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			case 50:
				// Gets the Citizen 1 character.
				characterObject = GameObject.Find("Citizen 1");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for the Citizen 1.
					dEvent = new GenericDialogueEvent (gHandler, "Great Oak is a huge tree. We're so high up, you can " +
						"hardly even see the bottom from here. Speaking of which, nobody I know has ever actually reached " +
						"the lowest level of Great Oak. How strange.");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			case 51:
				// Gets the Citizen 2 character.
				characterObject = GameObject.Find("Citizen 2");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for the Citizen 2.
					dEvent = new GenericDialogueEvent (gHandler, "Hmmm... You have the look of a keeper. A creature keeper " +
						"that is. But I see that you don't have a license quite yet. Only a member of royalty can give those, " +
						"and they're all a bunch of snobs, so good luck.");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			case 52:
				// Gets the Citizen 3 character.
				characterObject = GameObject.Find("Citizen 3");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for the Citizen 3.
					dEvent = new GenericDialogueEvent (gHandler, "I'm suppose to be here advertising the item shop next door, but I " +
						"despise the owner (he's a bit of a weirdo), so do what you want!");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			case 53:
				// Gets the Citizen 4 character.
				characterObject = GameObject.Find("Citizen 4");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for the Citizen 4.
					dEvent = new GenericDialogueEvent (gHandler, "They say that the best way to train creatures is to " +
						"take them out in the wilderness and fight wild ones. But here in Great Oak, nobody's ever been " +
						"to the wilderness so I don't know where they get their info from.");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			case 54:
				// Gets the Citizen 5 character.
				characterObject = GameObject.Find("Citizen 5");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for the Citizen 5.
					dEvent = new GenericDialogueEvent (gHandler, "If you were to ever venture outside of Great Oak, " +
						"please stock up and head to the item shop. In fact, " +
						"I don't really think it even gets customers other than the rare traveller. It's a wonder it even " +
						"survives with rising rental costs, and the increase in manufacturing prices... " +
						"Regardless, buying items like potions, repels, and most importantly amulets could really " +
						"save you from being wiped out and sent back here.");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			case 55:
				// Gets the Citizen 6 character.
				characterObject = GameObject.Find("Citizen 6");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for the Citizen 6.
					dEvent = new GenericDialogueEvent (gHandler, "This idiot here insists the beasts are the best. He's " +
						"living proof that they're not. Help me prove to him that bugs are better! Find me a strong " +
						"bug creature.");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			case 56:
				// Gets the Citizen 7 character.
				characterObject = GameObject.Find("Citizen 7");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for the Citizen 7.
					dEvent = new GenericDialogueEvent (gHandler, "My friend here needs some enlightenment. Beasts are way, " +
						"way, WAY better than bugs. Well stranger, let's show him who's right. Bring me a fellow beast. A " +
						"powerful one.");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			case 57:
				// Gets the Citizen 8 character.
				characterObject = GameObject.Find("Citizen 8");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for the Citizen 8.
					dEvent = new GenericDialogueEvent (gHandler, "Those two creatures are ALWAYS arguing about something. " +
						"In truth, neither beasts nor bugs may best one another. ALL creature families are separated only" +
						"by quirks. Take these two for example: bugs are especially vulnerable to physical damage but " +
						"very resistant to chemicals and magic, while beasts have little magic resistance but great vitality.");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			case 58:
				// Gets the Citizen 9 character.
				characterObject = GameObject.Find("Citizen 9");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for the Citizen 9.
					dEvent = new GenericDialogueEvent (gHandler, "Nearby is the Cathedral of the Holy Goddess dearie. " +
						"There you can pray for respite, recuperation, and resurrection. My father used to refer to them as " +
						"the three R's. Of course, due to my good conduct, I've never had to visit once in my life.");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			case 59:
				// Gets the Citizen 10 character.
				characterObject = GameObject.Find("Citizen 10");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for the Citizen 10.
					dEvent = new GenericDialogueEvent (gHandler, "People say I was designed weird, but what am I to do " +
						"about that? Anyway, the looks of a person don't tell it all. Take the slime creature family for " +
						"example. They may look cute, but they pack a punch!");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			case 60:
				// Gets the Citizen 11 character.
				characterObject = GameObject.Find("Citizen 11");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for the Citizen 11.
					dEvent = new GenericDialogueEvent (gHandler, "I may be just a kid. But I know a lot. Just try me.");
					// Increments character stage by one for Citizen 11. 
					storage.setStage (character, storage.getStage (character) + 1);
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				case 1:
					// Sets the second dialogue event for the Citizen 11.
					dEvent = new GenericDialogueEvent (gHandler, "If you breed together one Cyclops and one Slib, you'll get " +
						"yourself a Daemonslime.");
					// Increments character stage by one for Citizen 11. 
					storage.setStage (character, storage.getStage (character) + 1);
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				case 2:
					// Sets the third dialogue event for the Citizen 11.
					dEvent = new GenericDialogueEvent (gHandler, "Breeding together a Gorgon and Reflector produces some odd, but " +
						"useful results.");
					// Increments character stage by one for Citizen 11. 
					storage.setStage (character, storage.getStage (character) + 1);
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				case 3:
					// Sets the fourth dialogue event for the Citizen 11.
					dEvent = new GenericDialogueEvent (gHandler, "Should you ever find yourself lost in a maze of mirrors. " +
						"Go backwards and forwards again and again.");
					// Increments character stage by one for Citizen 11. 
					storage.setStage (character, storage.getStage (character) + 1);
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				case 4:
					// Sets the fifth dialogue event for the Citizen 11.
					dEvent = new GenericDialogueEvent (gHandler, "When the Prince is anxious, the only thing that will " +
						"calm him down is his stuffed teddy bear. Where it is? I don't know...");
					// Increments character stage by one for Citizen 11. 
					storage.setStage (character, storage.getStage (character) + 1);
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				case 5:
					// Sets the sixth dialogue event for the Citizen 11.
					dEvent = new GenericDialogueEvent (gHandler, "When in doubt, consult the Dragon Almanac. Consult me? " +
						"I don't know EVERYTHING.");
					// Increments character stage by one for Citizen 11. 
					storage.setStage (character, storage.getStage (character) + 1);
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				case 6:
					// Sets the seventh dialogue event for the Citizen 11.
					dEvent = new GenericDialogueEvent (gHandler, "There's a secret trick to gambling. But don't tell anyone else. " +
						"If you speak to the dicemaster more than 10 times, he'll get so irate that he'll fight you. " +
						"Win, and you win! Get it?");
					// Increments character stage by one for Citizen 11. 
					storage.setStage (character, storage.getStage (character) + 1);
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				case 7:
					// Sets the eighth dialogue event for the Citizen 11.
					dEvent = new GenericDialogueEvent (gHandler, "That's all for now, folks.");
					// Resets stage back to 1. 
					storage.setStage (character, 1);
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			case 61:
				// Gets the Citizen 12 character.
				characterObject = GameObject.Find ("Citizen 12");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for the Citizen 12.
					dEvent = new GenericDialogueEvent (gHandler, "It's quite lonely here in my house, all by myself. I'd " +
						"sure love a creature to keep me company. Something comely and muscular. Are you getting the " +
						"hint yet honey?");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			case 62:
				// Gets the Citizen 13 character.
				characterObject = GameObject.Find("Citizen 13");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Sets the first dialogue event for the Citizen 13.
					dEvent = new GenericDialogueEvent (gHandler, "Take a look at my garden stranger! Isn't it a beautiful " +
						"thing to behold. So much so, in fact, that it must be the reason you came into my house " +
						"uninvited and unprompted. Well?");
					// Increments the stage of the character.
					storage.setStage (character, 1);
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				case 1:
					// Sets the second dialogue event for the Citizen 13.
					dEvent = new GenericDialogueEvent (gHandler, "But, my garden could surely look even more astounding " +
						"were you to fetch be a rainbow hibiscus. It would even make up for your act of trespassing on my house.");
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}			
				break;
			case 90:
				// Gets the Merchant 1 character.
				characterObject = GameObject.Find("Merchant 1");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Create the shop event for Merchant 1. 
					List<Item> itemList = new List<Item>();
					itemList.Add(new Herb());
					itemList.Add(new RoseWater());
					itemList.Add(new Ether());
					itemList.Add(new Antidote());
					itemList.Add(new Warp_Wing());
					itemList.Add(new Repel());
					itemList.Add(new Beef_Jerky());
					ShopEvent sEvent = new ShopEvent (gHandler, itemList); 
					// Sets the dialogue event for the Merchant 1.
					dEvent = new GenericDialogueEvent (gHandler, "Hello! Welcome to my shop.", sEvent);
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			case 91:
				// Gets the Merchant 2 character.
				characterObject = GameObject.Find("Merchant 2");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Create the shop event for Merchant 2. 
					List<Item> itemList = new List<Item>();
					itemList.Add(new Buckler());
					itemList.Add(new Cotton_Robe());
					itemList.Add(new Running_Shoes());
					itemList.Add(new Fire_Talisman());
					itemList.Add(new Ice_Talisman());
					itemList.Add(new Thunder_Talisman());
					itemList.Add(new Water_Talisman());
					itemList.Add(new Healing_Staff());
					itemList.Add(new Shield_Staff());
					ShopEvent sEvent = new ShopEvent (gHandler, itemList); 
					// Sets the dialogue event for the Merchant 2.
					dEvent = new GenericDialogueEvent (gHandler, "Here to buy some talismans and staffs I assume?.", sEvent);
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			case 110:
				// Gets the Priest 1 character.
				characterObject = GameObject.Find("Priest 1");
				// Sets the dialogue in accordance with the stage of the character.
				switch (storage.getStage (character)) {
				case 0:
					// Create the praying event for Priest 1. 
					List<Item> itemList = new List<Item>();
					itemList.Add(new Buckler());
					itemList.Add(new Cotton_Robe());
					itemList.Add(new Running_Shoes());
					itemList.Add(new Fire_Talisman());
					itemList.Add(new Ice_Talisman());
					itemList.Add(new Thunder_Talisman());
					itemList.Add(new Water_Talisman());
					itemList.Add(new Healing_Staff());
					itemList.Add(new Shield_Staff());
					ShopEvent sEvent = new ShopEvent (gHandler, itemList); 
					// Sets the dialogue event for the Priest 1.
					dEvent = new GenericDialogueEvent (gHandler, "Come to pay your respect to the almighty Goddess?", sEvent);
					// Begins the dialogue event.
					gHandler.setCurrentEvent (dEvent);
					dEvent.begin ();
					break;
				}
				break;
			}

			// Gets the character script of the character.
			charScript = (Character) characterObject.GetComponent (typeof(Character));
			// If the character DOES move, return as we don't want to change his/her trajectory.
			if (charScript.doesMove ())
				return;
			// ELSE continue. 
			// Gets the sprite renderer of the character.
			cRenderer = (SpriteRenderer) characterObject.GetComponent (typeof(SpriteRenderer));

			// Lark is to the right of the character: set right. 
			if (lRenderer.bounds.center.x > cRenderer.bounds.max.x)
				charScript.faceDirection (1);
			// Lark is to the left of the character: set left.
			if (lRenderer.bounds.center.x < cRenderer.bounds.min.x)
				charScript.faceDirection (-1);
			// Lark is at the top of the character: set up. 
			if (lRenderer.bounds.center.y > cRenderer.bounds.max.y)
				charScript.faceDirection (2);
			// Lark is at the bottom of the character: set down.
			if (lRenderer.bounds.center.y < cRenderer.bounds.min.y)
				charScript.faceDirection (-2);
		}
	}
}

