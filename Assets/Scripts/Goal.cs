using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	private AudioSource goalAudio;

	void Awake () {
		goalAudio = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			goalAudio.PlayOneShot (SoundManager.Instance.levelComplete);
			GameManager.Instance.LevelCompleted ();
		}
	}

}
