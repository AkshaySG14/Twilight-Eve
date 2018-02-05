using UnityEngine;
using System.Collections;
using System;

namespace AssemblyCSharp
{
	// This is the class that handles all of the occurrences in the game, including cutscenes, spawning, 
	// enemy placement, NPC placement, etc. 
	public class GameHandler : MonoBehaviour
	{
		// The storage of the game.
		private Storage storage;
		// Lark, the player controlled character. 
		private GameObject lark;
		// The script that handles Lark's events. 
		private Lark larkScript;
		// The script that handles the pause screen.
		private PauseScreen pScreen;
		// The script that handles the item screen.
		// The camera for the game. 
		private GameObject gameCamera;
		// The ongoing event. This is for input purposes.
		private GameEvent currentEvent;
		// The ongoing dialogue. Also for input purposes. 
		private DialogueHandler dHandler;
		// The character handler responsible for handling the dialogue of all the characters. 
		private CharacterHandler cHandler;
		// The portal handler responsible for handling the instant teleportation of Lark. 
		private PortalHandler pHandler;
		// If the game is on cooldown, cannot pause or unpause. 
		private bool cooldown = false;
		// Whether the game is paused or not. 
		public bool paused = false;

		public GameHandler ()
		{

		}

		// The beginning of the game, should there be no game save. 
		void Start() {
			// Creates the storage class and starts a new game.
			storage = new Storage(this);
			storage.newGame ();
			// Gets Lark and moves him to his spawn point.
			lark = storage.getLark();
			// Gets Lark's script. 
			larkScript = storage.getLarkScript();
			// Gets the pause screen script.
			pScreen = storage.getPauseScreen();
			// Sets storage and handler for Lark. 
			larkScript.setStorageHandler(this, storage);
			// Same for the character handler. 
			cHandler = (CharacterHandler) GameObject.Find("Characters").GetComponent(typeof(CharacterHandler));
			cHandler.setInitialVariables (this, storage);
			// Same for the portal handler. 
			pHandler = (PortalHandler) GameObject.Find("Portals").GetComponent(typeof(PortalHandler));
			pHandler.initialize (this);
			// Gets the camera for the game. 
			gameCamera = GameObject.Find("Main Camera");
			// Instantiates and starts the starting event.
			StartingEvent sEvent = new StartingEvent(this);
			currentEvent = sEvent;
			sEvent.begin ();
		}

		void Update() {
			// If there are no events going on, check for cell change. 
			if (currentEvent == null)
				checkCellChange ();
		}

		// Updates camera to view current cell.
		public void updateCamera() {
			// Updates the Unity in-game position of the cell. This is the cell number * 16
			// (tile width/height) * 12 (number of tiles per cell in both directions) / 100
			// (pixels per unit) + half the camera width (1.92 / 2).
			gameCamera.transform.position = new Vector3 (storage.getCellX() * 1.92f + 0.96f,
				storage.getCellY() * 1.92f - 0.96f, -10);
		}

		public void translateCamera() {
			// Stuns and freezes Lark until the panning is done. 
			larkScript.stun();
			larkScript.freeze ();
			// Gets the difference between the new cellX and the old cellX using the camera's current position.
			int diffCellX = storage.getCellX() - (int) ((gameCamera.transform.position.x) / 1.92f);
			// Gets the distance in Unity terms.
			float distX = diffCellX * 1.92f;
			// Same but for the cellY. 
			int diffCellY = storage.getCellY() - (int) ((gameCamera.transform.position.y) / 1.92f);
			float distY = diffCellY * 1.92f;
			// Uses a for loop to pan the camera over a period of time. Uses one tenth of the difference 
			// for each set of cells to displace the camera. 
			for (int i = 0; i < 100; i++)
				StartCoroutine(panCamera (distX / 100f, distY / 100f, i));
		}

		// Checks if the Lark has moved out of the current cell. 
		private void checkCellChange() {
			// Checks for X cell increase.
			if (lark.transform.position.x > (storage.getCellX () + 1) * 1.92f) {
				storage.setCells (storage.getCellX () + 1, storage.getCellY ());
				translateCamera ();
			}
			// Checks for X cell decrease.
			if (lark.transform.position.x < (storage.getCellX ()) * 1.92f) {
				storage.setCells (storage.getCellX () - 1, storage.getCellY ());
				translateCamera ();
			}
			// Checks for Y cell increase.
			if (lark.transform.position.y > (storage.getCellY ()) * 1.92f) {
				storage.setCells (storage.getCellX (), storage.getCellY () + 1);
				translateCamera ();
			}
			// Checks for Y cell decrease.
			if (lark.transform.position.y < (storage.getCellY () - 1) * 1.92f) {
				storage.setCells (storage.getCellX (), storage.getCellY () - 1);
				translateCamera ();
			}
		}

