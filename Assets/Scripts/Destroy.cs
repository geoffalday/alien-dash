using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

	public float destroyDelay = 0f;

	void Start () {
		Destroy (gameObject, destroyDelay);
	}

}
