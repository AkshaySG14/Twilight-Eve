using System;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyCSharp
{
	public class PauseScreen : GameScreen
	{
		public PauseScreen ()
		{

		}

		void Start() {
			// Sets the indicator and its sprite renderer.
			indicator = GameObject.Find ("Pause Indicator");
			iSpriteRenderer = (SpriteRenderer) indicator.GetComponent (typeof(SpriteRenderer));
			// Moves indicator. 
			moveIndicator();
		}

		void Update() {
			// If unpaused or on a different screen, return.
			if (!paused || screen != 0)
				return;
			// Else process. 
			checkInput();
			// Adds delta time and checks for blink.
			deltaTime += Time.deltaTime;
			// Checks for blink of indicator.
			checkBlink ();
		}

		private void checkInput() {
			// Unpauses the game if the Z button is pressed and off cooldown. 
			if (Input.GetKeyDown (KeyCode.Z) && !cooldown) {
				reset ();
				GameScreen.gHandler.unpauseGame ();
			}
			// Goes up/down.
			if (Input.GetKeyDown (KeyCode.W))
				checkMovement (1);
			if (Input.GetKeyDown (KeyCode.S))
				checkMovement (-1);
			if (Input.GetKeyDown (KeyCode.F))
				checkDecision ();
		}

		// F button was pressed, execute a decision depending on where the indicator is located.
		private void checkDecision() {
			switch (position) {
			// Monsters, go to monster screen.
			case 0:
				// If Lark has no monsters, does NOT proceed to the monster screen.
				if (storage.getMonsters() == 0)
					return;
				screen = 1;
				break;
				// Items, go to item screen.
			case 1:
				screen = 2;
				// Moves inventory screen to the pause screen's current position, then moves pause screen out of the way.
				GameObject.Find ("Item Screen").transform.position = new Vector3 (this.gameObject.transform.position.x, 
					this.gameObject.transform.position.y, 0);
				// Sets the variables of the item screen. 
				((InventoryScreen)GameObject.Find ("Item Screen").GetComponent (typeof(InventoryScreen))).setVariables ();
				this.gameObject.transform.position = new Vector3 (-50f, -50f, 0);
				break;
				// Skills, go to skill screen.
			case 2:
				// Same as the above case for the monsters screen. 
				if (storage.getMonsters() == 0)
					return;
				screen = 3;
				break;
				// Portal keys, go to portal key screen.
			case 3:
				screen = 4;
				// Moves portal key screen to the pause screen's current position, then moves pause screen out of the way.
				GameObject.Find ("Portal Key Screen").transform.position = new Vector3 (this.gameObject.transform.position.x, 
					this.gameObject.transform.position.y, 0);
				// Sets the variables of the item screen. 
				((PortalKeyScreen)GameObject.Find ("Portal Key Screen").GetComponent (typeof(PortalKeyScreen))).setVariables ();
				this.gameObject.transform.position = new Vector3 (-50f, -50f, 0);
				break;
			}
		}

		// Checks for movement in accordance with the direction given (i.e. S is down, W is up, etc.)
		private void checkMovement(int direction) {
			// Moves position up (-1) or down (1).
			switch (direction) {
			case 1: 
				position--; 
				break;
			case -1:
				position++;
				break;
			}
			// Ensure no overflow.
			if (position < 0)
				position = 0;
			if (position > 3)
				position = 3;
			// Move the indicator accordingly. 
			moveIndicator ();
		}

		// Moves the indicator according to the position. 
		public void moveIndicator() {
			switch (position) {
			// The first position (topmost).
			case 0:
				indicator.transform.localPosition = new Vector3 (-0.325f, 0.18f, 0f);
				break;
				// The second postion (second from the top).
			case 1:
				indicator.transform.localPosition = new Vector3 (-0.325f, 0.07f, 0f);
				break;
				// Etc.
			case 2:
				indicator.transform.localPosition = new Vector3 (-0.325f, -0.05f, 0f);
				break;
			case 3:
				indicator.transform.localPosition = new Vector3 (-0.325f, -0.16f, 0f);
				break;
			}
		}

		// Resets the position when going back to the game.
		private void reset() {
			position = 0;
			moveIndicator ();
		}

		// Overrides super method to set gold counter. 
		public override void pause() {
			// Sets paused to true. 
			paused = true;

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
		}
	}
}

