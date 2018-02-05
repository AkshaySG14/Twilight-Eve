using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AssemblyCSharp
{
	public class DialogueHandler : MonoBehaviour
	{
		// The components of the dialogue box. 
		private GameObject textbox; 
		// Gets the sprite renderer of the text box. This is to use its width values. 
		SpriteRenderer tSpriteRenderer;

		private GameObject arrowIndicator;
		// Gets the sprite renderer of the arrow indicator. This is to change its transparency. 
		SpriteRenderer aSpriteRenderer;
		private GameObject stopIndicator; 
		// Same for the stop indicator. This is to change its transparency. 
		SpriteRenderer sSpriteRenderer;

		// The game handler and the storage.
		private GameHandler gHandler;
		private Storage storage; 
		// The game event responsible for the creation of this dialogue handler. 
		private GameEvent gEvent; 

		// The dialogue inside the box. 
		private GameObject dialogue1;
		private GameObject dialogue2;
		// The actual text displayed. 
		private string text;
		// The amount of lines of text, and the max lines. 
		private int line = 0, maxLines = 0;
		// The three lists that contain the lines in array form. The latter two are used to stutter the text. 
		private string[] textList = new string[100];
		private string[] displayList = new string[100];

		// The float time used to blink the indicators. 
		float deltaTime = 0;
		// The boolean for which indicator is blinked. Also whether the dialogue handler is active (should 
		// be updating).
		bool stop = false, active = false;

		public DialogueHandler ()
		{

		}

		public void startText(GameHandler gHandler, Storage storage, GameEvent gEvent, string text, float time) {
			this.gHandler = gHandler; 
			this.storage = storage;
			this.gEvent = gEvent;

			// Initializes both lists.
			for (int i = 0; i < 99; i++) {
				textList[i] = "";
				displayList[i] = "";
			}

			// Initializes all variables.
			line = 0;
			maxLines = 0;
			deltaTime = 0;
			stop = false;
			active = false;
			// Sets the dialogue handler for the game handler. 
			gHandler.setDialogueHandler (this);

			// Starts the dialogue event after the given time.
			StartCoroutine(startDialogue(text, time));
		}

		// This is a recursive method that will constantly call itself to break up the text until it is fully divided into
		// lines.
		private void breakText() {
			string placeHolder = "";
			// If the text when displayed is longer than the dialogue box, proceeds to break it up.
			if (text.Length * 4.6f > tSpriteRenderer.bounds.size.x * 100) {
				for (int i = text.Length; i > 0; i--)
					// Finds the space to truncate the string to so that words are not broken up.
					if (i * 4.6f < tSpriteRenderer.bounds.size.x * 100 && (text.ToCharArray())[i] == (" ".ToCharArray())[0]) {
						// Truncates the string to that point and adds it to the array position, as decided by the line
						// integer.
						for (int x = 0; x <= i; x++)
							textList[line] = textList[line] + (text.ToCharArray())[x];
						// Increments the line, as the next iteration of the method will need a new array position.
						line++;
						// Sets the text value to the REST of the string by adding it to a placeholder string. The text
						// value string is then set to the placeholder.
						for (int y = i + 1; y < text.Length; y++)
							placeHolder = placeHolder + (text.ToCharArray())[y];
						text = placeHolder;
						break;
					}
				// Recursive looping until the text value is a small enough length. Exits the method.
				breakText();
				return;
			}
			// Sets the final line of text, as the entire if statement is skipped if the method reaches this far.
			textList[line] = text;
			// Gets the total number of lines.
			maxLines = line;
			// Sets the line integer back to zero, as it is later used to draw the text.
			line = 0;
			// Draws the text onto the screen.
			stutterText();
			// Creates the arrow or stop sprite that indicates if the user is at the end of the dialogue (bottom right corner
			// of the box).
			setBackgroundSprite();
		}

		// Slowly writes the text out to create a more aesthetically pleasing effect.
		private void stutterText() {
			float loopTime = 0;
			// Line 1 (top text).
			for (int o = 0; o < textList [line].Length; o++) {
				// Sets the stagger coroutine and then starts it. 
				StartCoroutine (stagger (line, o, loopTime));
				// Increases time.
				loopTime += 0.05f;
			}
			// Line 2 (bottom text).
			for (int o = 0; o < textList [line + 1].Length; o++) {
				// Sets the stagger coroutine and then starts it. 
				StartCoroutine (stagger (line + 1, o, loopTime));
				// Increases time.
				loopTime += 0.05f;
			}
		}

		// The iEnumerator responsible for stuttering the text. 
		private IEnumerator stagger(int arrayLine, int charNumber, float time) {
			// Waits 0.025 seconds per interval.
			yield return new WaitForSeconds(time);
			// Adds the character one at a time to the display list.
			displayList[arrayLine] = displayList[arrayLine] + (textList[arrayLine].ToCharArray())[charNumber];
		}

		// When given input, Moves to the next line.
		public void incrementLine() {
			// If not active, return. 
			if (!active)
				return;

			// If the user is at the end of the dialogue, exits the dialogue and event.
			if (line + 1 >= maxLines) {
				endText ();
				gEvent.proceed ();
			}
			// Otherwise increases the line by two. NOTE: this is because line + 1 denotes the line that is drawn to the
			// bottom of the current line.
			else {
				line += 2;
				// Draws the new line in a stutter-like fashion.
				stutterText();
				// Checks if the arrow should be changed to a square.
				setBackgroundSprite();
			}
		}

		// Sets all relevant sprites' positions to zero. 
		private void endText() {
			// Clear all remaining coroutines. 
			StopAllCoroutines();
			// Sets text to no value.
			Text dText1 = (Text) dialogue1.GetComponent(typeof(Text));
			dText1.text = "";
			Text dText2 = (Text) dialogue2.GetComponent(typeof(Text));
			dText2.text = "";
			text = "";
			// Moves text box out of the way.
			textbox.transform.position = new Vector3 (-50, -50, 0);
			// Sets active to false, disallowing updates. 
			active = false;
			// Sets game handler to no longer receive input from this handler. 
			gHandler.setDialogueHandler(null);
		}

		// Sets the small indicator at the corner to be a arrow or square, depending on whether the user is on
		// the last string of dialogue.
		private void setBackgroundSprite() {
			// Last or second last line -- use the stop indicator.
			if (line + 1 >= maxLines) {
				// Sets the stop indicator to be opaque while the arrow is transparent. Also sets stop to 
				// true.
				sSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
				aSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
				stop = true;
			}
			// The reverse. 
			else { 
				// Sets stop to false as well.
				aSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
				sSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
				stop = false;
			}
		}

		// Draws the current line of text and resets the position of the background.
		void Update() {
			// If inactive, return. 
			if (!active)
				return;
			// Gets the text object and sets its text to the two lines of display text.
			Text dText1 = (Text) dialogue1.GetComponent(typeof(Text));
			dText1.text = displayList [line];
			Text dText2 = (Text) dialogue2.GetComponent(typeof(Text));
			dText2.text = displayList [line + 1];
			// Adds delta time and checks for blink.
			deltaTime += Time.deltaTime;
			checkBlink ();
		}

		// Checks if the transparent time is correct to make the indicator blink. 
		private void checkBlink() {
			// Every 5 time instances, checks for the blink.
			if (deltaTime > 0.5f) {
				deltaTime = 0;
				// If the stop indicator is currently displayed.
				if (stop) {
					// If currently transparent set opaque. 
					if (sSpriteRenderer.color.a == 0)
						sSpriteRenderer.color = new Color (1f, 1f, 1f, 1f);
					// Else if currently opaque set transparent.
					else 
						sSpriteRenderer.color = new Color (1f, 1f, 1f, 0f);
				}
				// If the arrow indicator is currently displayed.
				else {
					// If currently transparent set opaque. 
					if (aSpriteRenderer.color.a == 0)
						aSpriteRenderer.color = new Color (1f, 1f, 1f, 1f);
					// Else if currently opaque set transparent.
					else 
						aSpriteRenderer.color = new Color (1f, 1f, 1f, 0f);
				}
			}
		}

		// Delays the start of the dialogue vent by the time given. 
		private IEnumerator startDialogue(string text, float time) {
			yield return new WaitForSeconds(time);
			// Gets the three sprites involved. 
			textbox = this.gameObject;
			arrowIndicator = GameObject.Find ("Arrow Indicator");
			stopIndicator = GameObject.Find ("Stop Indicator");
			dialogue1 = GameObject.Find ("Dialogue 1");
			dialogue2 = GameObject.Find ("Dialogue 2");
			// Gets the textbox sprite renderer.
			tSpriteRenderer = (SpriteRenderer) textbox.GetComponent(typeof(SpriteRenderer));
			// Gets the arrow sprite renderer. 
			aSpriteRenderer = (SpriteRenderer) arrowIndicator.GetComponent(typeof(SpriteRenderer));
			// Same for the stop renderer.
			sSpriteRenderer = (SpriteRenderer) stopIndicator.GetComponent(typeof(SpriteRenderer));

			// Sets the text box to the bottom of the screen. 
			textbox.transform.position = new Vector3(storage.getCellX() * 1.92f + 0.96f, 
				(storage.getCellY() - 1) * 1.92f + tSpriteRenderer.bounds.size.y);

			// Sets active to true, allowing updates. 
			active = true;

			// Sets the text and then promptly breaks it.
			this.text = text;
			breakText ();
		}
	}
}

