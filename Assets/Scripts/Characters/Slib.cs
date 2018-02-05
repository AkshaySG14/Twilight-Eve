using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{
	public class Slib : Character {

		public Slib() {

		}

		// Use this for initialization
		void Start () {
			// Gets the animator for the player as per the Unix Engine.
			anim = GetComponent<Animator>();
			switch (initialDirection) {
			case 1:
				anim.CrossFade ("MoveRight", 0);
				break;
			case -1:
				anim.CrossFade ("MoveLeft", 0);
				break;
			case -2:
				anim.CrossFade ("MoveDown", 0);
				break;
			case 2:
				anim.CrossFade ("MoveUp", 0);
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