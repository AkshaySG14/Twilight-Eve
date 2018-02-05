using UnityEngine;
using System.Collections;
using System;

namespace AssemblyCSharp
{
	public class Character : MonoBehaviour
	{
		// The speed of the character sprite. Can be changed in-engine. 
		protected float speed = 0.01f; 

		// The direction that the character has last travelled in. Used for dialogue. 
		protected int dir = -0;

		// The initial direction set by the Unity engine. Used to set the initial animation. 
		public int initialDirection = 0;

		// Whether the character moves initially or not. 
		public bool moveInit = false;

		// Whether the citizen is moving. 
		protected bool moving = false;

		// The directions that the character can face. 
		protected const int LEFT = -1, RIGHT = 1, UP = 2, DOWN = -2;

		// The modifiers for speed (left, right, down, up). 
		protected float modX = 1, modY = 1;

		// Whether the character can move. Changed via the stun and unstun method. 
		protected bool stunned = false;

		// Whether the character is slowed. 
		protected bool slowed = false;

		// Whether the character animation should update. Changed via freeze and unfreeze method. 
		protected bool frozen = false;

		// Whether the character can update. Is usually set to true when the game is paused, and set to 
		// false otherwise. Consistent amongst all instances.
		protected static bool paused = false;

		// The animator for the character sprite. 
		protected Animator anim; 

		// The handler in charge of the game. 
		protected GameHandler gHandler;
		// The storage for the game. 
		protected Storage storage;

		public Character ()
		{
			
		}

		// Checks if the current character is within drawing distance of Lark (so as to avoid updates when the character 
		// is out of frame). 
		protected bool checkUpdate() {
			// Gets the character's and Lark's sprite renderers. 
			SpriteRenderer renderer = (SpriteRenderer) this.gameObject.GetComponent (typeof(SpriteRenderer));
			SpriteRenderer lRenderer = (SpriteRenderer) storage.getLark ().GetComponent (typeof(SpriteRenderer));
			// If the character is in the same CELL as Lark, return true, else false.
			return (int)(renderer.transform.position.x * 100 / 192) == (int)(lRenderer.transform.position.x * 100 / 192) &&
				(int)(renderer.transform.position.y * 100 / 192) == (int)(lRenderer.transform.position.y * 100 / 192);
		}

		public virtual void faceDirection(int dir) {
			this.dir = dir;
			// Changes animation accordingly. 
			switch (dir) {
			case RIGHT:
				anim.CrossFade ("MoveRight", 0);
				break;
			case LEFT:
				anim.CrossFade ("MoveLeft", 0);
				break;
			case UP:
				anim.CrossFade ("MoveUp", 0);
				break;
			case DOWN:
				anim.CrossFade ("MoveDown", 0);
				break;
			}
		}

		// Moves the character forward by a slight amount (used to simulate random movement). 
		public void moveForward(int dir) {
			// Faces the proper direction.
			faceDirection (dir);
			// Starts character movement.
			StartCoroutine (startMoving ());
			// Sets moving to true. 
			moving = true;
		}

		// Moves the character every 0.01 seconds for 1 second.
		private IEnumerator startMoving() {
			for (float i = 0; i <= 100; i += 1) {
				// Waits until there are NO current events and when the character is  in the same cell as Lark.
				if (!checkUpdate () || gHandler.getCurrentEvent() != null) {
					yield return new WaitWhile (() => !checkUpdate ());
					yield return new WaitWhile (() => gHandler.getCurrentEvent() != null);
				}
				// Waits for 0.01 seconds before continuing. 
				yield return new WaitForSeconds(0.01f);
				// While not stunned (default for 1 second), moves.
				switch (dir) {
				case RIGHT:
					transform.Translate (Vector2.right * speed * Time.deltaTime / 0.02f * modX / 2.5f);
					break;
				case LEFT:
					transform.Translate (Vector2.left * speed * Time.deltaTime / 0.02f * modX / 2.5f);
					break;
				case UP:
					transform.Translate (Vector2.up * speed * Time.deltaTime / 0.02f * modY / 2.5f);
					break;
				case DOWN:
					transform.Translate (Vector2.down * speed * Time.deltaTime / 0.02f * modY / 2.5f);
					break;
				}
				// If on the last iteration, set moving to false. 
				if (i == 100)
					moving = false;
			}
		}
			
		public void pause() {
			paused = true;
			freeze ();
		}

		public void unpause() {
			paused = false;
			unFreeze ();
		}

		// Note that the actual freeze begins after a 0.1 second delay. This is to ensure that any animations 
		// inputted before the freeze successfully play. Uses asynchronization to achieve this. This is a 
		// nasty work around because unfortunately animator.crossfade works on the same principles, and thus 
		// cannot be paired with a synchronized task, as such a task will precede it. 
		public void freeze() {
			StartCoroutine (waitFreeze ());
		}

		// Freezes the character, making animations impossible to change. Does so after 0.1 seconds.
		private IEnumerator waitFreeze() {
			yield return new WaitForSeconds(0.1f);
			frozen = true;
			anim.enabled = false;
		}

		// Stuns Lark, making movement impossible. 
		public void stun() {
			stunned = true;
		}

		// Phases the character, halting any collision. 
		public void phase() {
			BoxCollider2D collider = (BoxCollider2D) this.gameObject.GetComponent (typeof(BoxCollider2D));
			collider.enabled = false;
		}

		// Unstuns the character, making movement possible. 
		public void unStun() {
			stunned = false;
		}

		// Unfreezes the character, making animations possible to change. 
		public void unFreeze() {
			frozen = false;
			anim.enabled = true;
		}

		// Unphases the character, reinstating any collision. 
		public void unphase() {
			BoxCollider2D collider = (BoxCollider2D) this.gameObject.GetComponent (typeof(BoxCollider2D));
			collider.enabled = true;
		}

		// Sets the direction of the character. 
		public void setDirection(int dir) {
			this.dir = dir;
		}

		// Plays the animation given with the transition time given.
		public void setAnimation(string animation, float time) {
			anim.CrossFade (animation, time);
		}

		// Sets the storage to the given one. 
		public void setInitialVariables(GameHandler gHandler, Storage storage) {
			this.gHandler = gHandler;
			this.storage = storage;
		}

		// Returns whether the character does move or not (moveInit). 
		public bool doesMove() {
			return moveInit;
		}
		// After movement, executes the proceeding action. This method is meant to be overidden. 
		protected virtual void newAction() {
			
		}
	}
}

