using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{
	public class Lark : Character {
		// Use this for initialization
		void Start () {
			// Gets the animator for the player as per the Unix Engine.
			anim = GetComponent<Animator>();
		}

		// Update is called once per frame. Used to move Lark and refresh his animation. 
		void Update () {
			// If paused, return immediately. 
			if (paused)
				return;
			checkInput ();
			if (!frozen)
				setAnimation ();
			// Checks for any effects on Lark.
			checkEffects ();
		}

		private void checkInput() {
			// If any movement keys are entered, move. If stunned, does NOT move.
			if ((Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S) ||
				Input.GetKey (KeyCode.D)) && !stunned)
				Move ();
			// If the F key pressed, checks for any interaction including entering conversations or 
			// proceeding events. 
			if (Input.GetKeyDown (KeyCode.F))
				checkInteraction ();
			if (Input.GetKeyDown (KeyCode.Z) && !stunned)
				gHandler.pauseGame ();
		}

		// The method responsible for movement. Note that if stunned, the method won't activate. 
		private void Move() {
			// Moves the player by the speed given.
			// Right. Note the player cannot press A and D simutaneously, or W and S simutaneously. If either 
			// conditions occur, the player will not move. Also, all movement is affected by negative modifiers (i.e. 
			// slowing effects or platform effects).
			if (Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A) &&
			    !(Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.S))) {
				transform.Translate (Vector2.right * speed * Time.deltaTime / 0.02f * modX);
				dir = RIGHT;
			}
			// Left.
			if (Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D) &&
			    !(Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.S))) {
				transform.Translate (Vector2.left * speed * Time.deltaTime / 0.02f * modX);
				dir = LEFT;
			}
			// Up.
			if (Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.S) &&
			    !(Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.D))) {
				transform.Translate (Vector2.up * speed * Time.deltaTime / 0.02f * modY);
				dir = UP;
			}
			// Down.
			if (Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.W) &&
			    !(Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.D))) {
				transform.Translate (Vector2.down * speed * Time.deltaTime / 0.02f * modY);
				dir = DOWN;
			}
		}

		// Checks if the player can talk or proceed in an event. 
		private void checkInteraction() {
			// Can only speak if no event is occurring and is not stunned.
			if (gHandler.getCurrentEvent () == null && !stunned) {
				checkTalk ();
				checkDoor ();
			}
			else if (gHandler.getCurrentEvent() != null)
				gHandler.checkEvents();
			
		}

		// Checks if the player is within a certain range of a character that can speak. Also checks if 
		// Lark is facing the person. 
		private void checkTalk() {
			// Gets the sprite renderer of Lark for getting his center proper. 
			SpriteRenderer lSpriteRenderer = (SpriteRenderer)this.gameObject.GetComponent (typeof(SpriteRenderer));
			// Subtracts the two 3D vectors transform in order to ascertain the distance between Lark and 
			// a character.
			Component[] charList = GameObject.Find ("Characters").GetComponentsInChildren<SpriteRenderer> ();
			foreach (SpriteRenderer child in charList) {
				// Gets the sprite renderer of the character. 
				Vector3 dist = child.bounds.center - lSpriteRenderer.bounds.center;
				// If close enough, and facing a character, starts dialogue.
				if (Mathf.Abs (dist.x) < 0.175 && Mathf.Abs (dist.y) < 0.175 && isFacing (lSpriteRenderer, child))
					storage.getCharacterHandler ().checkDialogue (child.gameObject, gHandler);
			}

			// Checks if Lark is across a counter. 
			foreach (GameObject counter in GameObject.FindGameObjectsWithTag("Counters")) {
				Vector3 cPos = counter.transform.position;
				Vector3 lCenter = lSpriteRenderer.bounds.center;
				// If Lark's is in FRONT of the counter and facing up, starts the dialogue with the NEAREST character 
				// (a merchant).
				if (lCenter.x > cPos.x && lCenter.x < cPos.x + 0.16f && lCenter.y < cPos.y - 0.16f && lCenter.y > cPos.y - 0.24f)
					foreach (SpriteRenderer child in charList) {
						// Gets the sprite renderer of the character. 
						Vector3 dist = child.bounds.center - lSpriteRenderer.bounds.center;
						// Note the increased Y radius as compared to the above statement.
						if (Mathf.Abs (dist.x) < 0.175 && Mathf.Abs (dist.y) < 0.35 && isFacing (lSpriteRenderer, child))
							storage.getCharacterHandler ().checkDialogue (child.gameObject, gHandler);
					}
			}
		}

		// Check if Lark is facing any doors. 
		private void checkDoor() {
			// Gets the sprite renderer of Lark for getting his center proper. 
			SpriteRenderer lSpriteRenderer = (SpriteRenderer) this.gameObject.GetComponent(typeof(SpriteRenderer));
			bool greatOakKey = false;

			// Tries to find the Great Oak Key, and if it exists, set Great Oak Key to true.
			foreach (QuestItem item in storage.getQuestItems())
				if (item is GreatOakEntryKey) {
					greatOakKey = true;
					break;
				}
			
			// If Lark has the Great Oak Key, try to open the Great Oak Door. 
			if (greatOakKey) {
				// The event that may or may not be fired off.
				GreatOakOpeningEvent GOOE = new GreatOakOpeningEvent (gHandler);	
				// The position of the Great Oak Opening event trigger. 
				Vector3 eventPos = new Vector3 (GameObject.Find ("Great Oak Opening").transform.position.x + 0.16f, 
					                   GameObject.Find ("Great Oak Opening").transform.position.y - 0.08f, 0);
				// If within a certain distance, launches the door opening event. Checks for the event trigger as a whole.
				Vector3 dist = lSpriteRenderer.bounds.center - eventPos;
				if (Mathf.Abs (dist.x) < 0.175 && Mathf.Abs (dist.y) < 0.175 && dir == -2 && storage.getMainQuest () == 0) {
					GOOE.begin ();
					gHandler.setCurrentEvent (GOOE);
				}
			}
		}

		// Check if Lark should be affected by any tile effects.
		private void checkEffects() {
			// Checks if Lark should be slowed. 
			bool shouldSlow = false;
			// Gets the mesh renderer for all the tiles in the slow tiles layer.
			BoxCollider2D[] tiles = GameObject.Find("Slow Areas").GetComponentsInChildren<BoxCollider2D>();
			// Checks if the slow box's bounds contains Lark's center point.
			foreach (BoxCollider2D tile in tiles)
				if ((tile.bounds.Contains (this.gameObject.transform.position)))
					shouldSlow = true;
			// If Lark has gone into a slow tile, slows him. 
			if (shouldSlow) {
				slowed = true;
				modX = 0.65f;
				modY = 0.65f;
			} else if (slowed) {
				slowed = false;
				modX = 1;
				modY = 1;
			}
		}

		// Checks if Lark is facing the target object. 
		private bool isFacing(SpriteRenderer lSpriteRenderer, SpriteRenderer cSpriteRenderer) {
			// Object is to the right and Lark is facing right. 
			if (cSpriteRenderer.bounds.center.x > lSpriteRenderer.bounds.center.x &&
				inRange(lSpriteRenderer, cSpriteRenderer, RIGHT) && dir == RIGHT)
				return true;
			// Object is to the left and Lark is facing left. 
			if (cSpriteRenderer.bounds.center.x < lSpriteRenderer.bounds.center.x &&
				inRange(lSpriteRenderer, cSpriteRenderer, LEFT) && dir == LEFT)
				return true;
			// Object is to the up and Lark is facing up. 
			if (cSpriteRenderer.bounds.center.y > lSpriteRenderer.bounds.center.y &&
				inRange(lSpriteRenderer, cSpriteRenderer, UP) && dir == UP)
				return true;
			// Object is to the right and Lark is facing right. 
			if (cSpriteRenderer.bounds.center.y < lSpriteRenderer.bounds.center.y &&
				inRange(lSpriteRenderer, cSpriteRenderer, DOWN) && dir == DOWN)
				return true;
			return false;
		}

		// Checks if Lark is in range of the character (e.g. if to the right of the character sprite, is 
		// in between the top and bottom of the character sprite. 
		private bool inRange(SpriteRenderer lSpriteRenderer, SpriteRenderer cSpriteRenderer, int direction) {
			switch (direction) {
			// Should not occur.
			default:
				return false;
			// If on the right or left, Lark must be at roughly the center of the y of the character in respect to
			// his y position.
			case LEFT:
			case RIGHT: 
				return lSpriteRenderer.bounds.center.y > cSpriteRenderer.bounds.min.y && 
					lSpriteRenderer.bounds.center.y < cSpriteRenderer.bounds.max.y;
			// If on the top or bottom, Lark must be roughly at the center of the x of the character in respect 
			// to his x position.
			case UP:
			case DOWN:
				return lSpriteRenderer.bounds.center.x > cSpriteRenderer.bounds.min.x && 
					lSpriteRenderer.bounds.center.x < cSpriteRenderer.bounds.max.x;
			}
		}

		// Sets the animation of the player sprite. 
		private void setAnimation() {
			// If the user stops pressing the up key, and is pressing no other keys, freezes the walk animation.
			if (Input.GetKeyUp (KeyCode.W) && !Input.GetKey (KeyCode.S) && !Input.GetKey(KeyCode.A) 
				&& !Input.GetKey(KeyCode.D) )
				anim.enabled = false;
			// If the user is holding down the W key, plays the walking animation. Also resumes animation if it 
			// is disabled. 
			if (Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.S)) {
				anim.enabled = true;
				anim.CrossFade ("MoveUp", 0);
			}

			// Same but for the left animations.
			if (Input.GetKeyUp(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) 
				&& !Input.GetKey(KeyCode.S))
				anim.enabled = false;
			if (Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.W) 
				&& !Input.GetKey(KeyCode.S)) {
				anim.enabled = true;
				anim.CrossFade ("MoveLeft", 0);
			}

			// Same but for down animations.
			if (Input.GetKeyUp (KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) 
				&& !Input.GetKey(KeyCode.D))
				anim.enabled = false;
			if (Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.W)) {
				anim.enabled = true;
				anim.CrossFade ("MoveDown", 0);
			}
			// If pressing both the up and down keys, also freezes the current animation. This is to prevent 
			// too many animations rom firing at once.
			if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W)) 
				anim.enabled = false;

			// Same but for right animations.
			if (Input.GetKeyUp (KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) 
				&& !Input.GetKey(KeyCode.S))
				anim.enabled = false;
			if (Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.W)
				&& !Input.GetKey (KeyCode.S)) {
				anim.enabled = true;
				anim.CrossFade ("MoveRight", 0);
			}
			// Same as the above condition but for left and right.
			if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)) 
				anim.enabled = false;
		}

		public void wake() {
			StartCoroutine (awaken ());
		}

		// Wakes up after resting. Only used in cutscenes. 
		private IEnumerator awaken() {
			// Waits for one second. 
			yield return new WaitForSeconds(1);
			// Unstuns and unfreezes Lark. 
			unStun ();
			unFreeze ();
			// Moves Lark to the right so that he is no longer in bed. 
			this.gameObject.transform.Translate (new Vector3 (0.16f, 0, 0));
			// Unphases Lark so that he collides again. 
			unphase ();
			// Sets Lark's animation to look down and be awake. 
			setAnimation ("IdleDown", 0);
			setDirection (-2);
			// Sets current event to null. 
			gHandler.setCurrentEvent (null);

		}

		// Sets both the handler and the storage at once. 
		public void setStorageHandler(GameHandler gHandler, Storage storage) {
			this.gHandler = gHandler;
			this.storage = storage;
		}
			
	}
}