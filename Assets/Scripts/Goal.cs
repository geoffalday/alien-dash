using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.levelComplete);
			GameManager.Instance.LevelCompleted ();
		}
	}

}
