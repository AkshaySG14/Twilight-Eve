using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{
	public class Citizen14 : Character {

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
			if (paused || !checkUpdate())
				return;
			// Moves in a square pattern. 
			if (!moving && moveInit) {
				if (dir == 1)
					moveForward (2);
				else if (dir == 2)
					moveForward (-1);
				else if (dir == -1)
					moveForward (-2);
				else
					moveForward (1);
			}
		}
	}
}
