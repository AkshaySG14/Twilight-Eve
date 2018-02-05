using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{
	public class Irune : Character {

		public Irune() {
			
		}

		// Use this for initialization
		void Start () {
			// Gets the animator for the player as per the Unix Engine.
			anim = GetComponent<Animator>();
			anim.CrossFade ("IdleDown", 0);
		}

		// Overrides base class method. 
		public override void faceDirection(int dir) {
			switch (dir) {
			case RIGHT:
				anim.CrossFade ("IdleRight", 0);
				break;
			case LEFT:
				anim.CrossFade ("IdleLeft", 0);
				break;
			case UP:
				anim.CrossFade ("IdleUp", 0);
				break;
			case DOWN:
				anim.CrossFade ("IdleDown", 0);
				break;
			}
		}

		// Update is called once per frame
		void Update () {
			if (paused)
				return;
		}
	}
}