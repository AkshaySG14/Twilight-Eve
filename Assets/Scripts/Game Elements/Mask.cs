using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{
	public class Mask : MonoBehaviour {

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		// Fades the mask in or out. 
		public void fade(bool fadeout, float time) {
			// Clears pre-existing coroutines. 
			StopAllCoroutines();
			// Fades out by gradually increasing the alpha value.
			if (fadeout)
				for (double i = 0; i <= time; i += 0.1)
					StartCoroutine (gradFade (i / time, (float) i));
			// Fades in by gradually decreasing the alpha value.
			else
				for (double i = time; i >= 0; i -= 0.1)
					StartCoroutine (gradFade (i / time, (float) (time - i)));
		}

		// Instantly sets alpha of mask. 
		public void setAlpha(float alpha) {
			SpriteRenderer renderer = (SpriteRenderer) this.gameObject.GetComponent(typeof(SpriteRenderer));
			renderer.color = new Color(1f, 1f, 1f, (float) alpha);
		}

		// This is the enumerator responsible for staggering the fade interval such that a gradual fade is 
		// achieved. 
		private IEnumerator gradFade(double alpha, float time) {
			// Waits 0.05 seconds per interval.
			yield return new WaitForSeconds(time);
			// Sets the alpha value of the mask to the given alpha. This is accomplished by getting the 
			// sprite renderer and setting its alpha.
			SpriteRenderer renderer = (SpriteRenderer) this.gameObject.GetComponent(typeof(SpriteRenderer));
			renderer.color = new Color(1f, 1f, 1f, (float) alpha);
		}
	}
}