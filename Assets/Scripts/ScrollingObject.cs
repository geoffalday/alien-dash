using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour {

	public float scrollSpeed = -1f;

	private Rigidbody2D objectRigidBody;

	void Start () {
		objectRigidBody = GetComponent<Rigidbody2D>();
		objectRigidBody.velocity = new Vector2 (scrollSpeed, 0);
	}

	void Update () {
		if (GameManager.instance.gameOver) {
			objectRigidBody.velocity = Vector2.zero;
		}
	}
}
