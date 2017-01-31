using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : GenericSingleton<SoundManager> {

	public AudioClip jump;
	public AudioClip dieBySpike;
	public AudioClip quicksand;
	public AudioClip levelComplete;
	public AudioClip spring;

	private AudioSource soundEffectAudio;

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