		// Pauses the game by moving the pause screen to the center of the screen and pausing all characters 
		// in the game.
		public void pauseGame() {
			// If on cooldown, return.
			if (cooldown)
				return;
			
			GameObject.Find ("Pause Screen").transform.position = new Vector3 (storage.getCellX () * 1.92f + 0.96f, 
				storage.getCellY () * 1.92f - 0.96f, 0);
			larkScript.pause ();
			pScreen.pause ();
			// Sets cooldown to true and resets it after 0.25 seconds.
			cooldown = true;
			StartCoroutine (coolDown ());
			// Sets paused to true. 
			paused = true;
		}

		// Unpauses the game by moving the pause screen away from the screen and unpausing all characters 
		// in the game. 
		public void unpauseGame() {
			// If on cooldown, return. 
			if (cooldown)
				return; 
			GameObject.Find ("Pause Screen").transform.position = new Vector3 (-50, -50, 0);
			larkScript.unpause();
			pScreen.unpause ();
			// Sets Lark's animation to disabled. 
			Animator anim = lark.GetComponent<Animator>();
			anim.enabled = false;
			// Sets cooldown to true and resets it after 0.25 seconds.
			cooldown = true;
			StartCoroutine (coolDown ());
			// Sets paused to false. 
			paused = false;
		}

		// Pans the camera to view the current cell. 
		private IEnumerator panCamera(float x, float y, float time) {
			// Waits 0.1 * the number of times the loop has looped seconds, then translates the camera 
			// position by a very slight amount towards the end goal. This is to simulate panning.
			yield return new WaitForSeconds(0.005f * time);
			gameCamera.transform.Translate (new Vector3 (x, y, 0));
			// If on the final iteration, unstun Lark. 
			if (time == 99) {
				larkScript.unStun ();
				larkScript.unFreeze ();
				// Sets Lark's animation to disabled. 
				Animator anim = lark.GetComponent<Animator>();
				anim.enabled = false;
			}
		}

		// Sets cooldown to false after 0.25 seconds. 
		private IEnumerator coolDown() {
			yield return new WaitForSeconds (0.25f);
			cooldown = false;
		}

		// Shakes the screen. Used in openings and other Earth-moving events. 
		public void shakeScreen(GameEvent gEvent) {
			// Does the first shake, which is of a smaller magnitude than the other shakes. 
			StartCoroutine(shake(0.1f, 1, 0)); 
			// The left shakes. 
			for (float i = 0.1f; i < 1f; i += 0.2f)
				StartCoroutine(shake(0.2f, -1, i));
			// The right shakes.
			for (float i = 0.2f; i < 1f; i += 0.2f)
				StartCoroutine(shake(0.2f, 1, i));
			// The last shake to return to normalcy. 
			StartCoroutine(shake(0.1f, 1, 1f));
			// Event proceeds after shaking is done. 
			StartCoroutine(proceedEvent(gEvent, 1.25f));
		}

		// Shakes the screen in time increments. 
		private IEnumerator shake(float magnitude, int dir, float time) {
			yield return new WaitForSeconds (time);
			gameCamera.transform.Translate (new Vector3 (magnitude * dir, 0, 0));
		}

		// Proceeds event after done shaking.
		// Shakes the screen in time increments. 
		private IEnumerator proceedEvent(GameEvent gEvent, float time) {
			yield return new WaitForSeconds (time);
			gEvent.proceed ();
		}
			
		// Sets the dialogue handler for input. 
		public void setDialogueHandler(DialogueHandler dHandler) {
			this.dHandler = dHandler;
		}

		// Sets the current event. 
		public void setCurrentEvent(GameEvent gEvent) {
			currentEvent = gEvent;
		}

		// Prints any debugging messages for other classes, as this class is a child class of MonoBehavior.
		public void printDebug(object obj) {
			print (obj);
		}

		public void checkEvents() {
			if (dHandler != null)
				dHandler.incrementLine ();
		}
			
		public Storage getStorage() {
			return storage;
		}

		public GameObject getLark() {
			return lark;
		}

		public GameEvent getCurrentEvent() {
			return currentEvent;
		}
	}
}