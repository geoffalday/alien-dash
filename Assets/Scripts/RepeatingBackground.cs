using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour {

	private BoxCollider2D bgCollider;
	private float bgHorizontalLength;

	void Awake () {
		bgCollider = GetComponent<BoxCollider2D> ();
		bgHorizontalLength = bgCollider.size.x;
	}

	void Update () {
		if (transform.position.x < -bgHorizontalLength) {
			RepositionBackground ();
		}
	}

	void RepositionBackground () {
		Vector2 bgOffset = new Vector2 (bgHorizontalLength * 2f, 0);
		transform.position = (Vector2)transform.position + bgOffset;
	}
}
