using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{
	// This is the class that is responsible for the starting event. Note that it binds together all the 
	// different events, so as to facilitate input and response. 

	public abstract class GameEvent {

		protected GameHandler gHandler;
		protected GameObject lark;
		protected Mask mask;
		protected Lark larkScript;
		protected Storage storage;

		protected int stage = 0;

		// Gets the game handler, the storage, and lark for child classes.
		public GameEvent(GameHandler gHandler) {
			this.gHandler = gHandler;
			this.lark = gHandler.getLark ();
			this.storage = gHandler.getStorage ();
			this.larkScript = storage.getLarkScript ();
			// Also gets the Mask's script. 
			mask = 	(Mask) storage.getMask().GetComponent(typeof(Mask));
		}

		// Use this for initialization
		void Start () {
		}

		// Update is called once per frame
		void Update () {

		}

		// Begins the event in the child class.
		public void begin() {
			beginEvent (); 
		}
		// Begins the event, stunning Lark and doing other prepatory things.
		protected abstract void beginEvent ();

		// Proceeds the event. Usually increments stage and updates what's occuring.
		public void proceed() {
			stage++;
			message ();
		}
		// Ends the event, usually getting rid of dialogue and unstunning Lark and other sprites.
		protected abstract void end();
		// What is displayed or occurring in the event. Is usually called every time proceed is.
		protected abstract void message();
	}
}