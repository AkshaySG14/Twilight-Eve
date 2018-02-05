using UnityEngine;
using System.Collections;
using System;

namespace AssemblyCSharp
{
	public class GameScreen : MonoBehaviour
	{
		// The game handler. 
		protected static GameHandler gHandler;

		// The storage. 
		protected static Storage storage;

		// The indicator used to show where the player is hovering. 
		protected GameObject indicator;

		// The sprite renderer for the indicator. 
		protected SpriteRenderer iSpriteRenderer;

		// The position of the indicator, and the corresponding action taken when the play presses "F". 
		protected int position = 0;

		// The amount of time in between updates.
		protected float deltaTime = 0;

		// Whether the game is paused or not. 
		protected static bool paused = false;

		// Which screen is being used in the pause screen (general, monsters, inventory, skill, portal key, shop screen, 
		// confirmation screen.)
		protected static int screen = 0;

		// Cooldown for screen change. 
		protected static bool cooldown = false;

		public GameScreen ()
		{
			
		}

		void Start() {
			// Sets game handler and storage.
			gHandler = (GameHandler) GameObject.Find("Game Handler").GetComponent(typeof(GameHandler));
			storage = gHandler.getStorage ();
		}

		// Checks if the transparent time is correct to make the indicator blink. 
		protected void checkBlink() {
			// Every 5 time instances, checks for the blink.
			if (deltaTime > 0.5f) {
				deltaTime = 0;
				// If currently transparent set opaque. 
				if (iSpriteRenderer.color.a == 0)
					iSpriteRenderer.color = new Color (1f, 1f, 1f, 1f);
				// Else if currently opaque set transparent.
				else 
					iSpriteRenderer.color = new Color (1f, 1f, 1f, 0f);
			}
		}

		// Sets paused to true. 
		public virtual void pause() {
			paused = true;
		}

		// Sets paused to false.
		public void unpause() {
			paused = false;
		}

		// Acts based on input from confirmation window. Meant to be overidden.
		public virtual void getInput(bool input, string key) {
			
		}
			
		// Sets cooldown to false after 0.25 seconds. 
		protected IEnumerator coolDown() {
			yield return new WaitForSeconds (0.25f);
			cooldown = false;
		}

	}
}

