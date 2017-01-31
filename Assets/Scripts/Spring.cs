using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {

	private AudioSource springAudio;

	void Awake () {
		springAudio = GetComponent<AudioSource> ();
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Player") {
			springAudio.PlayOneShot (SoundManager.Instance.spring);
		}
	}
}
