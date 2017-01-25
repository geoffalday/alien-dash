using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance;
	public AudioClip jump;
	public AudioClip dieBySpike;
	public AudioClip quicksand;
	public AudioClip levelComplete;
	public AudioClip gameComplete;

	private AudioSource soundEffectAudio;

	void Awake () {
		if (instance == null) {
			instance = this;
		}  else if (instance != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}
		
	void Start () {
		AudioSource[] sources = GetComponents<AudioSource> ();

		foreach (AudioSource source in sources) {
			if (source.clip == null) {
				soundEffectAudio = source;
			}
		}
	}

	public void PlayOneShot(AudioClip clip) {
		soundEffectAudio.PlayOneShot(clip);
	}

}
