using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyCSharp
{
	public class ConfirmationScreen : GameScreen
	{
		// Game Screen that launched this one. 
		GameScreen launcher; 

		// Key that defines this confirmation's function. 
		string key;

		public ConfirmationScreen ()
		{

		}

		void Start() {
			// Sets the indicator and its sprite renderer.
			indicator = GameObject.Find ("Confirmation Indicator");
			iSpriteRenderer = (SpriteRenderer)indicator.GetComponent (typeof(SpriteRenderer));
		}

		// Tells the confirmation screen that it is ACTIVE. Gets the screen that launched this one, and transports it
		// to the given coordinates. 
		public void activate(GameScreen launcher, string key, float coordX, float coordY) {
			position = 0;
			this.launcher = launcher;
			this.key = key;
			this.gameObject.transform.position = new Vector3 (coordX, coordY, 0); 
			moveIndicator ();
		}

		// If on a different screen, don't update.
		void Update() {
			// If not the confirmation screen, return.
			if (screen != 6)
				return;
			// Else process. 
			checkInput();
			// Adds delta time and checks for blink.
			deltaTime += Time.deltaTime;
			// Checks for blink of indicator.
			checkBlink ();
		}

		private void checkInput() {
			// Goes up/down.
			if (Input.GetKeyDown (KeyCode.W))
				checkMovement (2);
			if (Input.GetKeyDown (KeyCode.S))
				checkMovement (-2);
			// Makes a decision based on the position of the cursor. 
			if (Input.GetKeyDown (KeyCode.F))
				makeDecision ();
		}

		// Does an action in accordance with the position.
		private void makeDecision() {
			switch (position) {
			// At OK, return proceed.
			case 0:
				launcher.getInput (true, key);
				break;
			// At "Cancel", return do NOT proceed. 
			case 1:
				launcher.getInput (false, key);
				break;
			}
			// Regardless of decision, moves the screen so that it isn't seen. 
			this.gameObject.transform.position = new Vector3 (-50f, -50f, 0);
		}

		// Checks for movement in accordance with the direction given (i.e. S is down, W is up, etc.)
		private void checkMovement(int direction) {
			// Indicator is on "OK", and down is pressed. Goes to "Cancel".
			if (position == 0 && direction == -2)
				position = 1;
			// Indicator is on "Cancel", and up is pressed. Goes to "OK".
			if (position == 1 && direction == 2)
				position = 0;

			// Move the indicator accordingly. 
			moveIndicator ();
		}

		// Moves the indicator according to the position. 
		private void moveIndicator() {
			switch (position) {
			// On OK, move to the top text.
			case 0:
				indicator.transform.localPosition = new Vector3 (-0.165f, 0.038f, 0);
				break;
			// On Cancel, move to the bottom text.
			case 1:
				indicator.transform.localPosition = new Vector3 (-0.165f, -0.032f, 0);
				break;
			}
		}
	}
}

