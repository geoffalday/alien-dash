using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

	private CanvasGroup canvasGroup;
	private float fadeSpeed;

	void Awake () {
		canvasGroup = GetComponent<CanvasGroup> ();
		fadeSpeed = 0.5f;
	}

	void OnEnable () {
		StartCoroutine ("FadeIn");
		canvasGroup.alpha = 0;
	}

	void OnDisable () {
		canvasGroup.alpha = 0;
	}

	IEnumerator FadeIn () {
		while (canvasGroup.alpha < 1) {
			canvasGroup.alpha += Time.deltaTime / fadeSpeed;
			yield return null;
		}

		yield return null;
	}
		
}
