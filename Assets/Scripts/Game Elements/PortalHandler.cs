using UnityEngine;
using System.Collections;
using System;

namespace AssemblyCSharp
{
	// The class responsible for handling the portals of the game (i.e. stairs, exits, entrances). 
	public class PortalHandler : MonoBehaviour
	{
		private GameObject lark;

		private Lark lScript;

		private GameHandler gHandler;

		private Storage storage; 

		private Mask mask;

		public PortalHandler ()
		{

		}

		public void initialize(GameHandler gHandler) {
			this.gHandler = gHandler;
			this.lark = gHandler.getLark ();
			this.storage = gHandler.getStorage ();
			// Gets the game mask. 
			mask = (Mask) GameObject.Find("Mask").GetComponent(typeof(Mask));
			// Gets Lark's script. 
			lScript = (Lark) lark.GetComponent(typeof(Lark));
		}

		void Update() {
			if (!gHandler.paused)
				checkPortals ();
		}

		// Checks if Lark's center is in any bounds of any portal (stairs, entrances, etc.).
		public void checkPortals() {
			// Gets the center of the sprite renderer of Lark. 
			Vector3 lCenter = ((SpriteRenderer) lark.GetComponent(typeof(SpriteRenderer))).bounds.center;

			// Searches the game objects in stairs. If one of them contains Lark's center, make him teleport to the 
			// corresponding set of stairs. 
			foreach (GameObject stairs in GameObject.FindGameObjectsWithTag("Stairs")) {
				Vector3 sPos = stairs.transform.position;
				// If Lark's center is WITHIN the bounds of the stairs (to the right of the x, to the left of the x + 
				// width, to the top of y, and to the bottom of y + height).
				if (lCenter.x > sPos.x && lCenter.x < sPos.x + 0.16f && lCenter.y > sPos.y - 0.16f && lCenter.y < sPos.y)
					checkStairs (stairs);
			}

			// Same but for entrances and exits (E&E).
			foreach (GameObject e in GameObject.FindGameObjectsWithTag("E&E")) {
				Vector3 ePos = e.transform.position;
				if (lCenter.x > ePos.x && lCenter.x < ePos.x + 0.32f && lCenter.y > ePos.y - 0.16f && lCenter.y < ePos.y)
					checkEandE (e);
			}
		}

		// Based on the name of the stairs, transports Lark. 
		private void checkStairs(GameObject stairs) {
			// Repositions and sets mask to opaque first. 
			mask.gameObject.transform.position = new Vector3((float) storage.getCellX() * 1.92f + 0.96f, 
				(float) storage.getCellY() * 1.92f - 0.96f, 0f);
			mask.setAlpha(1f);

			// If stairs 1, transports to stairs 2. 
			if (stairs.name.Equals ("Stairs 1")) {
				// Transports Lark to a position below stairs 2, sets the cells accordingly, and updates the camera.
				lark.transform.position = new Vector3 (GameObject.Find ("Stairs 2").transform.position.x + 0.08f, 
					GameObject.Find ("Stairs 2").transform.position.y - 0.16f, 0);
				storage.setCells ((int) (lark.transform.position.x / 1.92f), (int) (lark.transform.position.y / 1.92f));
				gHandler.updateCamera ();
			}

			// If stairs 2, transports to stairs 1. 
			if (stairs.name.Equals ("Stairs 2")) {
				lark.transform.position = new Vector3 (GameObject.Find ("Stairs 1").transform.position.x + 0.08f, 
					GameObject.Find ("Stairs 1").transform.position.y, 0);
				storage.setCells ((int) (lark.transform.position.x / 1.92f), (int) (lark.transform.position.y / 1.92f));
				gHandler.updateCamera ();
			}
					
			// Fogs in. 
			transition();
		}

		// Based on the name of the entrance/exit, transports Lark. 
		private void checkEandE(GameObject e) {
			// Repositions and sets mask to opaque first. 
			mask.gameObject.transform.position = new Vector3 ((float)storage.getCellX () * 1.92f + 0.96f, 
				(float)storage.getCellY () * 1.92f - 0.96f, 0f);
			mask.setAlpha (1f);

			// Iterates through all entrances and exits and sets their correspondonces.
			for (int i = 1; i <= 7; i++) {
				if (e.name.Equals ("Exit " + i.ToString ())) {
					lark.transform.position = new Vector3 (GameObject.Find ("Entrance " + i.ToString ()).transform.position.x + 0.16f, 
						GameObject.Find ("Entrance " + i.ToString ()).transform.position.y - 0.16f, 0);
					storage.setCells ((int)(lark.transform.position.x / 1.92f), (int)(lark.transform.position.y / 1.92f));
					gHandler.updateCamera ();
					break;
				} else if (e.name.Equals ("Entrance " + i.ToString ())) {
					lark.transform.position = new Vector3 (GameObject.Find ("Exit " + i.ToString ()).transform.position.x + 0.16f, 
						GameObject.Find ("Exit " + i.ToString ()).transform.position.y, 0);
					storage.setCells ((int)(lark.transform.position.x / 1.92f), (int)(lark.transform.position.y / 1.92f));
					gHandler.updateCamera ();
					break;
				}
			}

			// Fogs in. 
			transition ();
		}

		// Fades in to simulate a change in scenery. 
		private void transition() {
			// Fades into the scene by repositioning the mask first, then making it fade in.
			mask.gameObject.transform.position = new Vector3((float) storage.getCellX() * 1.92f + 0.96f, 
				(float) storage.getCellY() * 1.92f - 0.96f, 0f);
			mask.fade(false, 0.5f);
		}
	}
}

